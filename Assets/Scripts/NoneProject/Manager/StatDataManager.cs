using System.Collections.Generic;
using System.Linq;
using NoneProject.Actor.Data;
using NoneProject.Actor.Enemy;
using Sirenix.OdinInspector;
using Template.Manager;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.01.25
    // Data를 관리하는 매니저 클래스입니다.
    public class StatDataManager : SingletonBase<StatDataManager>
    {
        private const string EnemyStat = "EnemyStat";
        private const string PlayerStat = "PlayerStat";
        
        [ShowInInspector]
        private readonly Dictionary<string, ActorStatData> _playerStatDic = new Dictionary<string, ActorStatData>();
        [ShowInInspector]
        private readonly Dictionary<string, EnemyStatBase> _enemyStatDic = new Dictionary<string, EnemyStatBase>();

        public void LoadData()
        {
            LoadEnemyData();
            LoadPlayerData();
        }
        
        public ActorStatData GetPlayerStatData(string playerID)
        {
            return _playerStatDic.FirstOrDefault(x => x.Key.Equals(playerID)).Value;
        }

        public EnemyStatBase GetEnemyStatData(string enemyID)
        {
            return _enemyStatDic.FirstOrDefault(x => x.Key.Equals(enemyID)).Value;
        }

        private async void LoadEnemyData()
        {
            await AddressableManager.Instance.LoadAsset<EnemyStatSo>(EnemyStat, OnComplete);
            return;
            
            void OnComplete(EnemyStatSo so)
            {
                foreach (var data in so.data)
                {
                    _enemyStatDic.Add(data.baseStat.id, data);
                }
            }
        }

        private async void LoadPlayerData()
        {
            await AddressableManager.Instance.LoadAsset<PlayerStatSo>(PlayerStat, OnComplete);
            return;
            
            void OnComplete(PlayerStatSo asset)
            {
                foreach (var actorStat in asset.data)
                {
                    _playerStatDic.Add(actorStat.id, actorStat);
                }
                
                isInitialized = true;
            }
        }
    }
}