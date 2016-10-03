using UnityEngine;


namespace Game.Entity
{
    public class EntityMoveRandomizer : MonoBehaviour, IEntitySpawnLimits
    {
        [SerializeField]
        private float time;

        [SerializeField]
        private float timeScatter;

        [SerializeField]
        private float randomStepSize;

        [SerializeField]
        private float randomMoveSpeed;

        private EntityMover mover;
        private float randomStepTime;
        private bool isRandomed;

        public Vector3 LeftLimit { get; set; }
        public Vector3 RightLimit { get; set; }


        private void Awake()
        {
            mover = GetComponent<EntityMover>();
        }

        private void OnDestroy()
        {
            mover = null;
        }

        private void OnEnable()
        {
            randomStepTime = (Time.time + time) + Random.Range(-timeScatter, timeScatter);
            isRandomed = false;
        }

        private void Update()
        {
            if (BattleController.Instance.IsPaused)
                return;

            if (Time.time > randomStepTime && !isRandomed)
            {
                isRandomed = true;
                mover.Clear();

                Vector3 stepPos = transform.position + new Vector3(Random.Range(0, 1) == 0 ? LeftLimit.x : RightLimit.x, transform.position.y, -randomStepSize);
                Vector3 targetPos = new Vector3(stepPos.x, transform.position.y, LeftLimit.z);

                mover.Move(stepPos, randomMoveSpeed);
                mover.Move(targetPos, randomMoveSpeed);
            }
        }
    }
}

