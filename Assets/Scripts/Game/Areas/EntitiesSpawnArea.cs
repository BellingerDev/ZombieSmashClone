using Game.Entity;
using UnityEngine;


namespace Game.Areas
{
    public class EntitiesSpawnArea : MonoBehaviour
    {
        [SerializeField]
        private Vector3 spawnRange;

        private ObjectsContainer container;
        private PoolRetieveArea poolArea;


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
            container.Add(ObjectsContainer.ContainerType.Entities, entity);

            IEntity ie = (IEntity)entity.GetComponent(typeof(IEntity));
            if (ie != null)
                ie.ConfigureBalance(entityBalanceData);

            IMovableEntity me = (IMovableEntity)entity.GetComponent(typeof(IMovableEntity));
            if (me != null)
                me.Move(poolArea.transform.position); // need fix

            entity.transform.position = this.transform.position;
            entity.transform.forward = this.transform.forward;

            entity.SetActive(true);
        }
    }
}
