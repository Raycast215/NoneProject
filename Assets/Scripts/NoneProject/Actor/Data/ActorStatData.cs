using System;

namespace NoneProject.Actor.Data
{
    // Scripted by Raycast
    // 2025.01.25
    // Actor가 사용할 Stat Data 클래스입니다.
    [Serializable]
    public class ActorStatData
    {
        public string id;
        public float maxHp;
        public float moveSpeed;
        public float attackDelay;
        public float damage;
        public float knockBackRate;
        public string[] skills;
    }
}