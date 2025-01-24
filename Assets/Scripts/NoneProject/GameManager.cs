using Cysharp.Threading.Tasks;
using NoneProject.Common;
using NoneProject.Data;
using NoneProject.GameSystem;
using NoneProject.Manager;
using Template.Manager;
using UnityEngine;

namespace NoneProject
{
    public class GameManager : SingletonBase<GameManager>
    {
        public InGame InGame { get; private set; }
        
        public ConstData Const { get; private set; }

        protected override void Initialized()
        {
            base.Initialized();
            LoadConstData();
            StatDataManager.Instance.LoadData();
            Application.targetFrameRate = 120; 
        }

        public void SetInGame(InGame inGame)
        {
            InGame = inGame;
        }
        
        private void LoadConstData()
        {
            AddressableManager.Instance.LoadAssetsLabel<ConstData>(AddressableLabel.Common, OnComplete).Forget();
            return;

            void OnComplete(ConstData asset)
            {
                Const = asset;
                isInitialized = true;
            }
        }
    }
}