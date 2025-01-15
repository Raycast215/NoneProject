using System;
using NoneProject.Actor.Behaviour;
using NoneProject.Actor.Move;
using NoneProject.Common;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2025.01.15
    // Player의 이동 로직을 처리하는 클래스입니다.
    public class PlayerMoveBehaviour : MoveBehaviour
    {
        public event Action<ActorState> OnAnimationStateChanged;

        private readonly AutoMove _autoMove;
        private readonly DefaultMove _defaultMove;
        private Vector2 _autoTargetPosition;

        public PlayerMoveBehaviour(Rigidbody2D rigidbody2D)
        {
            Rigidbody = rigidbody2D;
            _autoMove = new AutoMove(Rigidbody);
            _defaultMove = new DefaultMove(Rigidbody);
            SetPosition(Vector2.zero);
        }

        public void Subscribe()
        {
            _autoMove.OnMoveVecUpdated += _ => OnAnimationStateChanged?.Invoke(ActorState.Run);
            _autoMove.OnDirectionUpdated += SetDirection;
            _defaultMove.Subscribe(() => OnAnimationStateChanged?.Invoke(ActorState.Run));
        }
        
        private void MoveDefault(Vector2 dirVec)
        {
            if (dirVec == Vector2.zero)
            {
                // 이동한 방향 값이 없는 경우 기본 애니메이션 재생.
                OnAnimationStateChanged?.Invoke(ActorState.Idle);
                return;
            }
            
            // 바라 보는 위치 변경.
            SetDirection(dirVec);
            // 이동 실행.
            _defaultMove.Move(MoveSpeed, dirVec);
        }

#region Override Methods
        
        public override void Move(float moveSpeed = 1.0f, Vector2 moveVec = new Vector2())
        {
            if (GameManager.Instance.InGame.IsAutoMove)
            {
                _autoMove.Move(MoveSpeed);
                return;
            }

            _defaultMove.SetMoveSpeed(MoveSpeed);
            MoveDefault(moveVec);
        }
        
#endregion
    }
}