using UnityEngine;

namespace NoneProject.Actor
{
    // Scripted by Raycast
    // 2025.01.14
    // Actor의 상위 클래스입니다.
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ActorBase : MonoBehaviour
    {
        public bool IsInitialized { get; protected set; }
        
        protected Rigidbody2D Rigidbody2D;
        protected float MoveSpeed;

        private void Start()
        {
            LoadRigidBody2D();
            Initialize();
        }
        
        private void LoadRigidBody2D()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        protected abstract void Initialize();
        protected abstract void Subscribe();
    }
}