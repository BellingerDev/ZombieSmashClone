using Game.Areas;
using UnityEngine;
using Utils.Poolable;


namespace Game.Entity
{
    public class EntityDiePostProcessor : MonoBehaviour
    {
        [SerializeField]
        private string diedEntityPoolId;

        private IEntity entity;
        private ObjectsContainer container;


        private void Awake()
        {
            entity = (IEntity)GetComponent(typeof(IEntity));
            container = FindObjectOfType<ObjectsContainer>();
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
            
            container.Add(ObjectsContainer.ContainerType.Decals, deadBody);

            deadBody.transform.position = transform.position;
            deadBody.SetActive(true);

            Pool.Instance.Retrieve(this.gameObject);
        }
    }
}
