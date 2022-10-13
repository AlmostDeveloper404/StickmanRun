using UnityEngine;
using Zenject;
using UniRx;


namespace Main
{
    public class CameraController : MonoBehaviour
    {
        private PlayerController _playerController;
        [SerializeField] private Animator _cameraAnimator;

        private CompositeDisposable _lateUpdate = new CompositeDisposable();

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void OnEnable()
        {
            GameManager.OnGameStarted += GameStated;
            GameManager.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= GameStated;
            GameManager.OnGameOver -= GameOver;
        }

        private void GameStated()
        {
            Observable.EveryLateUpdate().Subscribe(_ => FollowPlayer()).AddTo(_lateUpdate);
        }

        private void FollowPlayer()
        {
            transform.position = _playerController.transform.position;
        }

        private void GameOver()
        {
            _cameraAnimator.SetTrigger(Animations.GameOver);
        }

        private void OnDestroy()
        {
            _lateUpdate?.Clear();
        }

    }

}

