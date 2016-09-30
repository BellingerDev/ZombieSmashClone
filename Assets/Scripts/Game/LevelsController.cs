using Game.Data;
using System;
using UnityEngine;
using Utils.Saves;

namespace Game
{
    public class LevelsController : MonoBehaviour
    {
        #region Define Instance
        private static WeakReference instance;
        public static LevelsController Instance
        {
            get
            {
                if (instance == null)
                    instance = new WeakReference(GameObject.FindObjectOfType<LevelsController>());
                else if (instance.Target == null)
                    instance.Target = GameObject.FindObjectOfType<LevelsController>();

                return (LevelsController)instance.Target;
            }
        }
        #endregion

        private const string CURRENT_LEVEL_ID = "CURRENT_LEVEL_ID";

        [SerializeField]
        private LevelData[] levels;

        [SerializeField]
        private int currentLevel;

        public LevelData CurrentLevelData { get { return levels[currentLevel]; } }


        public int GetLevelsCount()
        {
            return levels.Length;
        }

        public int GetActiveLevelId()
        {
            return currentLevel;
        }

        public void SetActiveLevel(int lvlId)
        {
            currentLevel = lvlId;
            SavesManager.Instance.SetActiveLevel(currentLevel);
        }
    }
}
