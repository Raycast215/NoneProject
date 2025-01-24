using Sirenix.OdinInspector;
using UnityEngine;

namespace NoneProject.Data
{
    // Scripted by Raycast
    // 2025.01.09
    // 공용으로 사용할 Const를 Scriptable Object로 모아두기 위한 클래스입니다.
    [CreateAssetMenu(fileName = "Const Data", menuName = "Scriptable Object/Const Data")]
    public class ConstData : ScriptableObject
    {

#region Default Data

            [TitleGroup("Default Data")] 
            [SerializeField]
            private string defaultPlayerID;

            public string DefaultPlayerID => defaultPlayerID;
            
#endregion
            
#region String Const

        [TitleGroup("String Const")] 
        [SerializeField]
        private string playerHolder = "Player Holder";
        
        [TitleGroup("String Const")] 
        [SerializeField]
        private string tempHolder = "Temp Holder";
        
        [TitleGroup("String Const")] 
        [SerializeField]
        private string objectPoolHolder = "ObjectPool Holder";
        
        public string PlayerHolder => playerHolder;
        public string TempHolder => tempHolder;
        public string ObjectPoolHolder => objectPoolHolder;

#endregion
        
#region Behaviour Option

        [TitleGroup("Behaviour Option")] 
        [SerializeField]
        private float checkDistance = 0.5f;
        
        [TitleGroup("Behaviour Option")] 
        [SerializeField]
        private float randomMoveVecRange = 2.0f;
        
        public float CheckDistance => checkDistance;
        public float RandomMoveVecRange => randomMoveVecRange;
        
#endregion

#region SFX Object Pool

        [TitleGroup("SFX Object Pool Option")]
        [SerializeField] 
        private int capacity = 5;
        
        [TitleGroup("SFX Object Pool Option")]
        [SerializeField] 
        private int sfxDefaultLimitCount = 10;

        public int Capacity => capacity;
        public int SfxDefaultLimitCount => sfxDefaultLimitCount;

        #endregion

#region Object Pool Path

        [TitleGroup("Object Pool Path")] 
        [SerializeField]
        private string commonObjectPath;

        [TitleGroup("Object Pool Path")] 
        [SerializeField]
        private string soundObjectPath;
        
        [TitleGroup("Object Pool Path")] 
        [SerializeField]
        private string enemyObjectPath;
        
        [TitleGroup("Object Pool Path")] 
        [SerializeField]
        private string projectileObjectPath;
        
        public string SoundObjectPath => $"{commonObjectPath}/{soundObjectPath}";
        public string EnemyObjectPath => $"{commonObjectPath}/{enemyObjectPath}";
        public string ProjectileObjectPath => $"{commonObjectPath}/{projectileObjectPath}";

#endregion
    }
}