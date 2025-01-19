using System.Collections.Generic;
using NoneProject.Actor.Data;
using NoneProject.Actor.Stat;
using NoneProject.Common;
using Template.Manager;

namespace NoneProject.Manager
{
    public class DataManager : SingletonBase<DataManager>
    {
        private readonly Dictionary<string, ActorStat> _actorStatDic = new Dictionary<string, ActorStat>();

        public async void LoadData()
        {
            await AddressableManager.Instance.LoadAssetsLabel<StatBase>(AddressableLabel.Stat, OnComplete);
            return;

            void OnComplete(StatBase asset)
            {
                foreach (var actorStat in asset.stats)
                {
                    _actorStatDic.Add(actorStat.id, actorStat);
                }
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