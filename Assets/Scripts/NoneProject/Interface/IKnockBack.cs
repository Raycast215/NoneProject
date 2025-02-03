using UnityEngine;

namespace NoneProject.Interface
{
    // Scripted by Raycast
    // 2025.02.04
    // 넉백 로직 인터페이스.
    public interface IKnockBack
    {
        public void KnockBack(float power, Vector2 casterDirection);
    }
}