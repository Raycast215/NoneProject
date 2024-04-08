

namespace NoneProject.Common
{
    public static class Define
    {
        public const string Sound = "Sound";
        public const string Addressable = "Addressable";
        
        public const string SoundHolder = "Sound Holder";
        public const string ProjectileHolder = "Projectile Holder";
        public const string PlayerHolder = "Player Holder";
        public const string EnemyHolder = "Enemy Holder";
        public const string MapHolder = "Map Holder";

        public const float HolderOffset = -0.5f;
        
        public static float[] PosYOffset = new float[] { -1.5f, -0.75f, 0.0f, 0.75f, 1.5f };
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
    
    public enum SoundType
    {
        None,    // 기본 재생
        OneShot, // 기존 재생 취소 후 재생
        Fade     // 기존 재생 페이드 아웃
    }
    
    public enum StageStateType
    {
        Idle,
        Battle,
        Move,
    }
}