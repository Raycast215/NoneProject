using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Enemy;
using NoneProject.Pool;
using Template.Manager;
using Template.Pool;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.01.15
    // Enemy를 관리하고 Pool을 실행하는 클래스입니다.
    public class EnemyManager : SingletonBase<EnemyManager>
    {
        private const string TempHolder = "Temp Holder";
        private const string EnemyHolder = "Enemy Holder";
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Dictionary<string, Queue<EnemyController>> _enemyPoolTemp = new Dictionary<string, Queue<EnemyController>>();
        private Pooling<EnemyPool> _enemyPool;
        private Transform _poolHolder;
        private Transform _tempHolder;

        public async void GetEnemy(string enemyID)
        {
            // Manager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => isInitialized, cancellationToken: _cts.Token);
            
            if (_cts is null || _cts.IsCancellationRequested)
                return;
            
            if (_enemyPoolTemp.TryGetValue(enemyID, out var queue))
            {
                // 이전에 사용완료된 객체가 있는 경우.
                if (queue.Count != 0)
                {
                    // 반환된 Enemy로 설정.
                    SetEnemy(enemyID, queue.Dequeue());
                    return;
                }
            }
            else
            {
                // 재사용할 controller Queue 초기화.
                _enemyPoolTemp[enemyID] = new Queue<EnemyController>();
            }
            
            // Enemy 에셋을 로드.
            await AddressableManager.Instance.LoadAsset<GameObject>(enemyID, OnLoadComplete);
            return;

            void OnLoadComplete(GameObject prefab)
            {
                // 생성된 Enemy로 설정.
                SetEnemy(enemyID, Instantiate(prefab, _poolHolder).GetComponent<EnemyController>());
            }
        }
        
        public void ReleaseEnemy(EnemyPool enemy)
        {
            if (_enemyPoolTemp.TryGetValue(enemy.Controller.Name, out var queue))
            {
                // Enemy 오브젝트를 임시 보관 오브젝트 하위로 이동.
                enemy.SetControllerParent(_tempHolder, false);
                // 재사용을 위한 queue에 보관.
                queue.Enqueue(enemy.Controller);
            }
            
            _enemyPool.Return(enemy);
        }

        private void SetEnemy(string enemyID, EnemyController controller)
        {
            // Pool 오브젝트 반환.
            var enemyPool = _enemyPool.Get();

            // Pool에 Controller 등록.
            enemyPool.SetController(controller);
            // Enemy 초기화.
            controller.Initialized();
            // Enemy TestID 등록.
            controller.SetName(enemyID);
            // Enemy 위치 지정.
            controller.SetPosition(Vector2.zero, true);
            // Enemy 이동 처리. 
            controller.Move();
        }
        
        private void LoadPool()
        {
            var constData = GameManager.Instance.Const;
            var enemyObject = Resources.Load<EnemyPool>(constData.EnemyObjectPath);
            
            // Pool 생성.
            _enemyPool = new Pooling<EnemyPool>(enemyObject, constData.SfxCapacity, _poolHolder);
            _enemyPool.Pool();
        }

        private void LoadHolder()
        {
            // Pool 반환 시 프리팹 재사용을 위한 공간.
            _tempHolder = Util.CreateObject(TempHolder, transform).transform;
            // Pool 오브젝트들을 보관하기 위한 공간.
            _poolHolder = Util.CreateObject(EnemyHolder, transform).transform;
        }
        
#region Override Methods
    
        protected override async void Initialized()
        {
            // GameManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);
            
            if (_cts is null || _cts.IsCancellationRequested)
                return;
            
            LoadHolder();
            LoadPool();

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