using System;
using UnityEngine;

namespace NoneProject.Input
{
    // Scripted by Raycast
    // 2025.01.24
    // 이동을 위한 JoyStick을 관리하는 클래스입니다.
    public class JoyStickController : MonoBehaviour
    {
        public event Action<Vector2> OnMoveVectorUpdated; 

        private FloatingJoystick _joyStick;

        private void Start()
        {
            _joyStick = GetComponentInChildren<FloatingJoystick>();
        }

        public void UpdateController()
        {
            OnMoveVectorUpdated?.Invoke(_joyStick.Direction);
        }
    }
}