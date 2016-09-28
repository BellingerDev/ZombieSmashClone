using System;
using UnityEngine;


namespace Game.Data
{
    [CreateAssetMenu(fileName ="BattleData", menuName ="Data/BattleData", order=0)]
    public class BattleData : ScriptableObject
    {
        [Serializable]
        public class Wave
        {
            [Serializable]
            public class EnemyUnit
            {
                public string enemy;
                public int count;
            }


            public float time;
            public float timeScatter;
            public EnemyUnit[] enemies;
        }

        public Wave[] waves;
        public float battleTime;
    }
}
