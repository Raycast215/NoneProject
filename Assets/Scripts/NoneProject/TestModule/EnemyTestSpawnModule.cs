using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using NoneProject.Manager;
using Sirenix.OdinInspector;
using Template.Manager;
using UnityEngine;

namespace NoneProject.TestModule
{
    // Scripted by Raycast
    // 2025.01.17
    // 인스펙터에서 Enemy를 Spawn 테스트를 하기 위한 클래스입니다. 
    public class EnemyTestSpawnModule : MonoBehaviour
    {
        [TitleGroup("Enemy")]
        [HorizontalGroup("Enemy/List")]
        [SerializeField] private List<string> nameList = new List<string>();
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private async void Start()
        {
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);

            LoadList();
        }
        
        private async void LoadList()
        {
            await AddressableManager.Instance.LoadAssetsLabel<GameObject>(AddressableLabel.Enemy, OnComplete);

            void OnComplete(GameObject enemy)
            {
                nameList.Add(enemy.name);
            }
        }
        
        private void Log()
        {
            Debug.Log($"[Test Module] '{enemyID}' is null, empty or not found in the list...");
        }
        
        [TitleGroup("Enemy")]
        [HorizontalGroup("Enemy/Test")]
        [SerializeField] 
        private string enemyID;
        
        [HorizontalGroup("Enemy/Test", width: 0.1f)]
        [Button("Apply")]
        private async void Apply()
        {
            if (Application.isPlaying is false)
            {
                Debug.Log("[Test Module] Application isPlaying is false...");
                return;
            }
            
            if (string.IsNullOrEmpty(enemyID))
            {
                Log();
                return;
            }

            if (nameList.Contains(enemyID) is false)
            {
                Log();
                return;
            }
            
            await EnemyManager.Instance.Get(enemyID);
        }
        
        [Button("Apply All")]
        private async void ApplyAll()
        {
            if (Application.isPlaying is false)
            {
                Debug.Log("[Test Module] Application isPlaying is false...");
                return;
            }
            
            if (string.IsNullOrEmpty(enemyID))
            {
                Log();
                return;
            }

            if (nameList.Contains(enemyID) is false)
            {
                Log();
                return;
            }

            for (var i = 0; i < 100; i++)
            {
                await EnemyManager.Instance.Get(enemyID);
            }
        }
    }
}