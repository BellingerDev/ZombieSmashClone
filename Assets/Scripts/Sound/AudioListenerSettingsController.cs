using UnityEngine;
using Utils.Saves;


namespace Sound
{
    public class AudioListenerSettingsController : MonoBehaviour
    {
        private AudioListener listener;


        private void Awake()
        {
            listener = GetComponent<AudioListener>();
        }

        private void Update()
        {
            listener.enabled = SavesManager.Instance.Settings.sound;
        }
    }
}
