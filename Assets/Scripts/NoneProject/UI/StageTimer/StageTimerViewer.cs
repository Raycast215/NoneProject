using NoneProject.Manager;
using NoneProject.Ui;
using UnityEngine;

namespace NoneProject.UI.StageTimer
{
    // Scripted by Raycast
    // 2025.02.03
    // InGame에서 게임의 진행 시간을 표시하는 UI 오브젝트 클래스입니다.
    public class StageTimerViewer : UiObject
    {
        [SerializeField] private StageTimerUI ui;

#region Override Method

        public override void Subscribe()
        {
            GameManager.Instance.InGame.StageTimer.OnTimeUpdated += ui.SetTime;
        }

        protected override void RegisterUi()
        {
            UIManager.Instance.Add(nameof(StageTimerViewer), this);
        }

#endregion
    }
}