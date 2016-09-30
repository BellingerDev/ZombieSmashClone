using Game;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Poolable;
using Utils.Saves;

namespace UI.Screens
{
    public class UIBattleResultScreen : UIScreen
    {
        [Serializable]
        private class BattleState
        {
            public Color windowColor;
            public GameObject title;
            public string starPrototypeId;
            public GameObject actionButton;
        }

        [SerializeField]
        private BattleState winState;

        [SerializeField]
        private BattleState loseState;

        [SerializeField]
        private Transform starsContainer;

        private Image windowImage;


        public override void Init()
        {
            base.Init();
            windowImage = GetComponent<Image>();

            OnShow += OnShowCallback;
        }

        public override void DeInit()
        {
            OnShow -= OnShowCallback;

            base.DeInit();
        }

        public void OnShowCallback()
        {
            ResetAll();

            string starID = string.Empty;

            switch (BattleController.Instance.Result)
            {
                case BattleController.BattleResult.Won:
                    winState.title.SetActive(true);
                    winState.actionButton.SetActive(true);

                    windowImage.color = winState.windowColor;
                    starID = winState.starPrototypeId;
                    break;

                case BattleController.BattleResult.Lost:
                    loseState.title.SetActive(true);
                    loseState.actionButton.SetActive(true);

                    windowImage.color = loseState.windowColor;
                    starID = loseState.starPrototypeId;
                    break;
            }

            FillStars(starID, SavesManager.Instance.GetLevelStars(LevelsController.Instance.GetActiveLevelId()));
        }

        private void ResetAll()
        {
            winState.title.SetActive(false);
            winState.actionButton.SetActive(false);

            loseState.title.SetActive(false);
            loseState.actionButton.SetActive(false);

            windowImage.color = Color.white;
        }

        private void FillStars(string starId, int count)
        {
            while (starsContainer.childCount > 0)
                Pool.Instance.Retrieve(starsContainer.GetChild(0).gameObject);

            for (int i = 0; i < count; i++)
            {
                GameObject startGO = Pool.Instance.Get(starId);

                startGO.transform.SetParent(starsContainer);

                startGO.transform.localPosition = Vector3.zero;
                startGO.transform.localScale = Vector3.one;
                startGO.transform.localEulerAngles = Vector3.zero;

                startGO.SetActive(true);
            }
        }

        public void OnRetryClicked()
        {
            GameController.Instance.SwitchState(GameController.GameState.Battle);
        }

        public void OnContinueClicked()
        {
            int activeLvlId = LevelsController.Instance.GetActiveLevelId();
            int maxLevel = LevelsController.Instance.GetLevelsCount() - 1;

            if (activeLvlId + 1 <= maxLevel)
            {
                LevelsController.Instance.SetActiveLevel(activeLvlId + 1);
                GameController.Instance.SwitchState(GameController.GameState.Battle);
            }
            else
                OnSelectLevelClicked();
        }

        public void OnSelectLevelClicked()
        {
            GameController.Instance.SwitchState(GameController.GameState.SelectLevel);
        }
    }
}
