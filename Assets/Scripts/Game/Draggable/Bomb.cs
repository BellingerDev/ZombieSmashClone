using Game.Entity;
using UnityEngine;


namespace Game.Draggable
{
    public class Bomb : MonoBehaviour, IDraggable
    {
        private bool isActivated = false;

        [SerializeField]
        private GameObject explosion;

        [SerializeField]
        private GameObject castRangeCircle;

        [SerializeField]
        private float damageDuration;

        private int damage;
        private float radius;
        private float activatorTime;


        public void Setup(int damage, float radius)
        {
            this.damage = damage;
            this.radius = radius;
        }

        public void Prepare()
        {
            castRangeCircle.SetActive(true);
            explosion.SetActive(false);
        }

        public void Activate()
        {
            FindObjectOfType<Player>().ExplodeBomb();

            castRangeCircle.SetActive(false);
            explosion.SetActive(true);
            isActivated = true;
            activatorTime = Time.time;

            Debug.Log("Bomb : Activated");
        }

        public void Deactivate()
        {
            castRangeCircle.SetActive(false);
            explosion.SetActive(false);

            Debug.Log("Bomb : Deactivated");
        }

        public void SetPos(Vector3 position)
        {
            this.transform.position = position;
        }

        private void OnTriggerStay(Collider col)
        {
            if (!isActivated)
                return;


            IEntity entity = (IEntity)col.GetComponent(typeof(IEntity));
            if (entity != null)
            {
                Debug.Log("Exploded : " + col.name + " obtain : " + damage.ToString());
                entity.ObtainDamage(damage);
            }
        }

        private void Update()
        {
            if (Time.time > activatorTime + damageDuration)
                isActivated = false;
        }
    }
}
