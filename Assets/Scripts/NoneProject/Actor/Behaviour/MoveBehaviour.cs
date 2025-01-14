using UnityEngine;

namespace NoneProject.Actor.Behaviour
{
    // Scripted by Raycast
    // 2025.01.14
    // 이동로직을 처리하는 상위 클래스입니다.
    public abstract class MoveBehaviour
    {
        protected Rigidbody2D Rigidbody;

        public abstract void Move();
    }
}
