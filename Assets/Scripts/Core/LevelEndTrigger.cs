using UnityEngine;
using UnityEngine.Playables;

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
                GameManager.ChangeGameState(GameState.LevelCompleted);
            }
        }
    }
}


