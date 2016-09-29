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
                [SerializeField]
                private string enemy;

                [SerializeField]
                private int count;

                public string Enemy     { get { return enemy; } }
                public int Count        { get { return count; } }
            }

            [SerializeField]
            private float time;

            [SerializeField]
            private EnemyUnit[] enemies;

            public float Time           { get { return time; } }
            public EnemyUnit[] Enemies  { get { return enemies; } }
        }

        [SerializeField]
        private Wave[] waves;

        [SerializeField]
        private float battleTime;

        public Wave[] Waves             { get { return waves; } }
        public float BattleTime         { get { return battleTime; } }
    }
}
