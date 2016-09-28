using UnityEngine;


namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 2)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private int health;

        [SerializeField]
        private int damage;

        public int Health { get { return health; } }
        public int Damage { get { return damage; } }
    }
}
