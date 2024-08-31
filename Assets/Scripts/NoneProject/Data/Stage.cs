
using System;
using NoneProject.Common;


namespace NoneProject.Data
{
    [Serializable]
    public class Stage
    {
        public int index;
        public string mapType;
        public int[] spawnCounts;
        public string bossKey;
        public int rewardGold;
    }
}