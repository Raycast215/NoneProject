using NoneProject.Actor.Component.Model;
using NoneProject.Actor.Component.Move.Pattern;
using NoneProject.Common;
using NoneProject.Interface;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 로직을 처리하는 클래스입니다.
    public class PlayerController : ActorBase
    {
        public Transform Direction { get; private set; }
        
        [SerializeField] private Transform directionPoint;
        
        //private PlayerMoveController _moveController;
        private ModelController _modelController;

        private IMovable _mover;
        
        private void FixedUpdate()
        {
            if (IsInitialized is false)
                return;

            if (GameManager.Instance.InGame.IsAutoMove is false)
                return;
            
            _mover.Move(MoveSpeed, Vector2.zero);
        }

        public void Set()
        {
            MoveSpeed = 2.0f;
        }

        public void ChangeMove(bool isAutoMove)
        {
            Debug.Log(isAutoMove);
            
            if (isAutoMove is false)
            {
                _modelController.SetAnimationState(ActorState.Idle);
            }
            
            _mover = isAutoMove
                ? new MoveRandomVector(Rigidbody2D)
                : new MoveForward(Rigidbody2D);
            
            _mover.MoveFinish(_ => _modelController.SetAnimationState(ActorState.Run));
        }
        
        public void Move(Vector2 moveVec)
        {
            if (moveVec == Vector2.zero)
            {
                _modelController.SetAnimationState(ActorState.Idle);
            }
            
           // _mover.Move(MoveSpeed, moveVec);
        }
        
#region Override Methods

        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            //_moveController = new PlayerMoveController(Rigidbody2D);
            Direction = directionPoint;
            
            ChangeMove(false);
            Subscribe();
            Set();

            IsInitialized = true;
        }
        
        protected override void Subscribe()
        {
            //_moveController.OnDirectionUpdated += dir => directionPoint.localPosition = dir;
            //_moveController.OnAnimationStateChanged += state => _modelController.SetAnimationState(state);
           // _moveController.Subscribe();
        }
        
#endregion
    }
}