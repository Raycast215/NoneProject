using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using Sirenix.OdinInspector;
using Template.Manager;
using UnityEngine;

namespace NoneProject.TestModule
{
    // Scripted by Raycast
    // 2025.01.17
    // 인스펙터에서 BGM을 직접 테스트하기 위한 클래스입니다. 
    public class BgmTestModule : MonoBehaviour
    {
        [TitleGroup("BGM")]
        [HorizontalGroup("BGM/List")]
        [SerializeField] private List<string> nameList = new List<string>();
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        
        private async void Start()
        {
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);

            LoadList();
        }

        private async void LoadList()
        {
           await AddressableManager.Instance.LoadAssetsLabel<AudioClip>(AddressableLabel.Bgm, OnComplete);

           void OnComplete(AudioClip clip)
           {
               nameList.Add(clip.name);
           }
        }
        
        private void Log()
        {
            Debug.Log($"[Test Module] '{bgmClipName}' is null, empty or not found in the list...");
        }
        
        [HorizontalGroup("BGM/Fade")]
        [SerializeField] 
        private bool isFade;
        
        [HorizontalGroup("BGM/Volume")]
        [Range(0.0f, 1.0f)]
        [SerializeField] 
        private float volume;
        
        [HorizontalGroup("BGM/Test")]
        [SerializeField] 
        private string bgmClipName;
        
        [HorizontalGroup("BGM/Test", width: 0.1f)]
        [Button("Play")]
        private void Play()
        {
            if (Application.isPlaying is false)
            {
                Debug.Log("[Test Module] Application isPlaying is false...");
                return;
            }
            
            if (string.IsNullOrEmpty(bgmClipName))
            {
                Log();
                return;
            }
            
            if (nameList.Contains(bgmClipName) is false)
            {
                Log();
                return;
            }
            
            SoundManager.Instance.PlayBgm(bgmClipName, volume: volume, isFade: isFade);
        }
    }
}