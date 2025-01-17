using System;
using NoneProject.Actor.Component.Move;
using NoneProject.Common;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2025.01.15
    // Player의 이동 로직을 처리하는 클래스입니다.
    public class PlayerMoveController : MoveController
    {
        public event Action<Vector2> OnDirectionUpdated; 
        public event Action<ActorState> OnAnimationStateChanged;

        public Vector2 Direction { get; private set; } = Vector2.left;
        
        private readonly AutoMove _autoMove;
        private readonly DefaultMove _defaultMove;
        private Vector2 _autoTargetPosition;

        public PlayerMoveController(Rigidbody2D rigidbody2D)
        {
            Rigidbody = rigidbody2D;
            _autoMove = new AutoMove(Rigidbody);
            _defaultMove = new DefaultMove(Rigidbody);
        }
        
#region Override Methods
        
        public override void Move(float moveSpeed, Vector2 moveVec)
        {
            if (GameManager.Instance.InGame.IsAutoMove)
            {
                _autoMove.Move(moveSpeed);
                return;
            }

            if (moveVec == Vector2.zero)
            {
                // 이동한 방향 값이 없는 경우 기본 애니메이션 재생.
                OnAnimationStateChanged?.Invoke(ActorState.Idle);
                return;
            }
            
            // 바라 보는 위치 변경.
            //SetDirection(moveVec);
            // 이동 실행.
            _defaultMove.Move(moveSpeed, moveVec);
            // Player 방향 벡터 저장.
            Direction = moveVec;
            OnDirectionUpdated?.Invoke(Direction);
        }
        
        public override void Subscribe()
        {
            _autoMove.OnMoveVecUpdated += _ => OnAnimationStateChanged?.Invoke(ActorState.Run);
            _autoMove.OnDirectionUpdated += SetDirection;
            _defaultMove.Subscribe(_ => OnAnimationStateChanged?.Invoke(ActorState.Run));
        }
        
#endregion
    }
}