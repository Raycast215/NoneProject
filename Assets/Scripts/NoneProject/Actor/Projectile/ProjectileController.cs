using UnityEngine;

namespace NoneProject.Actor.Projectile
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile을 관리하는 클래스입니다.
    public class ProjectileController : ActorBase
    {
        private ProjectileMoveController _moveController;
        private Vector2 _targetVec;
        
        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy is false)
                return;
            
            //_moveController.Move(mOVES _targetVec);
        }

        public void SetPosition(Vector2 pos)
        {
            _moveController.SetPosition(pos);
        }

        public void SetAngle(Vector2 casterPos)
        {
            _moveController.SetAngle(casterPos);
        }

#region Override Methods
        
        public override void Move(Vector2 moveVec)
        {
            
        }

        protected override void Initialize()
        {
            _moveController = new ProjectileMoveController(Rigidbody2D);
            MoveSpeed = 1.0f;
            
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