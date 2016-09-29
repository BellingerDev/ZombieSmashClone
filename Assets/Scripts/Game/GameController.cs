using UI;
using UI.Screens;
using UnityEngine;
using Utils.Poolable;


namespace Game
{
    public class GameController : MonoBehaviour
    {
        public enum GameState
        {
            MainMenu,
            LevelChoose,
            Battle
        }

        [SerializeField]
        private GameState startState;

        private LevelsController levels;


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            InitComponents();

            SwitchState(startState);
        }

        private void InitComponents()
        {
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
                    BattleController.Instance.StartBattle(levels.CurrentLevelData);
                    UIController.Instance.Show<UIHUDScreen>();
                    break;

                case GameState.LevelChoose:

                    break;

                case GameState.MainMenu:

                    break;
            }
        }
    }
}
