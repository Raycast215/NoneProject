using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoneProject.TestModule
{
    // Scripted by Raycast
    // 2025.01.17
    // 인스펙터에서 오브젝트 풀을 직접 해제하기 위한 클래스입니다. 
    public class ObjectPoolReleaseModule : MonoBehaviour
    {
        public event Action OnReleased;
        
        [HorizontalGroup("Test Module : ObjectPool Release")]
        [Button("Release")]
        private void Release()
        {
            if (Application.isPlaying is false)
                return;
            
            OnReleased?.Invoke();
        }
    }
}