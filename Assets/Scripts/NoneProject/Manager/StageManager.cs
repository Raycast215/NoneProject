using System.Collections.Generic;
using NoneProject.ScriptableData;
using NoneProject.Stage.Data;
using Sirenix.Utilities;
using Template.Manager;

namespace NoneProject.Manager
{
    // Scripted by Raycast
    // 2025.02.04
    // Stage를 관리하는 매니저 클래스입니다.
    public class StageManager : SingletonBase<StageManager>
    {
        private readonly Dictionary<string, StageData> _stageDataDic = new Dictionary<string, StageData>();

        public async void LoadData()
        {
            const string assetName = nameof(StageScriptableData);
            
            await AddressableManager.Instance.LoadAsset<StageScriptableData>(assetName, OnComplete);
            return;

            void OnComplete(StageScriptableData asset)
            {
                asset.datas.ForEach(stageData => _stageDataDic.Add(stageData.id, stageData));
                isInitialized = true;
            }
        }

        public StageData GetStage(int index)
        {
            var stageID = $"Stage_{index:D2}";

            return _stageDataDic.GetValueOrDefault(stageID);
        }

        public int CheckStageIndex(int index)
        {
            return index >= _stageDataDic.Count
                ? _stageDataDic.Count // 마지막 인덱스의 스테이지를 반복하기 위함.
                : index;              // 해당 인덱스를 반환.
        }
    }
}