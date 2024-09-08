using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using NoneProject.Pool;
using Template.Pool;
using Template.Utility;
using UnityEngine;

namespace Template.Manager
{
    public class SoundManager : SingletonBase<SoundManager>
    {
        private const string Bgm = "Bgm";
        private const string SoundPath = "Sound/SoundObject";
        
        private event Action<AudioClip> OnBgmPlayed = delegate {  };
        
        private readonly Dictionary<string, AudioClip> _audioClipDic = new Dictionary<string, AudioClip>();
        private readonly Dictionary<string, int> _sfxCountDic = new Dictionary<string, int>();

        [SerializeField] private int sfxCapacity = 10;
        [SerializeField] private int sfxLimitCount = 5;
        
        private AudioSource _bgmSource;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private Pooling<SoundPool> _soundPool;
        private SoundPool _sfxObject;

        /// BGM을 실행합니다.
        public void PlayBgm(string clipName, float volume = 1.0f, bool isLoop = true, bool isFade = false)
        {
            if (string.IsNullOrEmpty(clipName))
                return;

            Action<AudioClip> onBgmPlayed = null;
            onBgmPlayed += ac => PlayBgmSound(ac, volume, isFade);

            _bgmSource.loop = isLoop;

            if (_audioClipDic.TryGetValue(clipName, out var clip))
            {
                // 동일한 Clip이 재생중이면 리턴.
                if (_bgmSource.clip == clip)
                    return;

                onBgmPlayed.Invoke(clip);
            }
            else
            {
                onBgmPlayed += ac => AddAudioClip(clipName, ac);
                AddressableManager.Instance.LoadAsset<AudioClip>(clipName, onBgmPlayed.Invoke).Forget();
            }
        }
        
        /// SFX를 실행합니다. 
        public void PlaySfx(string clipName, float volume = 1.0f)
        {
            if (string.IsNullOrEmpty(clipName))
                return;
            
            if (_sfxCountDic.ContainsKey(clipName) is false)
                _sfxCountDic.Add(clipName, 1);

            // 동일한 Clip이 일정 수 이상 재생중인 경우 리턴.
            if (_sfxCountDic[clipName] > sfxLimitCount)
                return;
            
            _sfxCountDic[clipName]++;
            
            Action<AudioClip> onSfxPlayed = null;
            var soundObject = _soundPool.Get();
            
            onSfxPlayed += _ => soundObject.OnReleased += () => _sfxCountDic[clipName]--;
            onSfxPlayed += ac => soundObject.PlaySound(ac, volume).Forget();

            if (_audioClipDic.ContainsKey(clipName))
            {
                onSfxPlayed.Invoke(_audioClipDic[clipName]);
            }
            else
            {
                onSfxPlayed += ac => AddAudioClip(clipName, ac);
                AddressableManager.Instance.LoadAsset<AudioClip>(clipName, onSfxPlayed.Invoke).Forget();
            }
        }

        /// Sfx를 반환합니다.
        public void Release(SoundPool sound)
        {
            _soundPool.Return(sound);
        }

        /// AudioClip을 추가합니다.
        private void AddAudioClip(string clipName, AudioClip clip)
        {
            if (clip is null)
                return;

            if (_audioClipDic.ContainsKey(clipName))
                return;
            
            _audioClipDic.Add(clipName, clip);
        }
        
        private void PlayBgmSound(AudioClip clipAsset, float volume, bool isFade)
        {
            var toVolume = Mathf.Clamp(volume, 0.0f, 1.0f);

            if (_bgmSource.isPlaying)
            {
                // 이전 사운드 Fade로 멈추고, 다음 사운드 Fade로 재생.
                if (isFade)
                {
                    Fade(_bgmSource, 0.0f, onFinished: Finished).Forget();
                    return;

                    void Finished()
                    {
                        _bgmSource.Stop();
                        OnBgmPlayed?.Invoke(clipAsset);
                        Fade(_bgmSource, volume).Forget();
                    }
                }

                // 이전 사운드 멈추고, 다음 사운드 재생.
                _bgmSource.Stop();
                OnBgmPlayed?.Invoke(clipAsset);
                return;
            }

            // Fade로 재생.
            if (isFade)
            {
                OnBgmPlayed?.Invoke(clipAsset);
                Fade(_bgmSource, toVolume).Forget();
                return;
            }

            // 즉시 재생.
            OnBgmPlayed?.Invoke(clipAsset);
        }
        
        private async UniTaskVoid Fade(AudioSource source, float toVolume, Action onFinished = null)
        {
            while (true)
            {
                await UniTask.Yield(cancellationToken: _cts.Token);

                if (_cts is null || _cts.IsCancellationRequested)
                    return;
                    
                source.volume += Util.GetToggleOne(toVolume > 0.0f) * Time.deltaTime;

                if (Mathf.Approximately(source.volume, toVolume) is false) 
                    continue;
                    
                source.volume = toVolume;
                break;
            }
                
            onFinished?.Invoke();
        }
        
        private void CrateBgmObject()
        {
            _bgmSource ??= Util.CreateObject<AudioSource>(Bgm, transform, Vector3.zero);
            _bgmSource.volume = 0.0f;
        }
        
        private void Subscribed()
        {
            OnBgmPlayed += clip => _bgmSource.clip = clip;
            OnBgmPlayed += _ => _bgmSource.Play();
        }

        private void InitializedSfx()
        {
            _sfxObject = Resources.Load<SoundPool>(SoundPath);
            _soundPool = new Pooling<SoundPool>(_sfxObject, sfxCapacity, transform);
            
            _soundPool.Pool();
        }

#region Override Methods Implementation
        
        protected override void Initialized()
        {
            InitializedSfx();
            CrateBgmObject();
            Subscribed();

            isInitialized = true;
        }
        
        protected override void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            base.OnDestroy();
        }
        
#endregion
    }
}