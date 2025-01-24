using UnityEngine;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.25
    // Enemy가 사용할 Stat SO 클래스입니다.
    [CreateAssetMenu(fileName = "EnemyStat", menuName = "Scriptable Object/Enemy Stat Data")]
    public class EnemyStatSo : ScriptableObject
    {
        public EnemyStatBase[] data;
    }
}