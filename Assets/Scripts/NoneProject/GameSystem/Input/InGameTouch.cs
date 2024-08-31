using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NoneProject.GameSystem.Input
{
    // Scripted by Raycast
    // 2024.09.01
    // InGame에서 Player의 이동에 필요한 Touch를 관리하는 클래스입니다.
    
    public class InGameTouch
    {
        public event Action<Vector2> OnTouched = delegate {  };
        
        private readonly GraphicRaycaster _graphicRaycaster;
        private readonly EventSystem _eventSystem;
        
        private Vector2 _startTouchPos;
        private Vector2 _curTouchPos;
        private Vector2 _movePos;

        public InGameTouch(GraphicRaycaster caster, EventSystem eventSystem)
        {
            _graphicRaycaster = caster;
            _eventSystem = eventSystem;
        }
        
        public void UpdateTouch()
        {
            if (UnityEngine.Input.touchCount is not 1) 
                return;
            
            var touch = UnityEngine.Input.GetTouch(0);

            if (IsPointerOverUI(touch.position))
            {
                OnTouched?.Invoke(Vector2.zero);
                return;
            }

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
        
        // UI에 닿았을 경우 Touch 로직을 멈추기 위한 함수.
        private bool IsPointerOverUI(Vector2 touchPosition)
        {
            var results = new List<RaycastResult>();
            var pointerEventData = new PointerEventData(_eventSystem)
            {
                position = touchPosition
            };
            
            _graphicRaycaster.Raycast(pointerEventData, results);

            return results.Count > 0;
        }
    }
}