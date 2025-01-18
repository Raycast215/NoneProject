using UnityEngine;

namespace NoneProject.Actor.Component.Rotate
{
    public class RotateAngle
    {
        private readonly Transform _transform;
        
        public RotateAngle(Transform transform)
        {
            _transform = transform;
        }

        public void SetAngle(Vector2 targetVec)
        {
            // 방향 벡터.
            var dir = (Vector2)_transform.position - targetVec;
            // 방향 벡터의 각도.
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            // 각도만큼 회전.
            _transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}