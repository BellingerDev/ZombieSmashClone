using Game.Data;
using System;
using UnityEngine;


namespace  Game.Entity
{
    public class Enemy : MonoBehaviour, IEntity
    {
        [SerializeField]
        private EnemyData data;

        private int health;
        private int damage;

        public Action OnDied { get; set; }


        public int TakeDamage()
        {
            return damage;
        }

        public void ObtainDamage(int damage)
        {
            health -= damage;
            if (health <= 0 && OnDied != null)
                OnDied();
        }

        private void OnEnable()
        {
            health = data.Health;
            damage = data.Damage;
        }
    }
}
