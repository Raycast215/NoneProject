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
    // 인스펙터에서 SFX를 직접 테스트하기 위한 클래스입니다. 
    public class SfxTestModule : MonoBehaviour
    {
        [TitleGroup("SFX")]
        [HorizontalGroup("SFX/List")]
        [SerializeField] private List<string> nameList = new List<string>();
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private async void Start()
        {
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized, cancellationToken: _cts.Token);

            LoadList();
        }
        
        private async void LoadList()
        {
            await AddressableManager.Instance.LoadAssetsLabel<AudioClip>(AddressableLabel.Sfx, OnComplete);

            void OnComplete(AudioClip clip)
            {
                nameList.Add(clip.name);
            }
        }

        private void Log()
        {
            Debug.Log($"[Test Module] '{sfxClipName}' is null, empty or not found in the list...");
        }
        
        [HorizontalGroup("SFX/Test")]
        [SerializeField] 
        private string sfxClipName;
        
        [HorizontalGroup("SFX/Test", width: 0.1f)]
        [Button("Play")]
        private void Play()
        {
            if (Application.isPlaying is false)
            {
                Debug.Log("[Test Module] Application isPlaying is false...");
                return;
            }
                

            if (string.IsNullOrEmpty(sfxClipName))
            {
                Log();
                return;
            }
               
            
            if (nameList.Contains(sfxClipName) is false)
            {
                Log();
                return;
            }
            
            SoundManager.Instance.PlaySfx(sfxClipName);
        }
    }
}