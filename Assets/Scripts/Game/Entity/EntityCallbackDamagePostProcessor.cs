using UnityEngine;


namespace Game.Entity
{
    public class EntityCallbackDamagePostProcessor : MonoBehaviour
    {
        [SerializeField]
        private int kickDamage;
      
        private IEntity entity;
        private IEntity player;


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            entity = (IEntity)GetComponent(typeof(IEntity));
        }

        private void OnDestroy()
        {
            entity = null;
        }

        private void OnEnable()
        {
            entity.OnDamageObtained += OnDamageObtained;

            player = (IEntity)FindObjectOfType<Player>();
        }

        private void OnDisable()
        {
            entity.OnDamageObtained -= OnDamageObtained;

            player = null;
        }

        private void OnDamageObtained(int damage)
        {
            player.ObtainDamage(kickDamage);
        }
    }
}
