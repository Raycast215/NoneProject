
using System;
using Cysharp.Threading.Tasks;
using NoneProject.Actor;
using NoneProject.Common;
using NoneProject.Manager;
using UnityEngine;
using Object = UnityEngine.Object;


namespace NoneProject.GameSystem.Stage
{
    public static class ActorCreator
    {
        public static async UniTask<PlayerController> CreatePlayer(string playerName, Transform parent)
        {
            PlayerController prefab = null;
            AddressableManager.Instance.LoadAsset<PlayerController>(playerName, asset => prefab = asset);

            await UniTask.WaitUntil(() => prefab is not null);
            
            var player = Object.Instantiate(prefab, parent);
            var playerTransform = player.transform;
            playerTransform.localScale = Vector3.one;
            playerTransform.localPosition = Vector3.zero;
            
            ActorListHolder.PlayerList.Add(player);
            
            return player;
        }

        public static async UniTask<EnemyController> CreateEnemy(string enemyName, Transform parent)
        {
            const float offset = 2.0f;
            
            EnemyController prefab = null;
            AddressableManager.Instance.LoadAsset<EnemyController>(enemyName, asset => prefab = asset);

            await UniTask.WaitUntil(() => prefab is not null);
            
            var enemy = Object.Instantiate(prefab, parent);
            enemy.transform.localScale = Vector3.one * offset;
            
            ActorListHolder.EnemyList.Add(enemy);
            
            return enemy;
        }

        public static async UniTask<MapController> CreateMap(string mapType, Transform parent)
        {
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