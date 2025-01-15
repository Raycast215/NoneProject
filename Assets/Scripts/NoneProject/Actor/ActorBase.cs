using UnityEngine;

namespace NoneProject.Actor
{
    // Scripted by Raycast
    // 2025.01.14
    // Actor의 상위 클래스입니다.
    public abstract class ActorBase : MonoBehaviour
    {
        public bool IsLoaded { get; protected set; }
        
        protected SPUM_Prefabs Model;
        protected Rigidbody2D Rigidbody2D;

        public virtual void Initialized()
        {
            LoadRigidBody2D();
            LoadModel();
        }
        
        private void LoadRigidBody2D()
        {
            if (TryGetComponent<Rigidbody2D>(out var rBody2D))
            {
                Rigidbody2D ??= rBody2D;
            }
            else
            {
                Debug.LogError("Rigidbody2D Component is null...");
            }
        }
        
        private void LoadModel()
        {
            if (TryGetComponent<SPUM_Prefabs>(out var model))
            {
                Model ??= model;
            }
            else
            {
                Debug.LogError("Model Component is null...");
            }
        }

        public abstract void Move(float moveSpeed = 1.0f, Vector2 moveVec = new Vector2());
    }
}