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
    // 인스펙터에서 Projectile을 Spawn 테스트를 하기 위한 클래스입니다. 
    public class ProjectileTestSpawnModule : MonoBehaviour
    {
        [TitleGroup("Projectile")]
        [HorizontalGroup("Projectile/List")]
        [SerializeField] private List<string> nameList = new List<string>();
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private async void Start()
        {
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);

            LoadList();
        }
        
        private async void LoadList()
        {
            await AddressableManager.Instance.LoadAssetsLabel<GameObject>(AddressableLabel.Projectile, OnComplete);

            void OnComplete(GameObject enemy)
            {
                nameList.Add(enemy.name);
            }
        }
        
        private void Log()
        {
            Debug.Log($"[Test Module] '{projectileID}' is null, empty or not found in the list...");
        }
        
        [TitleGroup("Projectile")]
        [HorizontalGroup("Projectile/Test")]
        [SerializeField] 
        private string projectileID;
        
        [HorizontalGroup("Projectile/Test", width: 0.1f)]
        [Button("Apply")]
        private async void Apply()
        {
            if (Application.isPlaying is false)
            {
                Debug.Log("[Test Module] Application isPlaying is false...");
                return;
            }

            if (string.IsNullOrEmpty(projectileID))
            {
                Log();
                return;
            }
            
            if (nameList.Contains(projectileID) is false)
            {
                Log();
                return;
            }
            
            var projectile = await ProjectileManager.Instance.Get(projectileID);
            var startPos = Manager.PlayerManager.Instance.Player.transform.position;
            
            //projectile.Set(startPos, Vector2.zero);
        }
    }
}