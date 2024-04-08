
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using NoneProject.Pool;
using Template.Manager;
using Template.Pool;
using UnityEngine;


namespace NoneProject.Manager
{
    public class PoolManager : SingletonBase<PoolManager>
    {
        private const string SoundKey = "SoundObject";
        private const string ProjectileKey = "ProjectileObject";
        private const int CratePoolCount = 10;
        
        public Pooling<SoundPool> SoundPool => soundPool;
        public Pooling<ProjectilePool> ProjectilePool => projectilePool;

        private static Pooling<SoundPool> soundPool;
        private static Pooling<ProjectilePool> projectilePool;
        
        private Transform _soundHolder;
        private Transform _projectileHolder;

        // To Do : 씬 이동 시 해당 오브젝트를 위치 이동시켜야함.
        
        private void LoadHolder()
        {
            _soundHolder = new GameObject(Define.SoundHolder).transform;
            _soundHolder.SetParent(transform);
            
            _projectileHolder = new GameObject(Define.ProjectileHolder).transform;
            _projectileHolder.SetParent(transform);
        }
        
        private async void LoadPool()
        {
            await UniTask.WaitUntil(() => AddressableManager.Instance.IsInitialized, cancellationToken: Cts.Token);
            
            AddressableManager.Instance.LoadAsset<SoundPool>(SoundKey, result => Load(out soundPool, result, _soundHolder));
            AddressableManager.Instance.LoadAsset<ProjectilePool>(ProjectileKey, result => Load(out projectilePool, result, _projectileHolder));
        }

        private void Load<T>(out Pooling<T> pooling, T prefab, Transform holder) where T : PoolBase
        {
            pooling = new Pooling<T>(prefab, CratePoolCount, holder);
            pooling.Pool();
        }

#region Override Implementation
        
        protected override void Initialized()
        {
            LoadHolder();
            LoadPool();
            
            IsInitialized = true;
        }
        
#endregion
    }
}