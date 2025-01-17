using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Template.Manager;
using Template.Pool;
using Template.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoneProject.Pool.Common
{
    // Scripted by Raycast
    // 2025.01.17
    // 오브젝트 풀을 사용하는 매니저에서 풀 반환 및 실행을 돕는 클래스입니다.
    public class ObjectPoolController<T, TU> where T : PoolBase<TU> where TU : Component
    {
        public event Func<string, TU, TU> OnObjectControllerUpdated;

        public Pooling<T> Pool { get; private set; }
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Dictionary<string, Queue<TU>> _objectPoolTempDic;
        private readonly string _objectPath;
        private readonly Transform _managerTransform;
        private Transform _tempHolder;
        private Transform _objectPoolHolder;
        private TU _selectObject;

        public ObjectPoolController(Transform managerTransform, string objectPath)
        {
            _managerTransform = managerTransform;
            _objectPath = objectPath;
            _objectPoolTempDic = new Dictionary<string, Queue<TU>>();

            CreateObjectHolder();
            LoadPool();
        }

        public async UniTask<TU> Get(string poolObjectID)
        {
            _selectObject = null;
            
            if (_objectPoolTempDic.TryGetValue(poolObjectID, out var queue))
            {
                // 이전에 반환된 오브젝트가 있는 경우.
                if (queue.Count != 0)
                {
                    // 반환된 오브젝트의 Controller를 수정.
                    return OnObjectControllerUpdated?.Invoke(poolObjectID, queue.Dequeue());
                }
            }
            else
            {
                // 재사용하기 위한 오브젝트의 Controller를 담을 Queue 초기화.
                _objectPoolTempDic[poolObjectID] = new Queue<TU>();
            }

            // 해당 오브젝트에서 사용되는 에셋을 로드.
            await AddressableManager.Instance.LoadAsset<GameObject>(poolObjectID, prefab => LoadAssetComplete(poolObjectID, prefab));
            await UniTask.WaitUntil(() => _selectObject, cancellationToken: _cts.Token);
            
            return _selectObject;
        }

        public void Release(T poolObject)
        {
            if (_objectPoolTempDic.TryGetValue(poolObject.ID, out var queue))
            {
                // Pool 오브젝트를 임시 보관 오브젝트 하위로 이동.
                poolObject.SetControllerParent(_tempHolder, false);
                // 재사용을 위한 queue에 보관.
                queue.Enqueue(poolObject.Controller);
            }
            
            Pool.Return(poolObject);
        }
        
        private void LoadPool()
        {
            var constData = GameManager.Instance.Const;
            var poolObject = Resources.Load<T>(_objectPath);
            
            // Pool 생성.
            Pool = new Pooling<T>(poolObject, constData.Capacity, _objectPoolHolder);
            Pool.Pool();
        }
        
        private void CreateObjectHolder()
        {
            var constData = GameManager.Instance.Const;
            
            // Pool 반환 시 프리팹 재사용을 위한 공간.
            _tempHolder = Util.CreateObject(constData.TempHolder, _managerTransform).transform;
            // Pool 오브젝트들을 보관하기 위한 공간.
            _objectPoolHolder = Util.CreateObject(constData.ObjectPoolHolder, _managerTransform).transform;
        }

        private async void LoadAssetComplete(string poolObjectID, GameObject prefab)
        {
            var newObject = Object.Instantiate(prefab, _objectPoolHolder).GetComponent<TU>();
            
            await UniTask.WaitUntil(() => newObject, cancellationToken: _cts.Token);
            
            // 오브젝트에서 사용할 에셋을 생성해서 오브젝트의 Controller를 수정.
            _selectObject = OnObjectControllerUpdated?.Invoke(poolObjectID, newObject);
        }
    }
}