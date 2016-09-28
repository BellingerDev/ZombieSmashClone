using Game.Entity;
using UnityEngine;


namespace Game
{
    public class TouchController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask raycastLayer;

        [SerializeField]
        private float raycastMaxDistance;

        private Camera gameCamera;
        private Player player;


        private void Awake()
        {
            gameCamera = FindObjectOfType<Camera>();
            player = FindObjectOfType<Player>();
        }

        private void OnDestroy()
        {
            gameCamera = null;
            player = null;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (player != null)
                {
                    Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, raycastMaxDistance, raycastLayer))
                    {
                        IEntity entity = (IEntity)hit.collider.GetComponent(typeof(IEntity));
                        if (entity != null)
                            entity.ObtainDamage(player.TakeDamage());
                    }
                }
            }
        }
    }
}
