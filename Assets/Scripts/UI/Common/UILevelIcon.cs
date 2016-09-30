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
        private GameObject[] starIcons;

        private Image image;

        private int levelId;
        private int starsCount;


        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Set(int id, int stars)
        {
            value.text = (id + 1).ToString();

            levelId = id;
            starsCount = stars;

            foreach (var s in starIcons)
                s.SetActive(false);

            for (int i = 0; i < starIcons.Length; i++)
            {
                if (stars <= starIcons.Length && stars <= i && stars < 0)
                    starIcons[i].SetActive(true);
            }

            image.color = stars == 0 && levelId > 1 ? Color.grey : Color.white;
        }

        public void OnClicked()
        {
            if (starsCount > 0 && levelId > 1)
                if (UISelectLevelScreen.OnLevelIconClicked != null)
                    UISelectLevelScreen.OnLevelIconClicked(levelId);
        }
    }
}
