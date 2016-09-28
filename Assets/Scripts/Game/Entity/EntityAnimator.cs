using UnityEngine;


namespace Game.Entity
{
    public class EntityAnimator : MonoBehaviour
    {
        private Animator animator;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            animator = null;
        }
    }
}
