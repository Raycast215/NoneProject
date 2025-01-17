using System;
using NoneProject.Actor.Component.Move;
using NoneProject.Common;
using UnityEngine;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy의 이동 로직을 처리하는 클래스입니다.
    public class EnemyMoveController : MoveController
    {
        public event Action<ActorState> OnAnimationStateChanged;
        
        private readonly AutoMove _autoMove;
    
        public EnemyMoveController(Rigidbody2D rigidbody2D)
        {
            Rigidbody = rigidbody2D;
            _autoMove = new AutoMove(Rigidbody);
        }

        public void SetPattern(MovePattern movePattern)
        {
            
        }

#region Override Methods
        
        public override void Move(float moveSpeed, Vector2 moveVec)
        {
            // 이동 실행.
            _autoMove.Move(moveSpeed);
        }
        
        public override void Subscribe()
        {
            _autoMove.OnMoveVecUpdated += _ => OnAnimationStateChanged?.Invoke(ActorState.Run);
            _autoMove.OnDirectionUpdated += SetDirection;
        }
        
#endregion
    }
}