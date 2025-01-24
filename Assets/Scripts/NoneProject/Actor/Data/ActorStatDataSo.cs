using UnityEngine;

namespace NoneProject.Actor.Data
{
    // Scripted by Raycast
    // 2025.01.25
    // Player가 사용할 Stat SO 클래스입니다.
    [CreateAssetMenu(fileName = "Stat Data", menuName = "Scriptable Object/Actor Stat Data")]
    public class PlayerStatSo : ScriptableObject
    {
        public ActorStatData[] data;
    }
}