using Cysharp.Threading.Tasks;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Actor.Component.Attack
{
    public class AttackStraightFormAngle : AttackBase
    {
        private const float MaxAngle = 360.0f;
        
        public AttackStraightFormAngle(Transform caster)
        {
            Caster = caster;
        }

#region Override Methods
        
        protected override async void SetProjectile(int count, float delay)
        {
            var minAngle = MaxAngle / count;
            
            for (var i = 0; i < count; i++)
            {
                var projectile = ProjectileList[i];
                var toStartPos = (Vector2)Caster.position + Util.GetVectorFromAngle(minAngle * i);
                
                projectile.gameObject.SetActive(true);
                projectile.Set(toStartPos, Caster);
                
                if (delay != 0.0f)
                {
                    await UniTask.WaitForSeconds(delay, cancellationToken: Cts.Token);
                }
            }
        }
        
#endregion
    }
}