using NoneProject.Actor.Data;
using UnityEngine;

namespace NoneProject.ScriptableData
{
    // Scripted by Raycast
    // 2025.01.25
    // Player의 Stat 데이터를 담은 Scriptable Object 클래스입니다.
    [CreateAssetMenu(fileName = "PlayerStatScriptableData", menuName = "Scriptable Object/Player Stat Data")]
    public class PlayerStatScriptableData : ScriptableObject
    {
        public PlayerStatData[] data;
    }
}