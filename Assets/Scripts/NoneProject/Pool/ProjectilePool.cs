
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Actor;
using NoneProject.Common;
using NoneProject.Manager;
using Template.Pool;
using UnityEngine;


namespace NoneProject.Pool
{
    public class ProjectilePool : PoolBase
    {
        // public async UniTaskVoid PlayProjectile(GameObject projectileObject)
        public async UniTaskVoid PlayProjectile(Vector3 pos)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = pos;
            
            // projectileObject.SetActive(true);
            // projectileObject.transform.SetParent(transform);
            // projectileObject.transform.position = Vector3.zero;
            // projectileObject.transform.localScale = Vector3.one;

            await UniTask.WaitForSeconds(5.0f, cancellationToken: Cts.Token);

            Release();
        }

        private void FixedUpdate()
        {
            var pos = 10.0f * Time.deltaTime;
            
            transform.Translate(Vector3.right * pos);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(ActorType.Enemy.ToString()))
            {
                Release();
            }
        }
        
        #region Override Implement
        
        protected override void Release()
        {
            Cts?.Cancel();
            Cts?.Dispose();
            Cts = new CancellationTokenSource();
            
            PoolManager.Instance.ProjectilePool.Return(this);
        }
        
#endregion
    }
}