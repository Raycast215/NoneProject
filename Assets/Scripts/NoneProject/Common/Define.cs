namespace NoneProject.Common
{
    public enum AddressableLabel
    {
        Common,
        Bgm,
        Sfx,
        Player,
        Enemy,
        Projectile
    }

    public enum MovePattern
    {
        Hold,
        Target,
        Move,
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