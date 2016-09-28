using UnityEngine;
using Utils.Poolable;


namespace Game.Entity
{
    public class EntityDiePostProcessor : MonoBehaviour
    {
        [SerializeField]
        private string diedEntityPoolId;

        private IEntity entity;


        private void Awake()
        {
            entity = (IEntity)GetComponent(typeof(IEntity));
        }

        private void OnDestroy()
        {
            entity = null;
        }

        private void OnEnable()
        {
            entity.OnDied += OnDiedCallback;
        }

        private void OnDisable()
        {
            entity.OnDied -= OnDiedCallback;
        }

        private void OnDiedCallback()
        {
            GameObject deadBody = Pool.Instance.Get(diedEntityPoolId);
            deadBody.transform.SetParent(null);
            deadBody.SetActive(true);

            Pool.Instance.Retrieve(this.gameObject);
        }
    }
}
