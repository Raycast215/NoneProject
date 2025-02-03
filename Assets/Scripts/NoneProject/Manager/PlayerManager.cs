using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private PlayerStatManager _statManager;
        
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
                // Player Stat Data 변경하는 이벤트 구독.
                Player.OnStatUpdated += _statManager.GetData;
                
                isInitialized = true;
            }
        }

#region Override Methods
        
        protected override async void Initialized()
        {
            base.Initialized();

            // GameManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);
            
            _statManager = new PlayerStatManager();
            
            // Stat Data 로드 완료까지 대기.
            await UniTask.WaitUntil(() => _statManager.IsLoaded, cancellationToken: _cts.Token);
            
            Load();
        }
        
        protected override void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
         
            base.OnDestroy();
        }
        
#endregion
    }
}