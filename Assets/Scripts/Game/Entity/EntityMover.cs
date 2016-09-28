using System;
using UnityEngine;


namespace Game.Entity
{
    public class EntityMover : MonoBehaviour
    {
        [SerializeField]
        private Vector3[] targets;

        [SerializeField]
        private float accuracy;

        public Action OnFinished { get; set; }

        private int current;
        private bool isFinish;


        private void OnEnable()
        {
            current = 0;
            isFinish = false;
        }

        private void OnDisable()
        {
            OnFinished = null;
        }

        private void Update()
        {
            if (!isFinish)
            {
                if (current < targets.Length)
                {
                    if ((targets[current] - transform.position).magnitude > accuracy)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, targets[current], Time.deltaTime);
                    }
                    else
                    {
                        if (++current >= targets.Length)
                        {
                            isFinish = true;

                            if (OnFinished != null)
                                OnFinished();
                        }
                    }
                }
            }
        }

        public void SetFinish(bool state)
        {
            isFinish = state;
        }
    }
}

