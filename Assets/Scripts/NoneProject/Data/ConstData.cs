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
        
#region Behaviour Option

        [TitleGroup("Behaviour Option")] 
        [SerializeField]
        private float checkDistance = 0.5f;

        [TitleGroup("Behaviour Option")] 
        [SerializeField]
        private float autoMoveVecOffset = 2.0f;
        
        public float CheckDistance => checkDistance;
        public float AutoMoveVecOffset => autoMoveVecOffset;
        
#endregion

#region SFX Object Pool

        [TitleGroup("SFX Object Pool Option")]
        [SerializeField] 
        private int sfxCapacity = 5;
        
        [TitleGroup("SFX Object Pool Option")]
        [SerializeField] 
        private int sfxDefaultLimitCount = 10;

        [TitleGroup("SFX Object Pool Option")] 
        [SerializeField]
        private string soundObjectPath;
        
        public int SfxCapacity => sfxCapacity;
        public int SfxDefaultLimitCount => sfxDefaultLimitCount;
        public string SoundObjectPath => soundObjectPath;

#endregion
    }
}