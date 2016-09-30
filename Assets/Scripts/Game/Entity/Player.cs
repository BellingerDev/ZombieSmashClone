using Game.Areas;
using Game.Data;
using Game.Entity;
using System;
using UnityEngine;


namespace Game
{
    public class Player : MonoBehaviour, IEntity, IExplodable
    {
        private int health;
        private int damage;

        private string bombId;
        private int bombsCount;
        private int bombDamage;

        public int Health                       { get { return health; } }

        public string BombId                    { get { return bombId; } }
        public int BombsCount                   { get { return bombsCount; } }
        public int BombDamage                   { get { return bombDamage; } }

        public Action OnDied                    { get; set; }
        public Action<int> OnDamageObtained     { get; set; }
        public Action<int> OnDamageTaken        { get; set; }


        public void ConfigureBalance(object data)
        {
            PlayerData pd = data as PlayerData;
            if (pd != null)
            {
                health = pd.Health;
                damage = pd.Damage;

                bombId = pd.BombId;
                bombsCount = pd.BombsCount;
                bombDamage = pd.BombDamage;
            }
        }

        public void ObtainDamage(int damage)
        {
            health -= damage;

            if (OnDamageObtained != null)
                OnDamageObtained(damage);

            if (health <= 0 && OnDied != null)
                OnDied();
        }

        public int TakeDamage()
        {
            if (OnDamageTaken != null)
                OnDamageTaken(damage);

            return damage;
        }

        public void ExplodeBomb()
        {
            bombsCount--;
        }

        private void Awake()
        {
            FindObjectOfType<PlayerHitArea>().OnDamageTaken += ObtainDamage;
        }

        private void OnDestroy()
        {
            //FindObjectOfType<PlayerHitArea>().OnDamageTaken -= ObtainDamage;
        }
    }
}
