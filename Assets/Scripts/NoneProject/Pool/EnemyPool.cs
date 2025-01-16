using NoneProject.Actor.Enemy;
using NoneProject.Manager;
using Template.Pool;
using UnityEngine;

namespace NoneProject.Pool
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy Pool의 오브젝트를 포함하는 클래스.
    public class EnemyPool : PoolBase<EnemyController>
    {
#region Override Methods

        protected override void Release()
        {
            if (Controller is null)
                return;
            
            // 위치 초기화.
            Controller.SetPosition(Vector2.zero);
            // Pool 해제.
            EnemyManager.Instance.Release(this);
            // Controller 초기화.
            Controller = null;
        }
        
#endregion
    }
}