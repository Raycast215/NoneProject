using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NoneProject.UI.EnemyCount
{
    // Scripted by Raycast
    // 2025.01.25
    // 활성화 된 Enemy 수를 UI에 표시하는 UI 클래스입니다.
    [Serializable]
    public class EnemyCountUI
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image icon;

#region UI Option

        [SerializeField] private float defaultScale = 1.0f;
        [SerializeField] private float maxScale = 1.2f;
        [SerializeField] private float duration = 0.1f;

#endregion

        private Sequence _sequence;
        private bool _isInitialized;
        
        public void SetCount(int count)
        {
            countText.text = $"{count}";
        }

        public void MoveIcon()
        {
            if (_isInitialized)
            {
                _sequence.Restart();
                return;
            }
            
            _sequence = DOTween.Sequence();
            _sequence.SetAutoKill(false);
            _sequence.Append(icon.rectTransform.DOScale(maxScale, duration));
            _sequence.Append(icon.rectTransform.DOScale(defaultScale, duration));
            
            _isInitialized = true;
        }
    }
}