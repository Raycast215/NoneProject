using System;
using NoneProject.Interface;
using UnityEngine;

namespace NoneProject.Actor.Move
{
    // Scripted by Raycast
    // 2025.01.15
    // 기본 이동 패턴 클래스입니다.
    public class DefaultMove : IMovable
    {
        public event Action OnMoveFinished;

        private readonly Rigidbody2D _rigidbody2D;
        private float _moveSpeed;

        public DefaultMove(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }

        public void Subscribe(Action onMoveFinished)
        {
            OnMoveFinished += onMoveFinished;
        }
        
        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        
        public void Move(float moveSpeed, Vector2 moveVec = new Vector2())
        {
            // 움직일 거리 계산.
            var moveDir = (Vector3)moveVec * (_moveSpeed * Time.deltaTime);
            // 실제 이동할 위치값.
            var movePos = _rigidbody2D.transform.position + moveDir;
            
            // 이동 실행.
            _rigidbody2D.MovePosition(movePos);
            // 이동 후 실행항 이벤트 실행.
            OnMoveFinished?.Invoke();
        }
    }
}