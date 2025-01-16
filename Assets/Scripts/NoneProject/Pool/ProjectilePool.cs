using NoneProject.Projectile;
using Template.Pool;

namespace NoneProject.Pool
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile Pool의 오브젝트를 포함하는 클래스.
    public class ProjectilePool : PoolBase<ProjectileController>
    {
#region Override Methods

        public override void SetController(ProjectileController controller)
        {
           
        }

        protected override void Release()
        {
            
        }
        
#endregion
    }
}