using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerAnimations : MonoBehaviour
    {
        private PlayerShooting _playerShooting;
        private UIManager _uiManager;

        [Inject]
        private void Construct(PlayerShooting playerShooting, UIManager uIManager)
        {
            _playerShooting = playerShooting;
            _uiManager = uIManager;
        }

        public void StartThrowingGranade()
        {
            _playerShooting.StopShooting();
        }

        public void ThrowGranade()
        {
            _playerShooting.ThrowGranade();
        }

        public void StopThrowingGranade()
        {
            _playerShooting.StartShooting();
        }

        public void TriggerGameOver()
        {
            _uiManager.ChangePanal(_uiManager.EndGamePanal);
        }
    }

}

