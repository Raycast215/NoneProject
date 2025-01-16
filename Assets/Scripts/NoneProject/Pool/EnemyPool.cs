using NoneProject.Actor.Enemy;
using NoneProject.Manager;
using Sirenix.OdinInspector;
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
        
        public override void SetController(EnemyController controller)
        {
            // Enemy 오브젝트 저장.
            Controller = controller;
            // Enemy 오브젝트를 Pool 오브젝트 하위로 이동.
            SetControllerParent(transform);
        }

        protected override void Release()
        {
            // 위치 초기화.
            Controller.SetPosition(Vector2.zero);
            // Pool 해제.
            EnemyManager.Instance.Release(this);
            // Controller 초기화.
            Controller = null;
        }
        
#endregion

#region Test Option

        [HorizontalGroup("Test")]
        [Button("Pool Release")]
        private void ReleaseButton()
        {
            if (Application.isPlaying is false)
                return;
                    
            EnemyManager.Instance.Release(this);
        }

#endregion
    }
}