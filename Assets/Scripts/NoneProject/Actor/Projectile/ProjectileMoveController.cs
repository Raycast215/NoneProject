using NoneProject.Actor.Component.Move;
using UnityEngine;

namespace NoneProject.Actor.Projectile
{
    public class ProjectileMoveController : MoveController
    {
        //private readonly DefaultMove _defaultMove;

        public ProjectileMoveController(Rigidbody2D rigidbody2D)
        {
            Rigidbody = rigidbody2D;
        }

        public void SetAngle(Vector2 casterPos)
        {
            // 방향 벡터.
            var dir = (Vector2)Rigidbody.transform.position - casterPos;
            // 방향 벡터의 각도.
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            // 각도만큼 회전.
            Rigidbody.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

#region Override Methods
        
        public override void Move(float moveSpeed, Vector2 moveVec)
        {
            //_defaultMove.Move(moveSpeed, moveVec);
        }

        public override void Subscribe()
        {
            //_defaultMove.OnMoveFinished += SetDirection;
        }
        
#endregion
    }
}