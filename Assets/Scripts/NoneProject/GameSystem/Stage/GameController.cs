
using System.Threading;
using NoneProject.Common;
using NoneProject.GameSystem.Stage.UI;
using NoneProject.Manager;
using UnityEngine;


namespace NoneProject.GameSystem.Stage
{
    public class GameController : MonoBehaviour
    {
        private Transform _playerHolder;
        private Transform _enemyHolder;
        private Transform _mapHolder;
        private Data.Stage _testStageData;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        
        private bool _isRun;
        
        private void Start()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            if (_isRun is false)
                return;
            
            ActorListHolder.EnemyList.ForEach(x => x.NodeRunner.OperateNode());
            ActorListHolder.MapList.ForEach(x => x.NodeRunner.OperateNode());
        }
        
        private void Initialize()
        {
            Clear();
            
            InitStateData();
            InitUi();
            InitHolder();
            InitPlayer();
            //InitMap();
            
            //DataManager.Instance.Load();
            
            _isRun = true;
        }
        
        private void InitStateData()
        {
            _testStageData = new Data.Stage
            {
                index = 0,
                mapType = $"{MapType.Forest}",
                spawnCounts = new int[] { 3, 3, 4 },
                bossKey = null,
                rewardGold = 10
            };
        }

        private void InitUi()
        {
            //UIManager.Instance.Get<UiStage>(ui => ui.Init());
        }
        
        private void InitHolder()
        {
            // Player Holder Setting.
            _playerHolder = new GameObject(Define.PlayerHolder).transform;
            _playerHolder.SetParent(transform);
            _playerHolder.transform.position = new Vector3(0.0f, Define.HolderOffset, 0.0f);

            // Enemy Holder Setting.
            _enemyHolder = new GameObject(Define.EnemyHolder).transform;
            _enemyHolder.SetParent(transform);
            _enemyHolder.transform.position = new Vector3(0.0f, Define.HolderOffset, 0.0f);
            
            // Map Holder Setting.
            _mapHolder = new GameObject(Define.MapHolder).transform;
            _mapHolder.SetParent(transform);
            _mapHolder.transform.position = new Vector3(0.0f, Define.HolderOffset, 0.0f);
        }
        
        private async void InitMap()
        {
            //var map = await ActorCreator.CreateMap(_testStageData.mapType, _mapHolder);
            //map.Set(_testStageData, _enemyHolder);
           // map.NodeRunner.SetRunState(true);
        }
        
        private async void InitPlayer()
        {
           // var player = await ActorCreator.CreatePlayer("Player_Normal_Magic", _playerHolder);
            // player.NodeRunner.SetRunState(true);
        }

        private void Clear()
        {
            _isRun = false;
            
            ActorListHolder.ClearList(ActorListHolder.PlayerList);
            ActorListHolder.ClearList(ActorListHolder.EnemyList);
            ActorListHolder.ClearList(ActorListHolder.MapList);
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}