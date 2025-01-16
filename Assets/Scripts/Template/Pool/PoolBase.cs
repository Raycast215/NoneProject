using System.Threading;
using UnityEngine;

namespace Template.Pool
{
    public abstract class PoolBase<T> : MonoBehaviour where T : Component
    {
        public T Controller { get; protected set; }
        
        protected CancellationTokenSource Cts = new CancellationTokenSource();
        
        private void OnDestroy()
        {
            Cts?.Cancel();
            Cts?.Dispose();
            Cts = null;
        }
        
        public void SetControllerParent(Transform parent, bool isActive = true)
        {
            // 오브젝트를 받아온 부모 하위로 이동.
            Controller.transform.SetParent(parent, false);
            // Controller 활성화 상태 변경.
            Controller.gameObject.SetActive(isActive);
        }

        public abstract void SetController(T controller);
        protected abstract void Release();
    }
}