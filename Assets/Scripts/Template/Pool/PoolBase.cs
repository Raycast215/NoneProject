
using System.Threading;
using UnityEngine;


namespace Template.Pool
{
    public abstract class PoolBase : MonoBehaviour
    {
        protected CancellationTokenSource Cts;

        private void Awake()
        {
            Cts = new CancellationTokenSource();
        }
        
        private void OnDestroy()
        {
            Cts.Cancel();
            Cts.Dispose();
            Cts = null;
        }

        protected abstract void Release();
    }
}