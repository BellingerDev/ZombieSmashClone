using UnityEngine;


namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 2)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private string  prototypeID;

        [SerializeField]
        private int     health;

        [SerializeField]
        private int     damage;

        [SerializeField]
        private float   speed;

        public string   PrototypeId     { get { return prototypeID; } }
        public int      Health          { get { return health; } }
        public int      Damage          { get { return damage; } }
        public float    Speed           { get { return speed; } }
    }
}
