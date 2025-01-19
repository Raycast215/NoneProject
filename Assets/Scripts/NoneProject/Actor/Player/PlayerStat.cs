using NoneProject.Actor.Stat;

namespace NoneProject.Actor.Player
{
    public class PlayerStat
    {
        public string ID { get; private set; }
        public float MaxHp { get; private set; }
        public float CurrentHp { get; private set; }
        public float MoveSpeed { get; private set; }
        public float AttackDelay { get; private set; }
        public float Damage { get; private set; }
        public float KnockBackRate { get; private set; }

        private readonly StatBase _baseStat;
        
        public PlayerStat(StatBase stat)
        {
            _baseStat = stat;

            Initialize();
        }

        public void Initialize()
        {
            // ID = _baseStat.id;
            // MoveSpeed = _baseStat.moveSpeed;
            // MaxHp = _baseStat.hp;
            // CurrentHp = MaxHp;
            // Damage = _baseStat.damage;
            // AttackDelay = _baseStat.attackDelay;
            // KnockBackRate = _baseStat.knockBackRate;
        }
    }
}