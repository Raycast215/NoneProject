using System;
using NoneProject.Actor.Player;
using NoneProject.Common;
using Template.Manager;
using UnityEngine;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.01.15
    // Player 생성하고 관리하는 클래스입니다.
    public class PlayerManager : SingletonBase<PlayerManager>
    {
        public PlayerController Player { get; private set; }

        private async void Load(Action<PlayerController> onComplete = null)
        {
            // Player의 에셋을 로드.
            await AddressableManager.Instance.LoadAssetsLabel<GameObject>(AddressableLabel.Player, OnLoadComplete);
            return;
            
            void OnLoadComplete(GameObject prefab)
            {
                // 오브젝트 생성.
                Player = Instantiate(prefab, transform).GetComponent<PlayerController>();
                // Player 생성 후 실행할 이벤트 실행.
                onComplete?.Invoke(Player);
                
                isInitialized = true;
            }
        }

#region Override Methods
        
        protected override void Initialized()
        {
            base.Initialized();

            Load();
        }
        
#endregion
    }
}