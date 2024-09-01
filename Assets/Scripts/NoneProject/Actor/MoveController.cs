using Template.Utility;
using UnityEngine;

namespace NoneProject.Actor
{
    // Scripted by Raycast
    // 2024.09.01
    // Actor의 이동로직을 관리하는 클래스입니다.
    
    public abstract class MoveController : MonoBehaviour
    {
        private Transform _transform;
        private Vector3 _scale;

        public void Initialized(Transform tr)
        {
            _transform = tr;
            _scale = _transform.localScale;
        }

        protected void SetDirection(Vector2 dirVec)
        {
            var direction = Util.GetToggleOne(dirVec.x <= 0);

            _transform.localScale = new Vector3(_scale.x * direction, _scale.y, _scale.z);
        }
        
        protected bool CheckDistance(Vector2 fromPos, Vector2 toPos, float distance)
        {
            return Vector2.Distance(fromPos, toPos) < distance;
        }

        public abstract void UpdateMove(Vector2 dirVec);
        protected abstract void Move(Vector2 dirVec);
    }
}