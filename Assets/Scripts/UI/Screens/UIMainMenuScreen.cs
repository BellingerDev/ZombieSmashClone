using Game;
using UnityEngine;
using Utils.Saves;


namespace UI.Screens
{
    public class UIMainMenuScreen : UIScreen
    {
        [SerializeField]
        private GameObject continueButton;


        public override void Init()
        {
            base.Init();
            OnShow += OnShowCallback;
        }

        public override void DeInit()
        {
            OnShow -= OnShowCallback;
            base.DeInit();
        }

        private void OnShowCallback()
        {
            continueButton.SetActive(SavesManager.Instance.GetLevelCompleted(0));
        }

        public void OnContinueClicked()
        {
            LevelsController.Instance.SetActiveLevel(SavesManager.Instance.GetActiveLevel());
            GameController.Instance.SwitchState(GameController.GameState.Battle);
        }

        public void OnNewGameClicked()
        {
            SavesManager.Instance.ResetSaves();
            LevelsController.Instance.SetActiveLevel(0);
            GameController.Instance.SwitchState(GameController.GameState.Battle);
        }

        public void OnSelectLevelClicked()
        {
            GameController.Instance.SwitchState(GameController.GameState.SelectLevel);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}
