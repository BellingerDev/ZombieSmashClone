using System;
using UnityEngine;


namespace Environment
{
    public class StreetLampController : MonoBehaviour
    {
        [Serializable]
        private class TimePowerIntensity
        {
            public float time;
            public bool state;
        }

        [SerializeField]
        private GameObject cone;

        [SerializeField]
        private TimePowerIntensity[] phases;

        private int active = -1;
        private float startTime;


        private void Update()
        {
            if (active == -1)
            {
                active = 0;
                startTime = Time.time;
            }

            if ((Time.time - startTime) > phases[active].time)
            {
                cone.SetActive(phases[active].state);

                if (++active > phases.Length - 1)
                    active = -1;
            }
        }
    }
}
