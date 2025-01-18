using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Projectile;
using NoneProject.Manager;
using UnityEngine;

namespace NoneProject.Actor.Component.Attack
{
    public abstract class AttackBase
    {
        protected CancellationTokenSource Cts = new CancellationTokenSource();
        protected readonly List<ProjectileController> ProjectileList = new List<ProjectileController>();
        protected Transform Caster;
        
        private bool _isLoaded;
        private bool _isPlaying;

        public async void Attack(string projectileID, int count, float delay)
        {
            if (count <= 0)
                return;

            if (_isPlaying)
                return;
            
            Cts ??= new CancellationTokenSource();

            _isLoaded = false;
            _isPlaying = true;

            LoadProjectiles(projectileID, count);
            
            await UniTask.WaitUntil(() => _isLoaded, cancellationToken: Cts.Token);

            SetProjectile(count, delay);
            
            ProjectileList.Clear();
            _isPlaying = false;
        }
        
        private async void LoadProjectiles(string projectileID, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var projectile = await ProjectileManager.Instance.Get(projectileID);
                
                projectile.gameObject.SetActive(false);
                ProjectileList.Add(projectile);
            }

            _isLoaded = true;
        }
        
        protected abstract void SetProjectile(int count, float delay);
    }
}