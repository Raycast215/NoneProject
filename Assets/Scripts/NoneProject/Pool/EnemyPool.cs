using NoneProject.Actor.Enemy;
using NoneProject.Manager;
using Sirenix.OdinInspector;
using Template.Pool;
using UnityEngine;

namespace NoneProject.Pool
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy Pool의 오브젝트가 되는 클래스.
    public class EnemyPool : PoolBase
    {
        public EnemyController Controller { get; private set; }
        
        public void SetController(EnemyController controller)
        {
            // Enemy 오브젝트 저장.
            Controller = controller;
            // Enemy 오브젝트를 Pool 오브젝트 하위로 이동.
            SetControllerParent(transform);
        }

        public void SetControllerParent(Transform parent, bool isActive = true)
        {
            // Enemy 오브젝트를 받아온 부모 하위로 이동.
            Controller.transform.SetParent(parent, false);
            // Controller 활성화 상태 변경.
            Controller.gameObject.SetActive(isActive);
        }
        
#region Override Methods
        
        protected override void Release()
        {
            Controller.SetPosition(Vector2.zero);
            // Pool 해제.
            EnemyManager.Instance.ReleaseEnemy(this);
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
                    
            EnemyManager.Instance.ReleaseEnemy(this);
        }

#endregion
    }
}