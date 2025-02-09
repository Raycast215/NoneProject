using System;
using System.Collections.Generic;
using NoneProject.Actor.Component.Model;
using NoneProject.Actor.Component.Move;
using NoneProject.Actor.Data;
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
        public event Func<string, PlayerStatData> OnStatUpdated;
        
        public Transform PlayerCenter => center;
        public Vector2 MoveVec { get; private set; }
        public Vector2 Direction { get; private set; } = Vector2.left;
        
        [SerializeField] private Transform center;
        
        private readonly Dictionary<bool, IMovable> _movePatternDic = new Dictionary<bool, IMovable>();
        private ModelController _modelController;
        private PlayerStatController _statController;
        private IMovable _mover;
        
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
            MoveVec = moveVec;
            
            if (moveVec == Vector2.zero)
            {
                _modelController.SetAnimationState(ActorState.Idle);
                return;
            }

            Direction = moveVec;
            
            _mover.SetMoveVec(moveVec);
            _mover.Move(_statController.MoveSpeed);
        }
        
#region Override Methods

        protected override void Initialize()
        {
            var playerID = GameManager.Instance.Const.DefaultPlayerID;
            var stat = OnStatUpdated?.Invoke(playerID);
            
            _modelController = new ModelController(this);
            _statController = new PlayerStatController(stat);
            IsInitialized = true;
            
            ChangeMove(false);
        }

#endregion
    }
}