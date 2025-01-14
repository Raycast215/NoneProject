using DG.Tweening;
using UnityEngine;

namespace NoneProject.UI.AutoPlay
{
    public class AutoPlayPoint : MonoBehaviour
    {
        private const float DurationTime = 1.0f;
        private const Ease AlphaEase = Ease.Linear;

        [SerializeField] private Transform iconTransform;
        [SerializeField] private Transform shadowTransform;
        
        private Sequence _tween;

        private void OnEnable()
        {
            _tween?.Restart();
        }

        private void OnDisable()
        {
            _tween?.Rewind();
        }

        public void Start()
        {
            Initialized();
        }

        public void SetPosition(Vector2 pos)
        {
            transform.position = pos;
        }
        
        private void Initialized()
        {
            _tween = DOTween.Sequence();
            _tween.Append(iconTransform.DOLocalMoveY(0.3f, DurationTime).SetEase(AlphaEase));
            _tween.Join(shadowTransform.DOScaleX(0.05f, DurationTime).SetEase(AlphaEase));
            _tween.Append(iconTransform.DOLocalMoveY(0.5f, DurationTime).SetEase(AlphaEase));
            _tween.Join(shadowTransform.DOScaleX(0.03f, DurationTime).SetEase(AlphaEase));
            _tween.SetLoops(-1);
        }
    }
}