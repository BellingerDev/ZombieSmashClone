using UnityEngine;


namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private int health;

        [SerializeField]
        private int damage;
        
        [SerializeField]
        private int bombsCount;

        public int Health       { get { return health; } }
        public int Damage       { get { return damage; } }
        public int BombsCount   { get { return bombsCount; } }
    }
}
