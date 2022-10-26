using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
namespace Main
{
    public enum EndType { BossFight, GoldCollect }

    public class LevelEndTrigger : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _collectGold;
        public EndType EndType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _collectGold.Play();
                LevelProgression levelProgression = new LevelProgression();
                levelProgression.CurrentLevel = SceneManager.GetActiveScene().buildIndex + 1;
                SaveLoadProgress.SaveData<LevelProgression>(levelProgression, UniqSavingIdentefiers.LevelProgression);
                GameManager.ChangeGameState(GameState.LevelCompleted);
            }
        }
    }
}


