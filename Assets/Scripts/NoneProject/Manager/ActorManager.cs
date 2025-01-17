using System;
using NoneProject.Actor.Player;
using NoneProject.Common;
using Template.Manager;
using UnityEngine;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.01.15
    // Player 및 Enemy을 생성하고 관리하는 클래스입니다.
    public class ActorManager : SingletonBase<ActorManager>
    {
        public PlayerController Player { get; private set; }

        public async void LoadPlayer(Transform playerHolder, Action<PlayerController> onComplete = null)
        {
            // Player의 에셋을 로드.
            await AddressableManager.Instance.LoadAssetsLabel<GameObject>(AddressableLabel.Player, OnLoadComplete);
            return;
            
            void OnLoadComplete(GameObject prefab)
            {
                // 오브젝트 생성.
                Player = Instantiate(prefab, playerHolder).GetComponent<PlayerController>();
                // Player 생성 후 실행할 이벤트 실행.
                onComplete?.Invoke(Player);
            }
        }

#region Override Methods
        
        protected override void Initialized()
        {
            base.Initialized();
            
            isInitialized = true;
        }
        
#endregion
    }
}