using System.Collections.Generic;
using NoneProject.Actor.Component.Model;
using NoneProject.Actor.Component.Move;
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
        private readonly Dictionary<bool, IMovable> _movePatternDic = new Dictionary<bool, IMovable>();
        private ModelController _modelController;
        private PlayerStat _stat;
        private IMovable _mover;
        
        private void FixedUpdate()
        {
            if (IsInitialized is false)
                return;

            if (GameManager.Instance.InGame.IsAutoMove is false)
                return;
            
            _mover.Move(MoveSpeed);
        }

        public void ChangeMove(bool isAutoMove)
        {
            if (isAutoMove is false)
            {
                _modelController.SetAnimationState(ActorState.Idle);
            }

            // 이전에 사용한 패턴이 있는 경우.
            if (_movePatternDic.TryGetValue(isAutoMove, out var pattern))
            {
                _mover = pattern;
                return;
            }
            
            _mover = isAutoMove
                ? new MoveRandomVector(Rigidbody2D) // 자동이동을 위한 기능.
                : new MoveForward(Rigidbody2D);     // 터치로 이동을 위한 기능.
                
            _movePatternDic.Add(isAutoMove, _mover);
            _mover.CompleteMove(_ => _modelController.SetAnimationState(ActorState.Run));
            _mover.CompleteMove(SetScaleDirection);
        }
        
        public void Move(Vector2 moveVec)
        {
            if (moveVec == Vector2.zero)
            {
                _modelController.SetAnimationState(ActorState.Idle);
                return;
            }
            
            _mover.SetMoveVec(moveVec);
            _mover.Move(MoveSpeed);
        }
        
#region Override Methods

        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            MoveSpeed = 2.0f;
            IsInitialized = true;
            
            ChangeMove(false);
        }

#endregion
    }
}