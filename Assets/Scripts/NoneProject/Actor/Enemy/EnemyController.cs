using NoneProject.Actor.Behaviour;
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
        private EnemyMoveBehaviour _moveBehaviour;
        private ModelAnimationBehaviour _modelAnimationBehaviour;

        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy is false)
                return;
            
            _moveBehaviour.Move();
        }
        
        private void Subscribed()
        {
            _moveBehaviour.OnAnimationStateChanged += state => _modelAnimationBehaviour.SetAnimationState(state);
            _moveBehaviour.Subscribe();
        }
        
        public void SetPosition(Vector2 position, bool isRandom = false)
        {
            var pos = isRandom 
                ? Util.GetRandomDirVec(transform.position, 2.0f, 2.0f) 
                : position;
            
            _moveBehaviour.SetPosition(pos);
        }

#region Override Methods
        
        public override void Initialized()
        {
            base.Initialized();
            
            _modelAnimationBehaviour = new ModelAnimationBehaviour(Model);
            _moveBehaviour = new EnemyMoveBehaviour(Rigidbody2D);
            _moveBehaviour.SetPattern(MovePattern.Random);
            _moveBehaviour.SetMoveSpeed(1.0f);
            
            Subscribed();
            
            IsLoaded = true;
        }

        public override void Move(float moveSpeed = 1.0f, Vector2 moveVec = new Vector2())
        {
            _moveBehaviour.Move();
        }
        
#endregion
    }
}