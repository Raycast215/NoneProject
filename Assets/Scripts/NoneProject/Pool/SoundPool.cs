
using System.Threading;
using Cysharp.Threading.Tasks;
using Template.Pool;
using UnityEngine;


namespace NoneProject.Pool
{
    public class SoundPool : PoolBase
    {
        [SerializeField] private AudioSource source;

        private CancellationTokenSource _cts;
        
        private void Awake()
        {
            _cts = new CancellationTokenSource();
        }

        private void OnDestroy()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        /// 받아온 clip으로 Sound를 재생하는 함수입니다. 
        public async UniTaskVoid PlaySound(AudioClip clip)
        {
            gameObject.SetActive(true);
            
            // clip 설정.
            source.clip = clip;
            // Sound 재생.
            source.Play();

            // 재생이 끝날 때까지 대기.
            await UniTask.WaitUntil(() => source.isPlaying is false, cancellationToken: _cts.Token);

            Release();
        }

#region Override Implement

        protected override void Release()
        {
            // Clip 초기화.
            source.clip = null;
            
            // Pool 해제.
            PoolController.Instance.SoundPool.Return(this);
        }

#endregion
    }
}