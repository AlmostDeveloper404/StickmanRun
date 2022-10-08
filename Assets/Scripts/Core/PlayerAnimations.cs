using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerAnimations : MonoBehaviour
    {
        private PlayerShooting _playerShooting;

        [Inject]
        private void Construct(PlayerShooting playerShooting)
        {
            _playerShooting = playerShooting;
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


    }

}

