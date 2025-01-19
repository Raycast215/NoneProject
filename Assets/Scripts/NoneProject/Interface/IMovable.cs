using System;
using UnityEngine;

namespace NoneProject.Interface
{
    // Scripted by Raycast
    // 2025.01.15
    // 이동 로직에서 사용하는 인터페이스.
    public interface IMovable
    {
        public void Move(float moveSpeed);
        public void SetMoveVec(Vector2 moveVec);
        public void CompleteMove(Action<Vector2> callback);
    }
}