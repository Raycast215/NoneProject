
using Cysharp.Threading.Tasks;
using NoneProject.Manager;
using Template.Pool;
using UnityEngine;


namespace NoneProject.Pool
{
    public class SoundPool : PoolBase
    {
        [SerializeField] private AudioSource source;
        
        /// 받아온 clip으로 Sound를 재생하는 함수입니다. 
        public async UniTaskVoid PlaySound(AudioClip clip)
        {
            gameObject.SetActive(true);
            
            // clip 설정.
            source.clip = clip;
            // Sound 재생.
            source.Play();

            // 재생이 끝날 때까지 대기.
            await UniTask.WaitUntil(() => source.isPlaying is false, cancellationToken: Cts.Token);

            Release();
        }

#region Override Implementation

        protected override void Release()
        {
            // Clip 초기화.
            source.clip = null;
            
            // Pool 해제.
            PoolManager.Instance.SoundPool.Return(this);
        }

#endregion
    }
}