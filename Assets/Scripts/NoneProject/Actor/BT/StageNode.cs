
using System.Linq;
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using NoneProject.Data;
using NoneProject.GameSystem.Stage;
using Template.BehaviorTree.Common;
using UnityEngine;


namespace NoneProject.Actor.BT
{
    public class StageNode : ActionNode
    {
        [SerializeField] private float spawnDelay = 2.0f;

        private Stage _stageData; 
        private Transform _enemyHolder;
        
        private int _spawnCount;
        private int _spawnIndex;
        private int _totalCount;
        private bool _isNodeRun;
        
        public void Set(Stage toStageData, Transform parent)
        {
            _stageData = toStageData;
            _enemyHolder = parent;
            _totalCount = 0;
        }
        
        public override NodeState Evaluate()
        {
            if (_totalCount >= _stageData.spawnCounts.Sum())
                return nodeState = NodeState.Failure;
            
            if (_stageData.spawnCounts[_spawnIndex] > _spawnCount)
            {
                StageUpdate();
                return nodeState = NodeState.Running;
            }
            
            _spawnIndex++;
            _spawnCount = 0;
            return nodeState = NodeState.Running;
        }

        private async void StageUpdate()
        {
            if (_isNodeRun)
                return;
            
            _isNodeRun = true;
            
            var enemy = await ActorCreator.CreateEnemy("Enemy_Normal_00", _enemyHolder);
            var randomIndex = Random.Range(0, Define.PosYOffset.Length);
            enemy.Init(Define.PosYOffset[randomIndex]);
            enemy.NodeRunner.SetRunState(true);
            
            _spawnCount++;
            _totalCount++;

            if (Cts.IsCancellationRequested)
                return;
            
            await UniTask.WaitForSeconds(spawnDelay, cancellationToken: Cts.Token);
            
            _isNodeRun = false;
        }
        
        private void OnDestroy()
        {
            Cts?.Cancel();
            Cts?.Dispose();
        }
    }
}