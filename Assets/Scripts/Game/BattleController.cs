using Game.Areas;
using Game.Data;
using System;
using UI;
using UI.Screens;
using UnityEngine;
using Utils.Poolable;
using Utils.Saves;

namespace Game
{
    public class BattleController : MonoBehaviour
    {
        #region Singleton Reailze
        private static WeakReference instance;
        public static BattleController Instance
        {
            get
            {
                if (instance == null || instance.Target == null)
                    instance = new WeakReference(FindObjectOfType<BattleController>());

                return instance.Target as BattleController;
            }
        }
        #endregion

        public enum BattleResult
        {
            Process,
            Won,
            Lost
        }

        public BattleResult Result { get; set; }

        private LevelData           levelData;

        private int                 currentWave;
        private float               startTime;

        private EntitiesSpawnArea   spawner;
        private ObjectsContainer    container;

        public bool                 IsInitialized { get; private set; }
        public bool                 IsPaused { get; private set; }


        public void StartBattle(LevelData ld)
        {
            IsPaused = false;
            Result = BattleResult.Process;

            container.Clear();

            levelData = ld;

            startTime = Time.time;
            currentWave = 0;

            // setup player
            spawner.Spawn(Pool.Instance.Get(levelData.Player.PrototypeId), levelData.Player);
            GameObject.FindObjectOfType<Player>().OnDied += OnPlayerDied;
        }

        public void RestartBattle()
        {
            StartBattle(levelData);
        }

        public void PauseBattle(bool state)
        {
            IsPaused = state;
        }

        public void Init()
        {
            spawner = GameObject.FindObjectOfType<EntitiesSpawnArea>();
            spawner.Init();

            container = GameObject.FindObjectOfType<ObjectsContainer>();

            IsInitialized = true;
        }

        public void DeInit()
        {
            IsInitialized = false;

            GameObject.FindObjectOfType<Player>().OnDied -= OnPlayerDied;

            spawner.DeInit();
            spawner = null;

            container = null;
            levelData = null;
        }

        private void Update()
        {
            if (!IsInitialized)
                return;

            if (levelData == null)
                return;

            if (IsPaused)
                return;

            if (Time.time - startTime < levelData.Battle.BattleTime)
            {
                if (currentWave < levelData.Battle.Waves.Length)
                {
                    var wave = levelData.Battle.Waves[currentWave];

                    if ((int)(Time.time - startTime) == (int)wave.Time)
                    {
                        foreach (var eu in wave.Enemies)
                        {
                            for (int i = 0; i < eu.Count; i++)
                            {
                                EnemyData ed = Array.FindLast(levelData.Enemies, e => e.PrototypeId == eu.Enemy);
                                spawner.Spawn(Pool.Instance.Get(eu.Enemy), ed);
                            }
                        }

                        currentWave++;
                    }

                    return;
                }
            }
            else
            {
                if (Result == BattleResult.Process)
                    OnBattleWin();
            }
        }

        private void OnPlayerDied()
        {
            PauseBattle(true);
            Result = BattleResult.Lost;

            SavesManager.Instance.SetLevelStars(LevelsController.Instance.GetActiveLevelId(), 1, false);
            GameController.Instance.SwitchState(GameController.GameState.BattleResult);
        }

        private void OnBattleWin()
        {
            PauseBattle(true);
            Result = BattleResult.Won;

            SavesManager.Instance.SetLevelStars(LevelsController.Instance.GetActiveLevelId(), FindObjectOfType<Player>().Health, true);
            GameController.Instance.SwitchState(GameController.GameState.BattleResult);
        }
    }
}
