using System;
using UI;
using UI.Screens;
using UnityEngine;
using Utils.Poolable;
using Utils.Saves;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        #region Singleton Define
        private static WeakReference instance;

        public static GameController Instance
        {
            get
            {
                if (instance == null || instance.Target == null)
                    instance = new WeakReference(FindObjectOfType<GameController>());

                return instance.Target as GameController;
            }
        }

        private GameController() { }
        #endregion

        public enum GameState
        {
            Main,
            SelectLevel,
            Battle,
            BattleResult,
            Pause
        }

        [SerializeField]
        private GameState startState;

        [SerializeField]
        private bool cleanStart = false;

        private LevelsController levels;

        public GameState CurrentState { get; private set; }


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            InitComponents();

            SwitchState(startState);
        }

        private void OnDestroy()
        {
            SavesManager.Instance.Save();
        }

        private void InitComponents()
        {
            if (cleanStart)
                SavesManager.Instance.ResetSaves();

            SavesManager.Instance.Load();

            Pool.Instance.Init();
            BattleController.Instance.Init();
            UIController.Instance.Init();

            levels = FindObjectOfType<LevelsController>();
        }

        public void SwitchState(GameState state)
        {
            switch (state)
            {
                case GameState.Battle:
                    switch (CurrentState)
                    {
                        case GameState.Pause:
                            BattleController.Instance.PauseBattle(false);
                            break;

                        case GameState.SelectLevel:
                            BattleController.Instance.StartBattle(levels.CurrentLevelData);
                            break;

                        case GameState.BattleResult:
                            BattleController.Instance.StartBattle(levels.CurrentLevelData);
                            break;

                        default:
                            BattleController.Instance.StartBattle(levels.CurrentLevelData);
                            break;
                    }
                    
                    UIController.Instance.Show<UIHUDScreen>();
                    break;

                case GameState.SelectLevel:
                    UIController.Instance.Show<UISelectLevelScreen>();
                    break;

                case GameState.Main:

                    break;

                case GameState.Pause:
                    BattleController.Instance.PauseBattle(true);
                    UIController.Instance.Show<UIPauseScreen>();
                    break;

                case GameState.BattleResult:
                    UIController.Instance.Show<UIBattleResultScreen>();
                    break;
            }

            CurrentState = state;
        }
    }
}
