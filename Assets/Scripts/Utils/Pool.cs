using System;
using System.Collections.Generic;
using UnityEngine;


namespace Utils.Poolable
{
    public class Pool : MonoBehaviour
    {
        #region Define Instance
        private static WeakReference instance;
        public static Pool Instance
        {
            get
            {
                if (instance == null)
                    instance = new WeakReference(GameObject.FindObjectOfType<Pool>());
                else if (instance.Target == null)
                    instance.Target = GameObject.FindObjectOfType<Pool>();

                return (Pool)instance.Target;
            }
        }
        #endregion

        [Serializable]
        private class PoolElement
        {
            [HideInInspector]
            public List<GameObject> instances = new List<GameObject>();

            public GameObject prototype;
            public int count;

            private Transform parent;


            public void Init(Transform parent)
            {
                this.parent = parent;

                for (int i = 0; i < count; i++)
                    instances.Add(CreateInstance(parent));
            }

            private GameObject CreateInstance(Transform parent)
            {
                GameObject instance = Instantiate<GameObject>(prototype);

                instance.transform.SetParent(parent);
                instance.SetActive(false);
                instance.name = instance.name.Replace("(Clone)", "");

                return instance;
            }

            public void DeInit()
            {
                instance = null;
            }

            public GameObject Get()
            {
                if (instances.Count > 0)
                {
                    GameObject instance = instances[0];
                    instances.RemoveAt(0);

                    return instance;
                }

                return CreateInstance(parent);
            }

            public void Retrieve(GameObject instance)
            {
                instance.transform.SetParent(parent);
                instances.Add(instance);
                instance.SetActive(false);
            }
        }

        [SerializeField]
        private PoolElement[] elements;


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            foreach (var e in elements)
                e.Init(this.transform);
        }

        private void OnDestroy()
        {
            foreach (var e in elements)
                e.DeInit();
        }

        public GameObject Get(string prototype)
        {
            PoolElement element = Array.FindLast<PoolElement>(elements, e => e.prototype.name == prototype);
            if (element != null)
                return element.Get();

            return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        public void Retrieve(GameObject instance)
        {
            PoolElement element = Array.FindLast<PoolElement>(elements, e => e.prototype.name == instance.name);
            if (element != null)
                element.Retrieve(instance);
            else
                Destroy(instance);
        }
    }
}
