using UnityEngine;

namespace NoneProject.Data
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Scriptable Object/Game Data")]
    public class GameData : ScriptableObject
    {
        public string playerId;
    }
}