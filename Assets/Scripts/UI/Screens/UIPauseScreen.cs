using Game;
using UnityEngine;
using UnityEngine.UI;
using Utils.Saves;

namespace UI.Screens
{
    public class UIPauseScreen : UIScreen
    {
        [SerializeField]
        private Toggle soundToggle;

        [SerializeField]
        private Toggle vibrateToggle;


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

        public void OnRestartClicked()
        {
            GameController.Instance.SwitchState(GameController.GameState.Battle);
            BattleController.Instance.RestartBattle();
        }

        public void OnSelectLevelClicked()
        {
            GameController.Instance.SwitchState(GameController.GameState.SelectLevel);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }

        public void OnSoundToggle()
        {
            SavesManager.Instance.Settings.sound = soundToggle.isOn;
        }

        public void OnVibrateToggle()
        {
            SavesManager.Instance.Settings.vibrate = vibrateToggle.isOn;
        }

        public void OnShowCallback()
        {
            soundToggle.isOn = SavesManager.Instance.Settings.sound;
            vibrateToggle.isOn = SavesManager.Instance.Settings.vibrate;
        }
    }
}
