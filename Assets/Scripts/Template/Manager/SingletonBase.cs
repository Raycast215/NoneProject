
using UnityEngine;


namespace Template.Manager
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool IsInitialized { get; protected set; }
        
        public static SingletonBase<T> Instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            
            DontDestroyOnLoad(this);
        }

        protected abstract void Initialized();
    }
}