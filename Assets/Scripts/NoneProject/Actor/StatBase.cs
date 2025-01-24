using NoneProject.Actor.Data;

namespace NoneProject.Actor
{
    // Scripted by Raycast
    // 2025.01.25
    // Actor가 사용할 Stat 클래스입니다.
    public abstract class StatBase
    {
        public string ID { get; protected set; }
        public float MaxHp { get; protected set; }
        public float CurrentHp { get; protected set; }
        public float MoveSpeed { get; protected set; }
        public float AttackDelay { get; protected set; }
        public float Damage { get; protected set; }
        public float KnockBackRate { get; protected set; }

        protected ActorStatData BaseStat;

        public virtual void Initialize()
        {
            ID = BaseStat.id;
            MoveSpeed = BaseStat.moveSpeed;
            MaxHp = BaseStat.maxHp;
            CurrentHp = MaxHp;
            Damage = BaseStat.damage;
            AttackDelay = BaseStat.attackDelay;
            KnockBackRate = BaseStat.knockBackRate;
        }
    }
}