using System;
using DG.Tweening;
using UnityEngine;

namespace NoneProject.UI.AutoPlay
{
    // Scripted by Raycast
    // 2024.09.01
    // AutoPlay의 로직을 실행하는 클래스입니다.
    
    public class AutoPlay : MonoBehaviour
    {
        private const float AlphaDurationTime = 2.0f;
        private const Ease AlphaEase = Ease.Linear;
        
        public event Action<bool> OnAutoPlayUpdated = delegate {  }; 

        public bool IsAutoPlay { get; private set; }
        
        [SerializeField] private AutoPlayViewer viewer;

        private Sequence _textTween;

        private void Awake()
        {
            viewer.GetButton.onClick.AddListener(() => SetAutoPlay());
        }

        public void Start()
        {
            InitializedTween();
            SetAutoPlay(false);
        }

        private void InitializedTween()
        {
            _textTween = DOTween.Sequence();
            _textTween.Append(viewer.GetTmp.DOFade(0.0f, AlphaDurationTime).SetEase(AlphaEase));
            _textTween.Append(viewer.GetTmp.DOFade(1.0f, AlphaDurationTime).SetEase(AlphaEase));
            _textTween.SetLoops(-1);
        }
        
        private void SetAutoPlay(bool isInit = true)
        {
            if (isInit)
                IsAutoPlay = !IsAutoPlay;
            
            viewer.SetActiveIcon(IsAutoPlay);
            UpdateTextAlpha(IsAutoPlay);
            
            OnAutoPlayUpdated?.Invoke(IsAutoPlay);
        }

        private void UpdateTextAlpha(bool isActive)
        {
            if (isActive)
                _textTween.Restart();
            else
                _textTween.Rewind();
        }
    }
}