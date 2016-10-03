using System;
using UnityEngine;


namespace Environment
{
    public class NeonLampController : MonoBehaviour
    {
        [Serializable]
        private class TimePowerIntensity
        {
            public float time;
            public float power;
        }

        [SerializeField]
        private Material material;

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
                material.SetColor("_EmissionColor", material.color * Mathf.LinearToGammaSpace(phases[active].power));
                
                if (++active > phases.Length - 1)
                    active = -1;
            }
        }
    }
}
