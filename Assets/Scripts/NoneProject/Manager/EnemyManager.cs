using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Enemy;
using NoneProject.Interface;
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
    public class EnemyManager : SingletonBase<EnemyManager>, IObjectPoolManager<EnemyPool, EnemyController>
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Dictionary<string, Queue<EnemyController>> _objectPoolTempDic = new Dictionary<string, Queue<EnemyController>>();
        private Pooling<EnemyPool> _pool;
        private Transform _objectPoolHolder;
        private Transform _tempHolder;
        
        public async void Get(string poolObjectID)
        {
            // Manager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => isInitialized, cancellationToken: _cts.Token);
            
            if (_objectPoolTempDic.TryGetValue(poolObjectID, out var queue))
            {
                // 이전에 반환된 오브젝트가 있는 경우.
                if (queue.Count != 0)
                {
                    // 반환된 오브젝트의 Controller를 수정.
                    SetController(poolObjectID, queue.Dequeue());
                    return;
                }
            }
            else
            {
                // 재사용하기 위한 오브젝트의 Controller를 담을 Queue 초기화.
                _objectPoolTempDic[poolObjectID] = new Queue<EnemyController>();
            }
            
            // 해당 오브젝트에서 사용되는 에셋을 로드.
            await AddressableManager.Instance.LoadAsset<GameObject>(poolObjectID, OnLoadComplete);
            return;

            void OnLoadComplete(GameObject prefab)
            {
                // 오브젝트에서 사용할 에셋을 생성해서 오브젝트의 Controller를 수정.
                SetController(poolObjectID, Instantiate(prefab, _objectPoolHolder).GetComponent<EnemyController>());
            }
        }

        public void SetController(string poolObjectID, EnemyController controller)
        {
            // Pool 오브젝트 반환.
            var poolObject = _pool.Get();

            // Pool에 Controller 등록.
            poolObject.SetController(controller);
            // Enemy 초기화.
            controller.Initialized();
            // Enemy TestID 등록.
            controller.SetID(poolObjectID);
            // Enemy 위치 지정.
            controller.SetPosition(Vector2.zero, true);
            // Enemy 이동 처리. 
            controller.Move();
        }

        public void Release(EnemyPool poolObject)
        {
            if (_objectPoolTempDic.TryGetValue(poolObject.Controller.ID, out var queue))
            {
                // Pool 오브젝트를 임시 보관 오브젝트 하위로 이동.
                poolObject.SetControllerParent(_tempHolder, false);
                // 재사용을 위한 queue에 보관.
                queue.Enqueue(poolObject.Controller);
            }
            
            _pool.Return(poolObject);
        }

        public void LoadPool()
        {
            var constData = GameManager.Instance.Const;
            var poolObject = Resources.Load<EnemyPool>(constData.EnemyObjectPath);
            
            // Pool 생성.
            _pool = new Pooling<EnemyPool>(poolObject, constData.Capacity, _objectPoolHolder);
            _pool.Pool();
        }
        
        public void CreateObjectHolder()
        {
            var constData = GameManager.Instance.Const;
            
            // Pool 반환 시 프리팹 재사용을 위한 공간.
            _tempHolder = Util.CreateObject(constData.TempHolder, transform).transform;
            // Pool 오브젝트들을 보관하기 위한 공간.
            _objectPoolHolder = Util.CreateObject(constData.ObjectPoolHolder, transform).transform;
        }

#region Override Methods
    
        protected override async void Initialized()
        {
            // GameManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);

            CreateObjectHolder();
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