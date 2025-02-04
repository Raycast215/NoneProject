using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Manager;
using NoneProject.Stage.Data;
using Sirenix.Utilities;
using Template.Manager;
using Template.Utility;

namespace NoneProject.Stage
{
    // Scripted by Raycast
    // 2025.02.04
    // Stage를 실행하는 로직을 담당하는 클래스입니다.
    public class StageController : IDisposable
    {
        public event Action<string> OnEnemySpawned; 
        
        public StageData CurrentStage { get; private set; }
        
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly WeightRandomPicker<EnemySpawnData> _randomPicker = new WeightRandomPicker<EnemySpawnData>();

        public async UniTaskVoid StartStage()
        {
            _cts ??= new CancellationTokenSource();
            
            // BGM 재생.
            SoundManager.Instance.PlayBgm(CurrentStage.bgmId, isFade: true);
            
            // 스폰할 수량 정의.
            var spawnCount = CurrentStage.maxSpawnCount;
            
            while (spawnCount > 0)
            {
                // 가중치에 의한 스폰 데이터를 가져옴.
                var spawnData = _randomPicker.Get();
                
                for (var i = 0; i < spawnData.spawnCount; i++)
                {
                    OnEnemySpawned?.Invoke(spawnData.enemyId);
                    spawnCount--;
                }
                
                // 스폰 간격에 따라 대기.
                await UniTask.WaitForSeconds(CurrentStage.intervalTime, cancellationToken: _cts.Token);
                
                if(_cts is null || _cts.IsCancellationRequested)
                    return;
            }
        }

        public void ChangeStageIndex(int index)
        {
            var stageIndex = StageManager.Instance.CheckStageIndex(index);
            
            // 인덱스에 해당하는 스테이지 데이터 가져옴.
            CurrentStage = StageManager.Instance.GetStage(stageIndex);
            // 랜덤 가중치 초기화.
            _randomPicker.Clear();
            // 랜덤 가중치 등록.
            CurrentStage.datas.ForEach(data => _randomPicker.Add(data, data.weight));
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}