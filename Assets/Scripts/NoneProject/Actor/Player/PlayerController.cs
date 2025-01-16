using NoneProject.Actor.Behaviour;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 로직을 처리하는 클래스입니다.
    public class PlayerController : ActorBase
    {
        private PlayerMoveController _moveController;
        private ModelAnimationBehaviour _modelAnimationBehaviour;
        
        private void FixedUpdate()
        {
            if (GameManager.Instance.InGame.IsAutoMove is false)
                return;
            
            _moveController.Move();
        }
        
        private void Subscribed()
        {
            _moveController.OnAnimationStateChanged += state => _modelAnimationBehaviour.SetAnimationState(state);
            _moveController.Subscribe();
        }
        
#region Override Methods
        
        public override void Initialized()
        {
            base.Initialized();
                    
            _modelAnimationBehaviour = new ModelAnimationBehaviour(Model);
            _moveController = new PlayerMoveController(Rigidbody2D);
            _moveController.SetMoveSpeed(1.0f);
                    
            Subscribed();

            IsInitialized = true;
        }

        public override void Move(float moveSpeed = 1.0f, Vector2 moveVec = new Vector2())
        {
            _moveController.Move(moveVec: moveVec);
        }
        
#endregion
    }
}