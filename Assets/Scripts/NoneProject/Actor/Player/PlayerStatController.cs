using System;
using NoneProject.Actor.Data;
using NoneProject.Actor.Stat;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2025.01.24
    // Player가 사용할 Stat을 관리하는 클래스입니다.
    [Serializable]
    public class PlayerStatController : StatBase
    {
        public PlayerStatController(PlayerStatData statData)
        {
            DataBaseStatData = statData.dataBaseStatData;
            
            base.Initialize();
        }
    }
}