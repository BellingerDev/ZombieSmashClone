using Game.Entity;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Game.Areas
{
    public class EntitiesSpawnArea : MonoBehaviour, IEntitySpawnLimits
    {
        [SerializeField]
        private float spawnRange;

        private ObjectsContainer container;
        private PoolRetieveArea poolArea;

        public Vector3 LeftLimit
        {
            get { return poolArea.transform.position + new Vector3(-spawnRange, 0, 0); }
            set { }
        }

        public Vector3 RightLimit
        {
            get { return poolArea.transform.position + new Vector3(spawnRange, 0, 0); }
            set { }
        }


        public void Init()
        {
            container = FindObjectOfType<ObjectsContainer>();
            poolArea = FindObjectOfType<PoolRetieveArea>();
        }

        public void DeInit()
        {
            container = null;
            poolArea = null;
        }

        public void Spawn(GameObject entity, object entityBalanceData)
        {
            Vector3 spawnPosition = this.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, 0);

            container.Add(ObjectsContainer.ContainerType.Entities, entity);

            IEntity ie = (IEntity)entity.GetComponent(typeof(IEntity));
            if (ie != null)
                ie.ConfigureBalance(entityBalanceData);

            IMovableEntity me = (IMovableEntity)entity.GetComponent(typeof(IMovableEntity));
            if (me != null)
                me.Move(new Vector3(spawnPosition.x, spawnPosition.y, poolArea.transform.position.z));

            IEntitySpawnLimits limits = (IEntitySpawnLimits)entity.GetComponent(typeof(IEntitySpawnLimits));
            if (limits != null)
            {
                limits.LeftLimit = LeftLimit;
                limits.RightLimit = RightLimit;
            }

            entity.transform.position = spawnPosition;
            entity.transform.forward = this.transform.forward;

            entity.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Vector3 leftLimit = transform.position + new Vector3(-spawnRange, 0, 0);
            Vector3 rightLimit = transform.position + new Vector3(spawnRange, 0, 0);

            Gizmos.DrawRay(transform.position, transform.forward);
            Gizmos.DrawLine(leftLimit, rightLimit);

            Gizmos.DrawSphere(leftLimit, 0.1f);
            Gizmos.DrawSphere(rightLimit, 0.1f);
        }
    }
}
