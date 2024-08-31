
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Template.Manager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace NoneProject.Manager
{
    public class AddressableManager : SingletonBase<AddressableManager>
    {
        [SerializeField] private List<string> assetNameList;
        
        private Dictionary<string, GameObject> _assetDic;

        public void LoadAsset<T>(string assetName, Action<T> onComplete)
        {
            _assetDic ??= new Dictionary<string, GameObject>();
            
            if (_assetDic.ContainsKey(assetName))
            {
                var asset = _assetDic[assetName].GetComponent<T>();
                onComplete?.Invoke(asset);
                return;
            }
            
            Addressables.LoadAssetAsync<GameObject>(assetName).Completed += handler =>
            {
                var asset = handler.Result.GetComponent<T>();
                onComplete?.Invoke(asset);
                _assetDic.Add(assetName, handler.Result);
                assetNameList.Add(assetName);
            };
        }

        public async void Load<T>(string assetName, Action<T> onComplete)
        {
            var handle = Addressables.LoadAssetAsync<T>(assetName);

            await UniTask.WaitUntil(() => handle.IsDone);
            
            onComplete?.Invoke(handle.Result);
            
            Addressables.Release(handle);
        }

#region Override Implementation
        
        protected override void Initialized()
        {
            assetNameList ??= new List<string>();
            _assetDic ??= new Dictionary<string, GameObject>();
            
            IsInitialized = true;
        }
        
#endregion
    }
}