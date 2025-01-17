using NoneProject.Actor.Component.Model;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 로직을 처리하는 클래스입니다.
    public class PlayerController : ActorBase
    {
        private PlayerMoveController _moveController;
        private ModelController _modelController;
        
        private void FixedUpdate()
        {
            if (GameManager.Instance.InGame.IsAutoMove is false)
                return;
            
            _moveController.Move(MoveSpeed, Vector2.zero);
        }

        public void Set()
        {
            MoveSpeed = 2.0f;
        }
        
#region Override Methods

        public override void Move(Vector2 moveVec)
        {
            _moveController.Move(MoveSpeed, moveVec);
        }
        
        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            _moveController = new PlayerMoveController(Rigidbody2D);
            
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