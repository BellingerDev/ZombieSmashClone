using UnityEngine;


namespace Game.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = -1)]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private BattleData battle;

        [SerializeField]
        private PlayerData player;

        [SerializeField]
        private EnemyData enemyCreep;

        [SerializeField]
        private EnemyData enemyZigZagGuy;

        [SerializeField]
        private EnemyData enemyDengerous;


        public BattleData Battle            { get { return battle; } }

        public PlayerData Player            { get { return player; } }

        public EnemyData EnemyCreep         { get { return enemyCreep; } }
        public EnemyData EnemyZigZagGuy     { get { return enemyZigZagGuy; } }
        public EnemyData EnemyDengerous     { get { return enemyDengerous; } }
    }
}
