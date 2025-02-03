using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Data;
using NoneProject.ScriptableData;
using Sirenix.Utilities;
using Template.Manager;

namespace NoneProject.Actor.Enemy
{
    public class EnemyStatManager
    {
        public bool IsLoaded { get; private set; }
        
        private readonly Dictionary<string, EnemyStatData> _statDic = new Dictionary<string, EnemyStatData>();
        
        public EnemyStatManager()
        {
            LoadData();
        }

        public EnemyStatData GetData(string id)
        {
            return _statDic.FirstOrDefault(x => x.Key.Equals(id)).Value;
        }
        
        private void LoadData()
        {
            const string assetName = nameof(EnemyStatScriptableData);

            AddressableManager.Instance.LoadAsset<EnemyStatScriptableData>(assetName,  OnComplete).Forget();
            return;

            void OnComplete(EnemyStatScriptableData so)
            {
                so.data.ForEach(stat => _statDic.Add(stat.dataBaseStatData.id, stat));
                IsLoaded = true;
            }
        }
    }
}