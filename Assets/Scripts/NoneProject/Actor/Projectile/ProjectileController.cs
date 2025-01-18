using UnityEngine;

namespace NoneProject.Actor.Projectile
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile을 관리하는 클래스입니다.
    public class ProjectileController : ActorBase
    {
        private ProjectileMoveController _moveController;
        
        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy is false)
                return;
            
            _moveController.Move(MoveSpeed, Vector2.zero);
        }

        public void Set(Vector2 startPos, Transform caster, Transform target = null)
        {
            _moveController.SetPosition(startPos);
            _moveController.Set(caster.position, target);
        }

#region Override Methods

        protected override void Initialize()
        {
            _moveController = new ProjectileMoveController(Rigidbody2D);
            MoveSpeed = 3.0f;
            
            Subscribe();
            
            IsInitialized = true;
        }

        protected override void Subscribe()
        {
            _moveController.Subscribe();
        }
        
#endregion
    }
}