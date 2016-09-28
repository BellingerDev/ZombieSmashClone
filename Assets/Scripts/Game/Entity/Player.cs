using Game.Areas;
using Game.Data;
using Game.Entity;
using System;
using UnityEngine;


namespace Game
{
    public class Player : MonoBehaviour, IEntity
    {
        [SerializeField]
        private PlayerData data;

        private int health;
        private int damage;
        private int bombsCount;

        public Action OnDied { get; set; }


        public void ObtainDamage(int damage)
        {
            health -= damage;
            if (health < 0 && OnDied != null)
                OnDied();
        }

        public int TakeDamage()
        {
            return damage;
        }

        private void Awake()
        {
            FindObjectOfType<PlayerHitArea>().OnDamageTaken += ObtainDamage;
        }

        private void OnDestroy()
        {
            FindObjectOfType<PlayerHitArea>().OnDamageTaken -= ObtainDamage;
        }

        private void OnEnable()
        {
            health = data.Health;
            damage = data.Damage;
            bombsCount = data.BombsCount;
        }
    }
}
