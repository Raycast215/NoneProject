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
        private EnemyMoveController _moveController;
        private ModelAnimationBehaviour _modelAnimationBehaviour;

        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy is false)
                return;
            
            _moveController.Move();
        }
        
        private void Subscribed()
        {
            if(IsInitialized)
                return;

            _moveController.OnAnimationStateChanged += state => _modelAnimationBehaviour.SetAnimationState(state);
            _moveController.Subscribe();
        }
        
        public void SetPosition(Vector2 position, bool isRandom = false)
        {
            var pos = isRandom 
                ? Util.GetRandomDirVec(transform.position, 2.0f, 2.0f) 
                : position;
            
            _moveController.SetPosition(pos);
        }

#region Override Methods
        
        public override void Initialized()
        {
            base.Initialized();
            
            _modelAnimationBehaviour ??= new ModelAnimationBehaviour(Model);
            _moveController ??= new EnemyMoveController(Rigidbody2D);
            _moveController.SetPattern(MovePattern.Random);
            _moveController.SetMoveSpeed(1.0f);
            
            Subscribed();
            
            IsInitialized = true;
        }

        public override void Move(float moveSpeed = 1.0f, Vector2 moveVec = new Vector2())
        {
            _moveController.Move();
        }
        
#endregion
    }
}