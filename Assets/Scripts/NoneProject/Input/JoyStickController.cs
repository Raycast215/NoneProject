using System;
using UnityEngine;

namespace NoneProject.Input
{
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