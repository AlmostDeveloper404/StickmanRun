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
            SceneManager.LoadSceneAsync(levelIndex);
        }

    }
}

