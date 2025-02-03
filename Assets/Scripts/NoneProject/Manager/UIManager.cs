using System.Collections.Generic;
using NoneProject.Ui;
using Template.Manager;
using UnityEngine;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.02.03
    // 사용할 UI에 접근하기 위한 매니저 클래스입니다.
    public class UIManager : SingletonBase<UIManager>
    {
        private readonly Dictionary<string, UiObject> _uiDic = new Dictionary<string, UiObject>();
        
        public T Get<T>(string uiID) where T : UiObject
        {
            return _uiDic.TryGetValue(uiID, out var ui) 
                ? ui.GetComponent<T>() 
                : default;
        }

        public void Add(string uiID, UiObject uiObject)
        {
            if (string.IsNullOrEmpty(uiID))
            {
                Debug.Log($"[UIManager] '{uiID}' is null or has an empty key...");
                return;
            }

            if (_uiDic.TryAdd(uiID, uiObject)) 
                return;
            
            Debug.Log($"[UIManager] '{uiID}' already contain key...");
        }
        
#region Override Method
        
        protected override void Initialized()
        {
            isInitialized = true;
        }
        
#endregion
    }
}