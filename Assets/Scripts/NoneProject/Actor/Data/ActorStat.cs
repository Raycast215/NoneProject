using System;

namespace NoneProject.Actor.Data
{
    [Serializable]
    public class ActorStat
    {
        public string id;
        public float moveSpeed;
        public float hp;
        public float damage;
        public float attackDelay;
        public float knockBackRate;
    }
}