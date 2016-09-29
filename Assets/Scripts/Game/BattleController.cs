using Game.Areas;
using Game.Data;
using System;
using UnityEngine;
using Utils.Poolable;


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
            Won,
            Lost
        }

        private LevelData           levelData;

        private int                 currentWave;
        private float               startTime;

        private EntitiesSpawnArea   spawner;
        private ObjectsContainer    container;

        public Action<BattleResult> OnBattleFinish { get; set; }
        public bool                 IsInitialized { get; private set; }


        public void StartBattle(LevelData ld)
        {
            levelData = ld;

            startTime = Time.time;
            currentWave = 0;

            // setup player
            spawner.Spawn(Pool.Instance.Get(levelData.Player.PrototypeId), levelData.Player);
            GameObject.FindObjectOfType<Player>().OnDied += OnPlayerDied;
        }

        public void ResetBattle()
        {
            container.Clear();
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

            if (Time.time - startTime < levelData.Battle.BattleTime)
            {
                if (currentWave < levelData.Battle.Waves.Length)
                {
                    var wave = levelData.Battle.Waves[currentWave];
                    
                    if ((int)(Time.time - startTime) == (int)wave.Time)
                    {
                        foreach (var eu in wave.Enemies)
                        {
                            for (int i = 0; i < eu.Count; i ++)
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


            if (OnBattleFinish != null)
                OnBattleFinish(BattleResult.Won);
        }

        private void OnPlayerDied()
        {
            Debug.Log("Game Lost");
        }
    }
}
