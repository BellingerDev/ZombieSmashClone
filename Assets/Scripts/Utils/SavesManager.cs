using System;
using UnityEngine;

namespace Utils.Saves
{
    public class SavesManager
    {
        private const string LEVELS_PROGRESS_SAVE_ID = "LEVELS_PROGRESS";
        private const string SETTINGS_SAVE_ID = "SETTINGS";

        #region Singleton Define
        private static SavesManager instance;

        public static SavesManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SavesManager();

                return instance;
            }
        }

        private SavesManager() { }
        #endregion

        [Serializable]
        public class LevelsProgress
        {
            [Serializable]
            public class LevelProgress
            {
                public int lvlId;
                public int stars;
                public bool completed;
            }

            [SerializeField]
            private LevelProgress[] levels = new LevelProgress[0];

            [SerializeField]
            private int activeLevel;


            public int GetStars(int lvlId)
            {
                var p = Array.FindLast<LevelProgress>(levels, l => l.lvlId == lvlId);
                if (p != null)
                    return p.stars;

                return 0;
            }

            public bool GetIsCompleted(int lvlId)
            {
                var p = Array.FindLast<LevelProgress>(levels, l => l.lvlId == lvlId);
                if (p != null)
                    return p.completed;

                return false;
            }

            public void SetStars(int lvlId, int stars, bool completed)
            {
                var p = Array.FindLast<LevelProgress>(levels, l => l.lvlId == lvlId);
                if (p != null)
                {
                    p.stars = stars;
                    p.completed = completed;
                }
                else
                {
                    var lp = new LevelProgress();

                    lp.lvlId = lvlId;
                    lp.stars = stars;
                    lp.completed = completed;

                    Array.Resize<LevelProgress>(ref levels, levels.Length + 1);
                    levels[levels.Length - 1] = lp;
                }
            }

            public int GetActiveLevel()
            {
                return activeLevel;
            }

            public void SetActiveLevel(int lvl)
            {
                activeLevel = lvl;
            }
        }

        [Serializable]
        public class GameSettings
        {
            public bool sound;
            public bool vibrate;
        }

        private LevelsProgress levelsProgress;
        private GameSettings settings;


        public void Load()
        {
            // load levels progress
            string lpString = PlayerPrefs.GetString(LEVELS_PROGRESS_SAVE_ID);

            if (!String.IsNullOrEmpty(lpString))
                levelsProgress = JsonUtility.FromJson<LevelsProgress>(lpString);
            else
                levelsProgress = new LevelsProgress();

            // load settings
            string sString = PlayerPrefs.GetString(SETTINGS_SAVE_ID);

            if (!string.IsNullOrEmpty(sString))
                settings = JsonUtility.FromJson<GameSettings>(sString);
            else
                settings = new GameSettings();
        }

        public void Save()
        {
            PlayerPrefs.SetString(LEVELS_PROGRESS_SAVE_ID, JsonUtility.ToJson(levelsProgress));
            PlayerPrefs.SetString(SETTINGS_SAVE_ID, JsonUtility.ToJson(settings));
        }

        public int GetLevelStars(int lvlId)
        {
            return levelsProgress.GetStars(lvlId);
        }

        public bool GetLevelCompleted(int lvlId)
        {
            return levelsProgress.GetIsCompleted(lvlId);
        }

        public void SetLevelStars(int lvlId, int stars, bool completed)
        {
            levelsProgress.SetStars(lvlId, stars, completed);
            Save();
        }

        public int GetActiveLevel()
        {
            return levelsProgress.GetActiveLevel();
        }

        public void SetActiveLevel(int lvl)
        {
            levelsProgress.SetActiveLevel(lvl);
        }

        public void ResetSaves()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        public GameSettings Settings { get { return settings; } }
    }
}
