
using System;
using Template.Manager;
using UnityEngine;


namespace NoneProject.Manager
{
    public class UIManager : SingletonBase<UIManager>
    {
        private Transform _canvas;
        
        
        public void Get<T>(Action<T> onComplete = null)
        {
            _canvas ??= FindObjectOfType<Canvas>().transform;
            
            var uiName = typeof(T).FullName;
            // var ui = AddressableManager.Instance.LoadAsset(uiName, onComplete);
            // ui.transform.SetParent(_canvas);
            // ui.transform.position = Vector3.zero;
        }
        
        protected override void Initialized()
        {
            IsInitialized = true;
        }
    }
}