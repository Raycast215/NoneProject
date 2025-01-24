using System;
using NoneProject.Actor.Data;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2025.01.24
    // Player가 사용할 Stat을 관리하는 클래스입니다.
    [Serializable]
    public sealed class PlayerStat : StatBase
    {
        public PlayerStat(ActorStatData stat)
        {
            BaseStat = stat;

            Initialize();
        }
    }
}