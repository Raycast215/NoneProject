using NoneProject.Actor.Component.Model;
using NoneProject.Common;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy를 관리하는 클래스입니다.
    public class EnemyController : ActorBase
    {
        private EnemyMoveController _moveController;
        private ModelController _modelController;

        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy is false)
                return;
            
            _moveController.Move(MoveSpeed, Vector2.zero);
        }

        public void Set()
        {
            MoveSpeed = 1.0f;
            _moveController.SetPattern(MovePattern.Random);
        }
        
        public void SetPosition(Vector2 position, bool isRandom = false)
        {
            var pos = isRandom 
                ? Util.GetRandomDirVec(transform.position, 2.0f, 2.0f) 
                : position;
            
            _moveController.SetPosition(pos);
        }

#region Override Methods
        
        public override void Move(Vector2 moveVec)
        {
            _moveController.Move(MoveSpeed, moveVec);
        }

        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            _moveController = new EnemyMoveController(Rigidbody2D);
            
            Subscribe();
            Set();
            
            IsInitialized = true;
        }
        
        protected override void Subscribe()
        {
            _moveController.OnAnimationStateChanged += state => _modelController.SetAnimationState(state);
            _moveController.Subscribe();
        }
        
#endregion
    }
}