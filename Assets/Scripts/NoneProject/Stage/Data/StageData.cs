using System;

namespace NoneProject.Stage.Data
{
    [Serializable]
    public class StageData
    {
        public string id;
        public float intervalTime;
        public float second;
        public int maxSpawnCount;
        public EnemySpawnData[] datas;
    }

    [Serializable]
    public class EnemySpawnData
    {
        public string enemyId;
        public int spawnCount;
        public int weight;
    }
}