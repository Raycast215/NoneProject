using System;

namespace NoneProject.Data
{
    [Serializable]
    public class AttackData : BaseData
    {
        public float damage;
        public float speed;
        public float range;
        public float durationTime;
    }
}