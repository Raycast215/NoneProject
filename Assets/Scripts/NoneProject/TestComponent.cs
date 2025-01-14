using Sirenix.OdinInspector;
using Template.Manager;
using UnityEngine;

namespace NoneProject
{
    public class TestComponent : MonoBehaviour
    {
        [TitleGroup("Bgm")]
        [HorizontalGroup("Bgm/Bgm A")]
        [SerializeField] 
        private string testBgmNameA;
        
        [HorizontalGroup("Bgm/Bgm A", width: 0.1f)]
        [Button("Play")]
        private void PlayTestBgmA()
        {
            if (Application.isPlaying is false)
                return;
            
            SoundManager.Instance.PlayBgm(testBgmNameA, isFade: true);
        }
        
        [HorizontalGroup("Bgm/Bgm B")]
        [SerializeField] 
        private string testBgmNameB;
        
        [HorizontalGroup("Bgm/Bgm B", width: 0.1f)]
        [Button("Play")]
        private void PlayTestBgmB()
        {
            if (Application.isPlaying is false)
                return;
            
            SoundManager.Instance.PlayBgm(testBgmNameB, isFade: true);
        }
        
        [TitleGroup("Sfx")]
        [HorizontalGroup("Sfx/Sfx A")]
        [SerializeField] 
        private string testSfxNameA;
        
        [HorizontalGroup("Sfx/Sfx A", width: 0.1f)]
        [Button("Play")]
        private void PlayTestSfxA()
        {
            if (Application.isPlaying is false)
                return;
            
            SoundManager.Instance.PlaySfx(testSfxNameA);
        }
        
        [HorizontalGroup("Sfx/Sfx B")]
        [SerializeField] 
        private string testSfxNameB;
        
        [HorizontalGroup("Sfx/Sfx B", width: 0.1f)]
        [Button("Play")]
        private void PlayTestSfxB()
        {
            if (Application.isPlaying is false)
                return;
            
            SoundManager.Instance.PlaySfx(testSfxNameB);
        }
    }
}