using UnityEngine;
using Utils.Poolable;


namespace Game.Areas
{
    public class PoolRetieveArea : MonoBehaviour
    {
        public void OnTriggerEnter(Collider col)
        {
            Pool.Instance.Retrieve(col.gameObject);
        }
    }
}
