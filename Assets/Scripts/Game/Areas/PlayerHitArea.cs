using Game.Entity;
using System;
using System.Linq;
using UnityEngine;


namespace Game.Areas
{
    public class PlayerHitArea : MonoBehaviour
    {
        [SerializeField]
        private string[] enemyTags;

        public Action<int> OnDamageTaken;


        private void OnTriggerEnter(Collider col)
        {
            if (enemyTags.Contains(col.tag))
            {
                Enemy e = col.GetComponent<Enemy>();
                if (e != null && OnDamageTaken != null)
                    OnDamageTaken(e.TakeDamage());
            }
        }
    }
}
