using UnityEngine;

namespace NoneProject.Projectile
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile을 관리하는 클래스입니다.
    public class ProjectileController : MonoBehaviour
    {
        public string ID { get; private set; }
        
        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy is false)
                return;
            
            // _moveBehaviour.Move();
        }
        
        public void SetID(string id)
        {
            ID = id;
        }
        
        private void Subscribed()
        {
          
        }

        public void Initialized()
        {
            
        }
    }
}