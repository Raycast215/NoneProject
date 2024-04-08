
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using NoneProject.Manager;
using UnityEngine;


namespace NoneProject.Actor
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SPUM_Prefabs animationController;
        [SerializeField] private ActorState state;
        [SerializeField] private Transform projectilePos;

        private bool _isFire;
        private float _attackDelay = 1.0f;
        private CancellationTokenSource _cts;
        
        private void Start()
        {
            var aspect = (float)Screen.width / Screen.height;
            var worldHeight = Camera.main.orthographicSize * 2.0f;
            var worldWidth = worldHeight * aspect;
            
            Debug.Log(worldWidth);

            _cts = new CancellationTokenSource();
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public async UniTaskVoid Attack()
        {
            // 발사중이면 리턴.
            if (_isFire)
                return;

            _isFire = true;
            
            await UniTask.WaitForSeconds(_attackDelay, cancellationToken: _cts.Token);

            animationController.PlayAnimation(ActorState.Attack_Magic.ToString());
            SoundManager.Instance.PlaySound("Sound_Fire");
            await UniTask.WaitForSeconds(0.2f, cancellationToken: _cts.Token);
            
            ProjectileManager.Instance.PlayProjectile("Projectile_15", projectilePos.position);
           
            
            _isFire = false;
        }

        public void Move()
        {
            animationController.PlayAnimation(ActorState.Run.ToString());
        }

        public void Idle()
        {
            animationController.PlayAnimation(ActorState.Idle.ToString());
        }
    }
}