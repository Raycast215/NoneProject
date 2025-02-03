using System;
using NoneProject.Actor.Data;
using NoneProject.Actor.Stat;
using UnityEngine;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.24
    // Enemy가 사용할 Stat을 관리하는 클래스입니다.
    [Serializable]
    public class EnemyStatController : StatBase
    {
        public float DetectRange { get; private set; }
        public float AttackRange { get; private set; }
        public float KnockBackRate { get; protected set; }
        
        public EnemyStatController(EnemyStatData statData)
        {
            DataBaseStatData = statData.dataBaseStatData;

            DetectRange = statData.detectRange;
            AttackRange = statData.attackRange;
            KnockBackRate = Mathf.Clamp01(statData.knockBackRate);
            
            base.Initialize();
        }
    }
}