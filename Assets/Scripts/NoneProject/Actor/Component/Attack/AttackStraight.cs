using System;
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using UnityEngine;

namespace NoneProject.Actor.Component.Attack
{
    public sealed class AttackStraight : AttackBase
    {
        private Vector2 _moveVec;
        private Vector2 _startPos;
        
        public AttackStraight(Transform caster)
        {
            Caster = caster;
        }

        public void SetStartPos(Vector2 startPos)
        {
            _startPos = startPos;
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
                projectile.Set(ID, MovePattern.Forward, _startPos, _moveVec);
                
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