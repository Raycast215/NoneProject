using System.Threading;
using Template.Utility;
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

        protected CancellationTokenSource Cts = new CancellationTokenSource();
        protected Rigidbody2D Rigidbody2D;

        private void Start()
        {
            LoadRigidBody2D();
            Initialize();
        }
        
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
        
        protected void SetScaleDirection(Vector2 dirVec)
        {
            var direction = Util.GetToggleOne(dirVec.x <= 0);
            var tr = Rigidbody2D.transform;
            var scale = tr.localScale;
            
            tr.localScale = new Vector3(Mathf.Abs(scale.x) * direction, scale.y, scale.z);
        }
        
        private void LoadRigidBody2D()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        protected abstract void Initialize();
    }
}