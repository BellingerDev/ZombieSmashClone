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
        private EnemyData[] enemies;


        public BattleData Battle            { get { return battle; } }

        public PlayerData Player            { get { return player; } }

        public EnemyData[] Enemies          { get { return enemies; } }
    }
}
