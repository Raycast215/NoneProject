using System;
using TMPro;
using UnityEngine;

namespace NoneProject.UI.StageTimer
{
    // Scripted by Raycast
    // 2025.02.03
    // InGame에서 게임의 진행 시간을 표시하는 UI를 담은 클래스입니다.
    [Serializable]
    public class StageTimerUI
    {
        private const int Offset = 60;
        
        [SerializeField] private TextMeshProUGUI timeText;

        public void SetTime(float time)
        {
            var min = (int)(time / Offset);
            var sec = (int)(time - Offset * min);
            
            timeText.text = $"{min:D2}:{sec:D2}";
        }
    }
}