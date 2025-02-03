using Cysharp.Threading.Tasks;
using NoneProject.Common;
using NoneProject.GameSystem;
using NoneProject.Manager;
using NoneProject.ScriptableData;
using Template.Manager;
using UnityEngine;

namespace NoneProject
{
    public class GameManager : SingletonBase<GameManager>
    {
        public InGame InGame { get; private set; }
        
        public ConstScriptableData Const { get; private set; }

        protected override void Initialized()
        {
            base.Initialized();
            LoadConstData();
            StageManager.Instance.LoadData();
            Application.targetFrameRate = 120; 
        }

        public void SetInGame(InGame inGame)
        {
            InGame = inGame;
        }
        
        private void LoadConstData()
        {
            AddressableManager.Instance.LoadAssetsLabel<ConstScriptableData>(AddressableLabel.Common, OnComplete).Forget();
            return;

            void OnComplete(ConstScriptableData asset)
            {
                Const = asset;
                isInitialized = true;
            }
        }
    }
}