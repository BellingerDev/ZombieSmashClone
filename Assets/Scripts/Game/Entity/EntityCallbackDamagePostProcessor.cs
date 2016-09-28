using UnityEngine;


namespace Game.Entity
{
    public class EntityCallbackDamagePostProcessor : MonoBehaviour
    {
        [SerializeField]
        private int callbackDamage;

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
            entity.OnDied += OnDied;
            player = (IEntity)FindObjectOfType<Player>();
        }

        private void OnDisable()
        {
            entity.OnDied -= OnDied;
            player = null;
        }

        private void OnDied()
        {
            player.ObtainDamage(callbackDamage);
        }
    }
}
