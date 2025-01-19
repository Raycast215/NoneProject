using System;
using Cysharp.Threading.Tasks;
using NoneProject.Common;
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
        
        protected override async void SetProjectile(int count, float delay, Action onFinished)
        {
            var minAngle = MaxAngle / count;
            
            for (var i = 0; i < ProjectileList.Count; i++)
            {
                var projectile = ProjectileList[i];
                var toStartPos = (Vector2)Caster.position + Util.GetVectorFromAngle(minAngle * i);
                
                projectile.gameObject.SetActive(true);
                projectile.Set(MovePattern.Forward, toStartPos, Caster);
                
                if (delay > 0.0f)
                {
                    await UniTask.WaitForSeconds(delay, cancellationToken: Cts.Token);
                }
            }
            
            onFinished?.Invoke();
        }
        
#endregion
    }
}