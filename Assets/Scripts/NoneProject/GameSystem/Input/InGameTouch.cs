using System;
using UnityEngine;

namespace NoneProject.GameSystem.Input
{
    // Scripted by Raycast
    // 2024.09.01
    // InGame에서 Player의 이동에 필요한 Touch를 관리하는 클래스입니다.
    
    public class InGameTouch
    {
        public event Action<Vector2> OnTouched = delegate {  };
        
        private Vector2 _startTouchPos;
        private Vector2 _curTouchPos;
        private Vector2 _movePos;
        
        public void UpdateTouch()
        {
            if (UnityEngine.Input.touchCount is not 1) 
                return;
            
            var touch = UnityEngine.Input.GetTouch(0);
                
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPos = touch.position - touch.deltaPosition;
                    break;
                    
                case TouchPhase.Moved:
                    _curTouchPos = touch.position - touch.deltaPosition;
                    _movePos = (_curTouchPos - _startTouchPos).normalized;
                    _startTouchPos = touch.position - touch.deltaPosition;
                    break;

                case TouchPhase.Stationary:
                    _movePos = _movePos.normalized;
                    break;
                
                case TouchPhase.Ended:
                    _startTouchPos = Vector2.zero;
                    _curTouchPos = Vector2.zero;
                    _movePos = Vector2.zero;
                    break;
            }
            
            OnTouched?.Invoke(_movePos);
        }
    }
}