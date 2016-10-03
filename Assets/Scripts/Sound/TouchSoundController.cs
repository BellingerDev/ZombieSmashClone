using Game;
using UnityEngine;


namespace Sound
{
    public class TouchSoundController : MonoBehaviour
    {
        private AudioSource source;


        private void Awake()
        {
            source = GetComponent<AudioSource>();
            GetComponent<TouchController>().OnEnemyClicked += OnEnemyClicked;
        }

        private void OnDestroy()
        {
            GetComponent<TouchController>().OnEnemyClicked -= OnEnemyClicked;
        }
        
        private void OnEnemyClicked()
        {
            source.Play();
        }
    }
}
