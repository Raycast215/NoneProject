using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Enemy;
using NoneProject.Interface;
using NoneProject.Pool;
using NoneProject.Pool.Common;
using Template.Manager;
using UnityEngine;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.01.15
    // Enemy를 관리하고 Pool을 실행하는 클래스입니다.
    public class EnemyManager : SingletonBase<EnemyManager>, IObjectPoolManager<EnemyPool, EnemyController>
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private ObjectPoolController<EnemyPool, EnemyController> _poolController;

        public async UniTask<EnemyController> Get(string poolObjectID)
        {
            // Manager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => isInitialized, cancellationToken: _cts.Token);
            
            return await _poolController.Get(poolObjectID);
        }

        public void Release(EnemyPool poolObject)
        {
            _poolController.Release(poolObject);
        }
        
        private EnemyController SetController(string poolObjectID, EnemyController controller)
        {
            // Pool 오브젝트 반환.
            var poolObject = _poolController.Pool.Get();

            // Pool에 Controller 등록.
            poolObject.SetController(controller);
            // Enemy 초기화.
            controller.Initialized();
            // Enemy ID 등록.
            poolObject.SetID(poolObjectID);
            // Enemy 위치 지정.
            controller.SetPosition(Vector2.zero, true);
            // Enemy 이동 처리. 
            controller.Move();

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