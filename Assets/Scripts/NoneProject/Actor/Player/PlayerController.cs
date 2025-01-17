using NoneProject.Actor.Component.Animation;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 로직을 처리하는 클래스입니다.
    public class PlayerController : ActorBase
    {
        private PlayerMoveController _moveController;
        private ActorAnimationController _actorAnimationController;
        
        private void FixedUpdate()
        {
            if (GameManager.Instance.InGame.IsAutoMove is false)
                return;
            
            _moveController.Move(MoveSpeed, Vector2.zero);
        }
        
        private void Subscribed()
        {
            _moveController.OnAnimationStateChanged += state => _actorAnimationController.SetAnimationState(state);
            _moveController.Subscribe();
        }
        
#region Override Methods
        
        public override void Initialized()
        {
            base.Initialized();
                    
            _actorAnimationController = new ActorAnimationController(Model);
            _moveController = new PlayerMoveController(Rigidbody2D);
            MoveSpeed = 2.0f;

            Subscribed();

            IsInitialized = true;
        }

        public override void Move(Vector2 moveVec)
        {
            _moveController.Move(MoveSpeed, moveVec);
        }
        
#endregion
    }
}