using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Manager;
using UnityEngine;

namespace NoneProject.UI.EnemyCount
{
    // Scripted by Raycast
    // 2025.01.25
    // 활성화 된 Enemy 수를 UI에 표시하는 클래스입니다.
    public class EnemyCountViewer : MonoBehaviour
    {
        [SerializeField] private EnemyCountUI ui;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        
        private async void Start()
        {
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);
            await UniTask.WaitUntil(() => GameManager.Instance.InGame, cancellationToken: _cts.Token);
            await UniTask.WaitUntil(() => GameManager.Instance.InGame.IsInitialized, cancellationToken: _cts.Token);
            
            EnemyManager.Instance.OnEnemyCountUpdated += ui.SetCount;
            EnemyManager.Instance.OnEnemyCountUpdated += _ => ui.MoveIcon();

            ui.SetCount(0);
        }
    }
}