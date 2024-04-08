
using System;
using System.Collections.Generic;
using Template.Manager;
using UnityEngine;


namespace NoneProject.Manager
{
    public class SoundManager : SingletonBase<SoundManager>
    {
        private readonly Dictionary<string, AudioClip> _audioDic = new Dictionary<string, AudioClip>();

        public void PlaySound(string soundName)
        {
            // sound pool 가져옴.
            var soundPool = PoolManager.Instance.SoundPool.Get();

            // 가져온 pool에 실행할 sound를 담아 재생.
            LoadClip(soundName, clip => soundPool.PlaySound(clip).Forget());
        }

        private void LoadClip(string soundName, Action<AudioClip> onComplete)
        {
            // 이미 key가 존재하는 경우 바로 실행.
            if (_audioDic.ContainsKey(soundName))
            {
                onComplete?.Invoke(_audioDic[soundName]);
                return;
            }
            
            // key가 없는 경우 새로 Load하여 실행.
            AddressableManager.Instance.LoadAsset<AudioClip>(soundName, OnComplete);
            return;
            
            void OnComplete(AudioClip clip)
            {
                _audioDic.Add(soundName, clip);
                    
                onComplete?.Invoke(clip);
            }
        }

#region Override Implementation
        
        protected override void Initialized()
        {
            IsInitialized = true;
        }
        
#endregion
    }
}