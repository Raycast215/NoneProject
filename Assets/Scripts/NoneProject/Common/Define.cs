

namespace NoneProject.Common
{
    public static class Define
    {
        public const string ProjectileHolder = "Projectile Holder";
        public const string MapHolder = "Map Holder";

        public const float HolderOffset = -0.5f;
        
        public static float[] PosYOffset = new float[] { -1.5f, -0.75f, 0.0f, 0.75f, 1.5f };
    }

    public enum AddressableLabel
    {
        Common,
        Player,
        Enemy,
        Projectile
    }

    public enum SoundTag
    {
        Bgm,
        Sfx
    }

    public enum MovePattern
    {
        Hold,
        Target,
        Move
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

    public enum MapType
    {
        None,
        Forest,
    }
    
    public enum ActorType
    {
        Player,
        Enemy,
        Projectile,
    }

    public enum StageStateType
    {
        Idle,
        Battle,
        Move,
    }
}