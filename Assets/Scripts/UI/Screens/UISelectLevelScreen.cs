using UnityEngine;
using Game;
using Utils.Poolable;
using UI.Common;
using Utils.Saves;
using System;

namespace UI.Screens
{
    public class UISelectLevelScreen : UIScreen
    {
        [SerializeField]
        private string levelIconPrototypeId;

        [SerializeField]
        private Transform levelsContainer;

        public static Action<int> OnLevelIconClicked { get; set; }


        public override void Init()
        {
            base.Init();

            OnShow += OnShowCallback;
            OnHide += OnHideCallback;
        }

        public override void DeInit()
        {
            base.DeInit();

            OnShow -= OnShowCallback;
            OnHide -= OnHideCallback;
        }

        public void OnShowCallback()
        {
            LoadLevels();
            OnLevelIconClicked += OnLevelClicked;
        }

        public void OnHideCallback()
        {
            OnLevelIconClicked -= OnLevelClicked;
            ClearLevels();
        }

        private void LoadLevels()
        {
            for (int i = 0; i < LevelsController.Instance.GetLevelsCount(); i++)
            {
                GameObject iconGO = Pool.Instance.Get(levelIconPrototypeId);
                iconGO.transform.SetParent(levelsContainer);

                iconGO.transform.localPosition = Vector3.zero;
                iconGO.transform.localScale = Vector3.one;
                iconGO.transform.localEulerAngles = Vector3.zero;

                UILevelIcon icon = iconGO.GetComponent<UILevelIcon>();

                icon.Set(i, SavesManager.Instance.GetLevelStars(i));

                iconGO.SetActive(true);
            }
        }

        private void ClearLevels()
        {
            while (levelsContainer.childCount > 0)
                Pool.Instance.Retrieve(levelsContainer.GetChild(0).gameObject);
        }

        private void OnLevelClicked(int id)
        {
            LevelsController.Instance.SetActiveLevel(id);
            GameController.Instance.SwitchState(GameController.GameState.Battle);
        }
    }
}
