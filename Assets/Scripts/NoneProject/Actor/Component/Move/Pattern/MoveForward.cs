using System;
using NoneProject.Interface;
using UnityEngine;

namespace NoneProject.Actor.Component.Move.Pattern
{
    // Scripted by Raycast
    // 2025.01.17
    // 지정된 방향으로 이동하는 클래스입니다.
    public class MoveForward
    {
        public event Action<Vector2> OnMoveFinished;
        
        private readonly Rigidbody2D _rigidbody2D;
        private float _moveSpeed;

        public MoveForward(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        
        public void Move(float moveSpeed)
        {
            
        }
    }
}