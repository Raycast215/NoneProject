using NoneProject.Manager;
using UnityEngine;

namespace NoneProject.Actor.Component.Attack
{
    public class AttackForward
    {
        private Transform _transform;
        
        public AttackForward(Transform transform)
        {
            _transform = transform;
        }

        public async void Attack(string projectileID, Vector2 casterPos, Vector2 startPos)
        {
            var projectile = await ProjectileManager.Instance.Get(projectileID);
            
            projectile.SetPosition(startPos);
            projectile.SetAngle(casterPos);
        }
    }
}


