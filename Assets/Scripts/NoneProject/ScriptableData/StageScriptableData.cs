using NoneProject.Stage.Data;
using UnityEngine;

namespace NoneProject.ScriptableData
{
    // Scripted by Raycast
    // 2025.02.04
    // Stage에 필요한 데이터를 담은 Scriptable Object 클래스입니다.
    [CreateAssetMenu(fileName = "StageScriptableData", menuName = "Scriptable Object/Stage Data")]
    public class StageScriptableData : ScriptableObject
    {
        public StageData[] datas;
    }
}