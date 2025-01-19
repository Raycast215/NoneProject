using System;
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using UnityEngine;

namespace NoneProject.Actor.Component.Attack
{
    public class AttackStraight : AttackBase
    {
        private Vector2 _moveVec;
        
        public AttackStraight(Transform caster)
        {
            Caster = caster;
        }

        public void SetMoveVec(Vector2 moveVec)
        {
            _moveVec = moveVec;
        }
        
#region Override Methods
        
        protected override async void SetProjectile(int count, float delay, Action onFinished)
        {
            foreach (var projectile in ProjectileList)
            {
                projectile.gameObject.SetActive(true);
                projectile.Set(MovePattern.Forward, (Vector2)Caster.position + _moveVec, Caster);
                
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