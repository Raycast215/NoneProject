using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NoneProject.Actor.Component.Attack
{
    public class AttackStraight : AttackBase
    {
        public AttackStraight(Transform caster)
        {
            Caster = caster;
        }
        
#region Override Methods
        
        protected override async void SetProjectile(int count, float delay)
        {
            for (var i = 0; i < count; i++)
            {
                var projectile = ProjectileList[i];
                
                projectile.gameObject.SetActive(true);
                projectile.Set(Caster.position, Caster);
                
                if (delay != 0.0f)
                {
                    await UniTask.WaitForSeconds(delay, cancellationToken: Cts.Token);
                }
            }
        }
        
#endregion
    }
}