using NoneProject.Actor.Data;
using UnityEngine;

namespace NoneProject.ScriptableData
{
    // Scripted by Raycast
    // 2025.01.25
    // Enemy의 Stat 데이터를 담은 Scriptable Object 클래스입니다.
    [CreateAssetMenu(fileName = "EnemyStatScriptableData", menuName = "Scriptable Object/Enemy Stat Data")]
    public class EnemyStatScriptableData : ScriptableObject
    {
        public EnemyStatData[] data;
    }
}