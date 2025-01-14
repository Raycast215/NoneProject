using Cysharp.Threading.Tasks;
using NoneProject.Common;
using NoneProject.Data;
using NoneProject.GameSystem;
using Template.Manager;

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
        }

        public void SetInGame(InGame inGame)
        {
            InGame = inGame;
        }
        
        private void LoadConstData()
        {
            AddressableManager.Instance.LoadAssetsLabel<ConstData>(AddressableLabel.Common, asset =>
            {
                Const = asset;
                isInitialized = true;
            }).Forget();
        }
    }
}