using UnityEngine;

namespace NoneProject.Projectile
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile을 관리하는 클래스입니다.
    public class ProjectileController : MonoBehaviour
    {
        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy is false)
                return;
            
            // _moveBehaviour.Move();
        }

        public void SetPosition(Vector2 position)
        {
            
        }
        
        public void Initialized()
        {
            
        }
        
        private void Subscribed()
        {
          
        }
    }
}