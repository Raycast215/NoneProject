using System;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.24
    // Enemy가 사용할 Stat을 관리하는 클래스입니다.
    [Serializable]
    public sealed class EnemyStat : StatBase
    {
        public float DetectRange { get; private set; }
        public float AttackRange { get; private set; }
        
        public EnemyStat(EnemyStatBase stat)
        {
            BaseStat = stat.baseStat;

            DetectRange = stat.detectRange;
            AttackRange = stat.attackRange;
            
            Initialize();
        }
    }
}