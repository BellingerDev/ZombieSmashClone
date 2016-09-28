using System;
using UnityEngine;


namespace Game.Entity
{
    public class EntityMoveRandomizer : MonoBehaviour
    {
        private enum MoveDirection
        {
            Left, Right
        }

        [Serializable]
        private class MoveRandomizeStep
        {
            public float time;
            public float timeScatter;

            public MoveDirection direction;
            public bool isRandomDirection;
        }

        [SerializeField]
        private MoveRandomizeStep[] steps;

        private EntityMover mover;
        private float startTime;
        private int currentStep;


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
            startTime = Time.time;
            currentStep = 0;
        }

        private void OnDisable()
        {
            startTime = -1;
        }

        private void Update()
        {
            MoveRandomizeStep step = steps[currentStep];


        }
    }
}

