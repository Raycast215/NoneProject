using NoneProject.Actor.Stat;
using UnityEngine;

namespace NoneProject.ScriptableData
{
    // Scripted by Raycast
    // 2025.02.04
    // Projectile의 Stat 데이터를 담은 Scriptable Object 클래스입니다.
    [CreateAssetMenu(fileName = "ProjectileStatScriptableData", menuName = "Scriptable Object/Projectile Stat Data")]
    public class ProjectileStatScriptableData : ScriptableObject
    {
        public ProjectileStat[] data;
    }
}