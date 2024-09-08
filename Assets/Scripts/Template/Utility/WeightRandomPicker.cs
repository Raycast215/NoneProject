using System;
using System.Collections.Generic;
using System.Linq;

namespace Template.Utility
{
    // Scripted by Raycast
    // 2024.03.04
    // 가중치에 따라 지정한 T를 랜덤으로 반환하는 클래스입니다.
    
    public class WeightRandomPicker<T>
    {
        private readonly Dictionary<T, int> _weightDic = new Dictionary<T, int>();
        
        public void Add(T type, int weight)
        {
            _weightDic.Add(type, weight);
        }

        public void Clear()
        {
            _weightDic.Clear();
        }

        public T Get(Action<T,  float> onComplete = null)
        {
            // 랜덤 시드.
            var seed = UnityEngine.Random.Range(0, int.MaxValue);
            
            // 시드 초기화.
            UnityEngine.Random.InitState(seed);
            
            // 반환할 대상.
            T ret = default;
            // 비교할 가중치.
            var sum = 0;
            // 가중치 전체 합산.
            var weightTotalSum = _weightDic.Values.Sum(x => x);
            // 랜덤 값.
            var randomValue = UnityEngine.Random.Range(1, weightTotalSum + 1);

            foreach (var x in _weightDic.OrderBy(x => x.Value))
            {
                // 비교할 가중치 계산.
                sum += x.Value;

                // 가중치 계산 값보다 랜덤 값이 더 큰 경우 건너뜀.
                if (sum < randomValue)
                    continue;
                
                // 반환할 대상 저장.
                ret = x.Key;
                // 완료 이벤트 실행.
                onComplete?.Invoke(ret, (float)x.Value / weightTotalSum);
                break;
            }
            
            return ret;
        }
    }
}