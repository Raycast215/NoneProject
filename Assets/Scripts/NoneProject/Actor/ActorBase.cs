using UnityEngine;

namespace NoneProject.Actor
{
    // Scripted by Raycast
    // 2025.01.14
    // Actor의 상위 클래스입니다.
    public abstract class ActorBase : MonoBehaviour
    {
        protected SPUM_Prefabs Model;
        protected Rigidbody2D Rigidbody2D;

        public abstract void Initialized();
        
        protected void LoadRigidBody2D()
        {
            if (TryGetComponent<Rigidbody2D>(out var rBody2D))
            {
                Rigidbody2D = rBody2D;
            }
            else
            {
                Debug.LogError("Rigidbody2D Component is null...");
            }
        }
    }
}