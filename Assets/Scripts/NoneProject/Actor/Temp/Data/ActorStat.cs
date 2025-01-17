
using System;


namespace NoneProject.Actor.Data
{
    [Serializable]
    public class ActorStat
    {
        public float currentHp;
        public float maxHp;
        public float moveSpeed;
        public float targetPosX;
        public float attackDelay;
        public bool isDead;
    }
}