using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Enemy;
using NoneProject.Common;
using NoneProject.Interface;
using NoneProject.Pool;
using NoneProject.Pool.Common;
using Template.Manager;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.01.15
    // Enemy를 관리하고 Pool을 실행하는 클래스입니다.
    public class EnemyManager : SingletonBase<EnemyManager>, IObjectPoolManager<EnemyPool, EnemyController>
    {
        public event Action<int> OnEnemyCountUpdated;

        public bool IsEnemyActivate => _activateList.Count != 0;
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly List<EnemyController> _activateList = new List<EnemyController>();
        private ObjectPoolController<EnemyPool, EnemyController> _poolController;
        private EnemyStatManager _statManager;

        private void FixedUpdate()
        {
            for (var i = _activateList.Count - 1; i >= 0; i--)
            {
                _activateList[i].UpdateEnemy();
            }
        }

        public EnemyController GetNearEnemy(Vector2 targetPos)
        {
            if (IsEnemyActivate is false)
                return null;

            EnemyController enemy = null;
            var temp = 0.0f;
            
            for (var i = _activateList.Count - 1; i >= 0; i--)
            {
                var dist = Mathf.Abs(Vector2.Distance(targetPos, _activateList[i].transform.position));

                if (enemy is null || dist < temp)
                {
                    temp = dist;
                    enemy = _activateList[i];
                }
            }

            return enemy;
        }

        public async void Get(string enemyID, Vector2 startPos, bool isRandomPos = false)
        {
            if (isInitialized is false)
                 return;
            
            var enemy = await _poolController.Get(enemyID);

            if (isRandomPos is false)
            {
                enemy.SetPosition(startPos);
                return;
            }

            var playerPos = PlayerManager.Instance.Player.transform.position;
            var pos = Util.GetRandomDirVec(playerPos, 20.0f, 20.0f);
            
            enemy.SetPosition(pos);
        }

        public void Release(EnemyPool poolObject)
        {
            _activateList.Remove(poolObject.Controller);
            _poolController.Release(poolObject);
            OnEnemyCountUpdated?.Invoke(_activateList.Count);
        }
        
        private EnemyController SetController(string poolObjectID, EnemyController controller)
        {
            // Pool 오브젝트 반환.
            var poolObject = _poolController.Pool.Get();

            controller.ClearEvent();
            controller.OnStatUpdated += _statManager.GetData;
            controller.OnDied += () => Release(poolObject);
            
            // Pool에 Controller 등록.
            poolObject.SetController(controller);
            // 오브젝트 ID 등록.
            poolObject.SetID(poolObjectID);
            // Stat 지정.
            controller.SetData(poolObjectID);
            // 오브젝트 위치 지정.
            controller.SetPosition(Vector2.zero);
            // Pattern 등록.
            controller.SetPattern(MovePattern.Random);
            
            _activateList.Add(controller);
            OnEnemyCountUpdated?.Invoke(_activateList.Count);
            
            return poolObject.Controller;
        }

        private void Subscribe()
        {
            _poolController.OnObjectControllerUpdated += SetController;
        }

#region Override Methods
    
        protected override async void Initialized()
        {
            var constData = GameManager.Instance.Const;
            
            // GameManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);
            
            _poolController = new ObjectPoolController<EnemyPool, EnemyController>(transform, constData.EnemyObjectPath);
            _statManager = new EnemyStatManager();
            
            // Stat Data 로드 완료까지 대기.
            await UniTask.WaitUntil(() => _statManager.IsLoaded, cancellationToken: _cts.Token);
            
            Subscribe();
            
            isInitialized = true;
        }
        
        protected override void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
         
            base.OnDestroy();
        }
    
#endregion
    }
}