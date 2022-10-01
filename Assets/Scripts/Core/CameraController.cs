using UnityEngine;
using Zenject;


namespace Main
{
    public class CameraController : MonoBehaviour
    {
        private PlayerController _playerController;
        [SerializeField] private Animator _cameraAnimator;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void OnEnable()
        {
            GameManager.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= GameOver;
        }

        private void LateUpdate()
        {
            transform.position = _playerController.transform.position;
        }
        private void GameOver()
        {
            _cameraAnimator.SetTrigger(Animations.GameOver);
        }

    }

}

