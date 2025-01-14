using NoneProject.GameSystem.Composition;

namespace NoneProject.GameSystem.Attack
{
    // Scripted by Raycast
    // 2024.11.28
    public abstract class AttackBase
    {
        protected AssetRender AssetRenderer;
        protected Move2DBehaviour MoveBehaviour;
        
        public abstract void Initialized();
    }
}