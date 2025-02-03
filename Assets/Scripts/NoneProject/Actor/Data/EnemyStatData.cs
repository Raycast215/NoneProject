using System;

namespace NoneProject.Actor.Data
{
    [Serializable]
    public class EnemyStatData
    {
        public ActorStatDataBase dataBaseStatData;
        public float detectRange;
        public float attackRange;
        public float knockBackRate;
    }
}