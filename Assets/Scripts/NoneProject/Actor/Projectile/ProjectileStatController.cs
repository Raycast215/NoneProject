using NoneProject.Actor.Data;

namespace NoneProject.Actor.Projectile
{
    public class ProjectileStatController
    {
        public string ID { get; protected set; }
        public float MoveSpeed { get; protected set; }
        public float Damage { get; protected set; }
        public float KnockBackPower { get; protected set; }

        public ProjectileStatController(ProjectileStatData statData)
        {
            ID = statData.id;
            MoveSpeed = statData.moveSpeed;
            Damage = statData.damage;
            KnockBackPower = statData.knockBackPower;
        }
    }
}