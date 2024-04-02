
using System.Threading;
using UnityEngine;


namespace Template.Manager
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        
        public static T Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = FindObjectOfType<T>();
                    
                    if (instance is null)
                    {
                        var singletonObject = new GameObject(typeof(T).Name);
                        
                        instance = singletonObject.AddComponent<T>();
                    }
                }
                
                return instance;
            }
        }
        
        public bool IsInitialized { get; protected set; }

        protected CancellationTokenSource Cts = new CancellationTokenSource();
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            instance = this as T;
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Initialized();
        }

        private void OnDestroy()
        {
            Cts.Cancel();
            Cts.Dispose();
        }

        protected abstract void Initialized();
    }
}