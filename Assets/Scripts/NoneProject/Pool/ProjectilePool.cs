using NoneProject.Actor.Projectile;
using NoneProject.Manager;
using Template.Pool;
using UnityEngine;

namespace NoneProject.Pool
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile Pool의 오브젝트를 포함하는 클래스.
    public class ProjectilePool : PoolBase<ProjectileController>
    {
#region Override Methods

        protected override void Release()
        {
            if (Controller is null)
                return;
            
            // Pool 해제.
            ProjectileManager.Instance.Release(this);
            // Controller 초기화.
            Controller = null;
        }
        
#endregion
    }
}