using Game.Draggable;
using Game.Entity;
using UnityEngine;


namespace Game
{
    public class TouchController : MonoBehaviour
    {
        public static IDraggable DragObject { get; set; }

        [SerializeField]
        private LayerMask raycastLayer;

        [SerializeField]
        private float raycastMaxDistance;

        [SerializeField]
        private string gameCameraTag;

        [SerializeField]
        private string groundTag;

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
            if (BattleController.Instance.IsPaused)
                return;

            if (DragObject == null)
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
                    else
                        player = FindObjectOfType<Player>();
                }
            }
            else
            {
                bool isCasted = false;

                Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastMaxDistance, raycastLayer))
                {
                    DragObject.SetPos(hit.point);
                    isCasted = true;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (isCasted)
                        DragObject.Activate();
                    else
                        DragObject.Deactivate();

                    DragObject = null;
                }
            }
        }
    }
}
