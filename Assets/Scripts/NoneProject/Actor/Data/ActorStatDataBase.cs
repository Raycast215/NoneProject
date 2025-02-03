using System;

namespace NoneProject.Actor.Data
{
    [Serializable]
    public class ActorStatDataBase
    {
        public string id;
        public float maxHp;
        public float moveSpeed;
        public float attackDelay;
        public float damage;
        public string[] skills;
    }
}