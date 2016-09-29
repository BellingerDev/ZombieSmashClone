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

        [SerializeField]
        private string gameCameraTag;

        private Camera gameCamera;
        private Player player;


        private void Awake()
        {
            gameCamera = GameObject.FindGameObjectWithTag(gameCameraTag).GetComponent<Camera>();
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
                    Debug.DrawRay(ray.origin, ray.direction);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, raycastMaxDistance))
                    {
                        Debug.Log("TouchController : Hit : " + hit.collider.name);

                        IEntity entity = (IEntity)hit.collider.GetComponent(typeof(IEntity));
                        if (entity != null)
                            entity.ObtainDamage(player.TakeDamage());
                    }
                }
            }
        }
    }
}
