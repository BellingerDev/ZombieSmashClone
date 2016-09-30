using UI.Screens;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Common
{
    public class UILevelIcon : MonoBehaviour
    {
        [SerializeField]
        private Text value;

        [SerializeField]
        private GameObject[] starIconsPositive;
        
        [SerializeField]
        private GameObject[] starIconsNegative;

        private Image image;

        private int levelId;
        private int starsCount;
        private bool isLevelCompleted;
        private bool isLevelAvailable;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Set(int id, int stars, bool isCompleted, bool isAvailable)
        {
            value.text = (id + 1).ToString();

            levelId = id;
            starsCount = stars;
            isLevelCompleted = isCompleted;
            isLevelAvailable = isAvailable;

            foreach (var s in starIconsPositive)
                s.SetActive(false);

            foreach (var s in starIconsNegative)
                s.SetActive(false);

            GameObject[] icons = isCompleted ? starIconsPositive : starIconsNegative;

            for (int i = 0; i < icons.Length; i++)
            {
                if (stars <= icons.Length && i < stars)
                    icons[i].SetActive(true);
            }

            image.color = !isAvailable ? Color.grey : Color.white;
        }

        public void OnClicked()
        {
            if (isLevelCompleted || isLevelAvailable)
                if (UISelectLevelScreen.OnLevelIconClicked != null)
                    UISelectLevelScreen.OnLevelIconClicked(levelId);
        }
    }
}
