using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NoneProject.Utility
{
    // Scripted by Raycast
    // 2025.02.04
    // 타이머 기능을 실행하는 클래스입니다.
    public class Timer : IDisposable
    {
        public event Action<float> OnTimeUpdated;
        public event Action OnTimerStarted;
        public event Action OnTimerFinished;
        
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly float _startDelayTime;
        private float _remainingTime;
        private bool _isStart;

        public Timer(float startDelayTime = 0.0f)
        {
            _startDelayTime = startDelayTime;
        }
        
        public async UniTaskVoid StartTimer(float startTime)
        {
            if (_isStart)
                return;
            
            _isStart = true;
            _remainingTime = startTime;
            _cts ??= new CancellationTokenSource();
            
            // 시작 시간 업데이트.
            OnTimeUpdated?.Invoke(_remainingTime);
            
            // 시작 딜레이 대기.
            await UniTask.WaitForSeconds(_startDelayTime, cancellationToken: _cts.Token);
                
            if(_cts is null || _cts.IsCancellationRequested)
                return;
            
            // 타이머 시작 이벤트 실행.
            OnTimerStarted?.Invoke();
            
            while (_remainingTime > 0.0f)
            {
                await UniTask.Yield(cancellationToken: _cts.Token);
             
                if (_cts is null || _cts.IsCancellationRequested)
                    return;
                
                // 남은 시간 감소.
                _remainingTime -= Time.deltaTime;
                // 남은 시간 업데이트.
                OnTimeUpdated?.Invoke(_remainingTime);
            }
            
            _isStart = false;
            _remainingTime = 0.0f;
            
            // 남은 시간 업데이트.
            OnTimeUpdated?.Invoke(_remainingTime);
            // 타이머 종료 이벤트 실행.
            OnTimerFinished?.Invoke();
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}