using System;
using NoneProject.Actor.Data;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.25
    // Enemy가 사용할 Stat 클래스입니다.
    [Serializable]
    public class EnemyStatBase
    {
        public ActorStatData baseStat;
        public float detectRange;
        public float attackRange;
    }
}