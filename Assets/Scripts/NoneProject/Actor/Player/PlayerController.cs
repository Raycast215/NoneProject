using NoneProject.Actor.Behaviour;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 로직을 처리하는 클래스입니다.
    public class PlayerController : ActorBase
    {
        private PlayerMoveBehaviour _moveBehaviour;
        private ModelAnimationBehaviour _modelAnimationBehaviour;
        
        private void Subscribed()
        {
            _moveBehaviour.Subscribed();
            _moveBehaviour.OnAnimationStateChanged += state => _modelAnimationBehaviour.SetAnimationState(state);
        }
        
#region Override Methods
        
        public override void Initialized()
        {
            base.Initialized();
                    
            _modelAnimationBehaviour = new ModelAnimationBehaviour(Model);
            _moveBehaviour = new PlayerMoveBehaviour(Rigidbody2D);
            _moveBehaviour.SetMoveSpeed(1.0f);
                    
            Subscribed();

            IsLoaded = true;
        }

        public override void Move(Vector2 dirVec)
        {
            _moveBehaviour.Move(dirVec);
        }
        
#endregion
    }
}