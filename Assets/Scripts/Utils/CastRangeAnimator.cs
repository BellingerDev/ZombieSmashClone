using UnityEngine;


namespace Utils
{
    public class CastRangeAnimator : MonoBehaviour
    {
        [SerializeField]
        private LeanTweenType showHideTween;

        [SerializeField]
        private float rotateSpeed;

        [SerializeField]
        private float showHideSpeed;

        private Vector3 originScale;


        private void Awake()
        {
            originScale = this.transform.localScale;
        }

        private void OnEnable()
        {
            this.transform.localScale = Vector3.zero;

            LeanTween.scale(this.gameObject, originScale, showHideSpeed)
                .setEase(showHideTween)
                .setOnComplete(() => {
                    LeanTween.rotate(this.gameObject, this.transform.eulerAngles + new Vector3(0, 180, 0), rotateSpeed)
                    .setLoopPingPong();
                });
        }

        private void OnDisable()
        {
            LeanTween.cancel(this.gameObject);
        }
    }
}
