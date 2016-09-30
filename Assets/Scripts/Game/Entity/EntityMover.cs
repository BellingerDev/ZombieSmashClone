using System.Collections.Generic;
using UnityEngine;


namespace Game.Entity
{
    public class EntityMover : MonoBehaviour
    {
        private class MoveData
        {
            public Vector3 Position { get; private set; }
            public float Speed { get; private set; }

            public MoveData(Vector3 pos, float s)
            {
                Position = pos;
                Speed = s;
            }
        }

        [SerializeField]
        private float               accuracy;

        private Queue<MoveData>     moves = new Queue<MoveData>();
 

        public void Move(Vector3 position, float speed)
        {
            moves.Enqueue(new MoveData(position, speed));
        }

        public void Clear()
        {
            moves.Clear();
        }

        private void Update()
        {
            if (BattleController.Instance.IsPaused)
                return;

            if (moves.Count > 0)
            {
                MoveData md = moves.Peek();
                if (md != null)
                {
                    if ((md.Position - transform.position).magnitude > accuracy)
                        transform.position = Vector3.MoveTowards(transform.position, md.Position, md.Speed * Time.deltaTime);
                    else
                        moves.Dequeue();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            MoveData[] mds = moves.ToArray();

            for (int i = 0; i < mds.Length; i++)
            {
                if (i + 1 < mds.Length - 1)
                    Gizmos.DrawLine(mds[i].Position + new Vector3(0, 1, 0), mds[i + 1].Position + new Vector3(0, 1, 0));

                Gizmos.DrawSphere(mds[i].Position + new Vector3(0, 1, 0), 0.1f);
            }
        }
    }
}

