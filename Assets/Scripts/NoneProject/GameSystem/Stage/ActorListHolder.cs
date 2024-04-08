
using System.Collections.Generic;
using NoneProject.Actor;
using UnityEngine;


namespace NoneProject.GameSystem.Stage
{
    public static class ActorListHolder
    {
        public static readonly List<PlayerController> PlayerList = new List<PlayerController>();
        public static readonly List<EnemyController> EnemyList = new List<EnemyController>();
        public static readonly List<MapController> MapList = new List<MapController>();
        
        public static void ClearList<T>(List<T> toList) where T : Component
        {
            foreach (var ac in toList)
            {
                Object.Destroy(ac);
            }
            
            toList.Clear();
        }
    }
}