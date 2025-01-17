using NoneProject.Interface;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Actor.Component.Move
{
    // Scripted by Raycast
    // 2025.01.14
    // 이동로직을 처리하는 상위 클래스입니다.
    public abstract class MoveController : IMovable
    {
        protected Rigidbody2D Rigidbody;

        public void SetPosition(Vector2 position)
        {
            Rigidbody.transform.localPosition = position;
        }
        
#region Override Methods
        
        protected void SetDirection(Vector2 dirVec)
        {
            var direction = Util.GetToggleOne(dirVec.x <= 0);
            var transform = Rigidbody.transform;
            var scale = transform.localScale;
            
            transform.localScale = new Vector3(Mathf.Abs(scale.x) * direction, scale.y, scale.z);
        }
        
#endregion

        public abstract void Move(float moveSpeed, Vector2 moveVec);
        public abstract void Subscribe();
    }
}