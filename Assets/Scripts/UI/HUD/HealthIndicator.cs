using Game;
using Game.Entity;
using UnityEngine;
using Utils.Poolable;

namespace UI.HUD
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField]
        private string healthPointPoolId;

        private IEntity player;


        private void Update()
        {
            if (player != null)
                SetCount(player.Health);
            else
                player = FindObjectOfType<Player>() as IEntity;
        }

        public void SetCount(int count)
        {
            while (transform.childCount > 0)
                Pool.Instance.Retrieve(this.transform.GetChild(0).gameObject);

            if (transform.childCount > count)
                while (transform.childCount > count)
                    Pool.Instance.Retrieve(this.transform.GetChild(this.transform.childCount - 1).gameObject);

            if (transform.childCount < count)
                while (transform.childCount < count)
                {
                    GameObject point = Pool.Instance.Get(healthPointPoolId);

                    point.transform.SetParent(this.transform);
                    point.transform.localPosition = Vector3.zero;
                    point.transform.localScale = Vector3.one;
                    point.transform.localEulerAngles = Vector3.zero;
                    point.SetActive(true);
                }
        }
    }
}
