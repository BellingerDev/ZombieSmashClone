using UnityEngine;
using Utils.Poolable;


namespace Game.Areas
{
    public class ObjectsContainer : MonoBehaviour
    {
        public enum ContainerType
        {
            Entities,
            Decals
        }

        [SerializeField]
        private Transform entitiesRoot;

        [SerializeField]
        private Transform decalsRoot;


        public void Add(ContainerType type, GameObject go)
        {
            switch (type)
            {
                case ContainerType.Entities:
                    go.transform.SetParent(entitiesRoot);
                    break;

                case ContainerType.Decals:
                    go.transform.SetParent(decalsRoot);
                    break;
            }
        }

        public void Clear()
        {
            while (entitiesRoot.childCount > 0)
                Pool.Instance.Retrieve(entitiesRoot.GetChild(0).gameObject);
            
            while (decalsRoot.childCount > 0)
                Pool.Instance.Retrieve(decalsRoot.GetChild(0).gameObject);
        }
    }
}
