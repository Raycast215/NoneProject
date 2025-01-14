using Cysharp.Threading.Tasks;
using NoneProject.Actor;
using Template.Manager;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NoneProject.GameSystem.Stage
{
    public static class ActorCreator
    {
        public static async UniTask<EnemyController> CreateEnemy(string enemyName, Transform parent)
        {
            return null;
            
            // const float offset = 2.0f;
            //
            // EnemyController prefab = null;
            // await AddressableManager.Instance.LoadAsset<EnemyController>(enemyName, asset => prefab = asset);
            //
            // await UniTask.WaitUntil(() => prefab is not null);
            //
            // var enemy = Object.Instantiate(prefab, parent);
            // enemy.transform.localScale = Vector3.one * offset;
            //
            // ActorListHolder.EnemyList.Add(enemy);
            //
            // return enemy;
        }

        public static async UniTask<MapController> CreateMap(string mapType, Transform parent)
        {
            return null;
            
            const float offset = 1.5f;
            var mapName = $"Map_{mapType}";
            
            MapController prefab = null;
            AddressableManager.Instance.LoadAsset<MapController>(mapName, asset => prefab = asset);

            await UniTask.WaitUntil(() => prefab is not null);
            
            var map = Object.Instantiate(prefab, parent);
            var mapTransform = map.transform;
            mapTransform.localScale = new Vector3(offset, offset, offset);
            mapTransform.localPosition = Vector3.zero;
            
            ActorListHolder.MapList.Add(map);
            
            return map;
        }
    }
}