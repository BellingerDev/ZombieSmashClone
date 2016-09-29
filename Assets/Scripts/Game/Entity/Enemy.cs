using Game.Data;
using System;
using UnityEngine;


namespace  Game.Entity
{
    public class Enemy : MonoBehaviour, IEntity, IMovableEntity
    {
        private int             health;
        private int             damage;
        private float           speed;

        public int Health       { get { return health; } }

        public Action<int>      OnDamageObtained { get; set; }
        public Action<int>      OnDamageTaken { get; set; }
        public Action           OnDied { get; set; }

        private EntityMover     mover;


        private void Awake()
        {
            mover = GetComponent<EntityMover>();
        }

        public void ConfigureBalance(object data)
        {
            EnemyData ed = data as EnemyData;
            if (ed != null)
            {
                health = ed.Health;
                damage = ed.Damage;
                speed = ed.Speed;
            }
        }

        public int TakeDamage()
        {
            if (OnDamageTaken != null)
                OnDamageTaken(damage);

            return damage;
        }

        public void ObtainDamage(int damage)
        {
            health -= damage;

            if (OnDamageObtained != null)
                OnDamageObtained(damage);

            if (health <= 0 && OnDied != null)
                OnDied();
        }

        public void Move(Vector3 position)
        {
            mover.Move(position, speed);
        }
    }
}
