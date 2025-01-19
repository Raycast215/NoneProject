using NoneProject.Actor.Data;
using UnityEngine;

namespace NoneProject.Actor.Stat
{
    [CreateAssetMenu(fileName = "Stat Data", menuName = "Scriptable Object/Stat Data")]
    public class StatBase : ScriptableObject
    {
        public ActorStat[] stats;
    }
}