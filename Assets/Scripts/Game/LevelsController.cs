using Game.Data;
using System;
using UnityEngine;


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

        public int CurrentLevel
        {
            get
            {
                return PlayerPrefs.GetInt(CURRENT_LEVEL_ID);
            }
        }

        public LevelData CurrentLevelData { get { return levels[CurrentLevel]; } }
    }
}
