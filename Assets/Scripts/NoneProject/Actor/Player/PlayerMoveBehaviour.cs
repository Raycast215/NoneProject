using System;
using NoneProject.Actor.Behaviour;
using NoneProject.Actor.Player.Move;
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
        private readonly Transform _transform;
        private Vector2 _autoTargetPosition;

        public PlayerMoveBehaviour(Rigidbody2D rigidbody2D)
        {
            Rigidbody = rigidbody2D;
            _transform = Rigidbody.transform;
            _autoMove = new AutoMove(_transform);
            SetPosition(Vector2.zero);
        }

        public void Subscribed()
        {
            _autoMove.OnMoveVecUpdated += MoveDefault;
        }
        
        private void MoveDefault(Vector2 dirVec)
        {
            if (dirVec == Vector2.zero)
            {
                // 이동한 방향 값이 없는 경우 기본 애니메이션 재생.
                OnAnimationStateChanged?.Invoke(ActorState.Idle);
                return;
            }

            // 움직일 거리 계산.
            var moveDir = (Vector3)dirVec * (MoveSpeed * Time.deltaTime);
            // 실제 이동할 위치값.
            var movePos = _transform.position + moveDir;
            
            Rigidbody.MovePosition(movePos);
            SetDirection(dirVec);
            OnAnimationStateChanged?.Invoke(ActorState.Run);
        }

#region Override Methods
        
        public override void Move(Vector2 moveVec = new Vector2())
        {
            if (GameManager.Instance.InGame.IsAutoMove)
            {
                _autoMove.Move();
                return;
            }

            MoveDefault(moveVec);
        }
        
#endregion
    }
}