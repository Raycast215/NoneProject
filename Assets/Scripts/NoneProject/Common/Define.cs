namespace NoneProject.Common
{
    public enum AddressableLabel
    {
        Common,
        Bgm,
        Sfx,
        Player,
        Enemy,
        Projectile,
        Stat
    }

    public enum ActorType
    {
        Player,
        Enemy,
        Projectile
    }
    
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public enum MovePattern
    {
        None,
        Hold,
        Forward,
        Target,
        Random
    }

    public enum ActorState
    {
        Idle,
        Run,
        Death,
        Debuff_Stun,
        Attack_Normal,
        Attack_Bow,
        Attack_Magic,
        Skill_Normal,
        Skill_Bow,
        Skill_Magic,
    }
}