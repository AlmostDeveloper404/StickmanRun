using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Main
{

    [Serializable]
    public struct LevelProgression
    {

        public int CurrentLevel;
    }
    public class SceneLoader : MonoBehaviour
    {
        private void Start()
        {
            LevelProgression levelProgression = SaveLoadProgress.LoadData<LevelProgression>(UniqSavingIdentefiers.LevelProgression);
            int levelIndex = levelProgression.Equals(default(LevelProgression)) ? 1 : levelProgression.CurrentLevel;
            if (levelIndex == SceneManager.sceneCountInBuildSettings)
            {
                levelIndex = SceneManager.sceneCountInBuildSettings - 1;
            }
            AppMetrica.Instance.SendEventsBuffer();
            SceneManager.LoadSceneAsync(levelIndex);
        }

    }
}

