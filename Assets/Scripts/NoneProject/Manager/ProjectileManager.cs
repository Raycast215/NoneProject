using NoneProject.Interface;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Projectile;
using NoneProject.Pool;
using NoneProject.Pool.Common;
using Template.Manager;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile을 관리하고 Pool을 실행하는 클래스입니다.
    public class ProjectileManager : SingletonBase<ProjectileManager>, IObjectPoolManager<ProjectilePool, ProjectileController>
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private ObjectPoolController<ProjectilePool, ProjectileController> _poolController;
        private ProjectileStatManager _statManager;
        
        public async UniTask<ProjectileController> Get(string poolObjectID)
        {
            // Manager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => isInitialized, cancellationToken: _cts.Token);
            
            return await _poolController.Get(poolObjectID);
        }
        
        public void Release(ProjectilePool poolObject)
        {
            _poolController.Release(poolObject);
        }

        private ProjectileController SetController(string poolObjectID, ProjectileController controller)
        {
            // Pool 오브젝트 반환.
            var poolObject = _poolController.Pool.Get();

            controller.ClearEvent();
            controller.OnStatUpdated += _statManager.GetData;
            controller.OnReleased += () => Release(poolObject);
            
            // Pool에 Controller 등록.
            poolObject.SetController(controller);
            // 오브젝트 ID 등록.
            poolObject.SetID(poolObjectID);
            
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

            _poolController = new ObjectPoolController<ProjectilePool, ProjectileController>(transform, constData.ProjectileObjectPath);

            _statManager = new ProjectileStatManager();
            
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