using NoneProject.Manager;
using NoneProject.Ui;
using UnityEngine;

namespace NoneProject.UI.EnemyCount
{
    // Scripted by Raycast
    // 2025.01.25
    // InGame에서 활성화 된 Enemy 수를 UI에 표시하는 UI 오브젝트 클래스입니다.
    public class EnemyCountViewer : UiObject
    {
        [SerializeField] private EnemyCountUI ui;
        
        protected override void Start()
        {
            base.Start();
            ui.SetCount(0);
        }

#region Override Method
        
        public override void Subscribe()
        {
            EnemyManager.Instance.OnEnemyCountUpdated += ui.SetCount;
            EnemyManager.Instance.OnEnemyCountUpdated += _ => ui.PlayAnimation();
        }

        protected override void RegisterUi()
        {
            UIManager.Instance.Add(nameof(EnemyCountViewer), this);
        }
        
 #endregion
    }
}