using UnityEngine;

namespace NoneProject.Interface
{
    // Scripted by Raycast
    // 2025.01.15
    // 이동함수 인터페이스.
    public interface IMovable
    {
        public void Move(Vector2 moveVec = new Vector2());
    }
}