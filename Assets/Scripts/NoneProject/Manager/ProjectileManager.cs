using System;
using System.Collections;
using System.Collections.Generic;
using Template.Manager;
using UnityEngine;


namespace NoneProject.Manager
{
    public class ProjectileManager : SingletonBase<ProjectileManager>
    {
        private readonly Dictionary<string, GameObject> _projectileDic = new Dictionary<string, GameObject>();

        public void PlayProjectile(string projectileName, Vector3 pos)
        {
            // projectile pool 가져옴.
            var projectilePool = PoolManager.Instance.ProjectilePool.Get();

            projectilePool.PlayProjectile(pos).Forget();

            // 가져온 pool에 실행할 projectile를 담아 재생.
            // LoadProjectile(projectileName, projectile => projectilePool.PlayProjectile(projectile));
        }
        
        private void LoadProjectile(string projectileName, Action<GameObject> onComplete)
        {
            // 이미 key가 존재하는 경우 바로 실행.
            if (_projectileDic.ContainsKey(projectileName))
            {
                onComplete?.Invoke(_projectileDic[projectileName]);
                return;
            }
            
            // key가 없는 경우 새로 Load하여 실행.
            AddressableManager.Instance.LoadAsset<GameObject>(projectileName, OnComplete);
            return;
            
            void OnComplete(GameObject projectile)
            {
                _projectileDic.Add(projectileName, projectile);
                    
                onComplete?.Invoke(projectile);
            }
        }
        
        protected override void Initialized()
        {
            IsInitialized = true;
        }
    }
}


