using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Data;
using NoneProject.ScriptableData;
using Sirenix.Utilities;
using Template.Manager;

namespace NoneProject.Actor.Projectile
{
    public class ProjectileStatManager
    {
        public bool IsLoaded { get; private set; }
        
        private readonly Dictionary<string, ProjectileStatData> _statDic = new Dictionary<string, ProjectileStatData>();

        public ProjectileStatManager()
        {
            LoadData();
        }
        
        public ProjectileStatData GetData(string id)
        {
            return _statDic.FirstOrDefault(x => x.Key.Equals(id)).Value;
        }
        
        private void LoadData()
        {
            const string assetName = nameof(ProjectileStatScriptableData);

            AddressableManager.Instance.LoadAsset<ProjectileStatScriptableData>(assetName,  OnComplete).Forget();
            return;

            void OnComplete(ProjectileStatScriptableData so)
            {
                so.data.ForEach(stat => _statDic.Add(stat.id, stat));
                IsLoaded = true;
            }
        }
    }
}