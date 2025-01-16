using System;
using System.Collections.Generic;
using NoneProject.Actor.Behaviour;
using NoneProject.Actor.Move;
using NoneProject.Common;
using NoneProject.Interface;
using UnityEngine;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy의 이동 로직을 처리하는 클래스입니다.
    public class EnemyMoveController : MoveBehaviour
    {
        public event Action<ActorState> OnAnimationStateChanged;

        private Dictionary<MovePattern, IMovable> _movePatternDic;

        private MovePattern _movePattern;
        private readonly AutoMove _autoMove;
    
        public EnemyMoveController(Rigidbody2D rigidbody2D)
        {
            Rigidbody = rigidbody2D;
            _autoMove = new AutoMove(rigidbody2D);
            
            _movePatternDic = new Dictionary<MovePattern, IMovable>()
            {
                {MovePattern.Random, _autoMove}
            };
        }

        public void SetPattern(MovePattern movePattern)
        {
            _movePattern = movePattern;
        }

        public void Subscribe()
        {
            _autoMove.OnMoveVecUpdated += _ => OnAnimationStateChanged?.Invoke(ActorState.Run);
            _autoMove.OnDirectionUpdated += SetDirection;
        }

#region Override Methods
        
        public override void Move(float moveSpeed = 1.0f, Vector2 moveVec = new Vector2())
        {
            // 이동 실행.
            _autoMove.Move(MoveSpeed);
        }
        
#endregion
    }
}