using System;

namespace NoneProject.Actor.Data
{
    [Serializable]
    public class ProjectileStatData
    {
        public string id;
        public float moveSpeed;
        public float damage;
        public float knockBackPower;
    }
}