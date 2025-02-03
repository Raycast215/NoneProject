using NoneProject.Actor.Data;

namespace NoneProject.Actor.Stat
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

        protected ActorStatDataBase DataBaseStatData;

        public void SetCurrentHp(float delta)
        {
            CurrentHp += delta;
        }
        
        public virtual void Initialize()
        {
            ID = DataBaseStatData.id;
            MoveSpeed = DataBaseStatData.moveSpeed;
            MaxHp = DataBaseStatData.maxHp;
            CurrentHp = MaxHp;
            Damage = DataBaseStatData.damage;
            AttackDelay = DataBaseStatData.attackDelay;
        }
    }
}