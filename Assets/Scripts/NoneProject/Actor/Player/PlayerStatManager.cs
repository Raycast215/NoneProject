using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Data;
using NoneProject.ScriptableData;
using Sirenix.Utilities;
using Template.Manager;

namespace NoneProject.Actor.Player
{
    public class PlayerStatManager
    {
        public bool IsLoaded { get; private set; }
        
        private readonly Dictionary<string, PlayerStatData> _statDic = new Dictionary<string, PlayerStatData>();

        public PlayerStatManager()
        {
            LoadData();
        }
        
        public PlayerStatData GetData(string id)
        {
            return _statDic.FirstOrDefault(x => x.Key.Equals(id)).Value;
        }
        
        private void LoadData()
        {
            const string assetName = nameof(PlayerStatScriptableData);

            AddressableManager.Instance.LoadAsset<PlayerStatScriptableData>(assetName,  OnComplete).Forget();
            return;

            void OnComplete(PlayerStatScriptableData so)
            {
                so.data.ForEach(stat => _statDic.Add(stat.dataBaseStatData.id, stat));
                IsLoaded = true;
            }
        }
    }
}