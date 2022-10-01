using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private float _playerSpeed;


        [SerializeField] private float _maxXPos = 2.5f;
        [SerializeField] private Animator _animator;

        private DynamicJoystick _fixedJoystick;

        [Inject]
        public void Construct(DynamicJoystick fixedJoystick)
        {
            _fixedJoystick = fixedJoystick;
        }

        private void OnEnable()
        {
            GameManager.OnGameOver += GameOver;
            GameManager.OnStatedPreporations += StartPreporations;
            GameManager.OnGameStarted += StartGame;
        }

        private void StartGame()
        {
            _speed = _playerSpeed;
            _animator.SetTrigger(Animations.StartGame);
        }

        private void StartPreporations()
        {
            _playerSpeed = _speed;
            _speed = 0;
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= GameOver;
            GameManager.OnStatedPreporations -= StartPreporations;
            GameManager.OnGameStarted -= StartGame;
        }

        private void Update()
        {
            Move();
            MoveAnimation();
        }

        private void MoveAnimation()
        {
            _animator.SetFloat(Animations.HorizontalValue, _fixedJoystick.Horizontal);
        }

        private void Move()
        {
            Vector3 offset = new Vector3(_fixedJoystick.Horizontal, 0f, 1f).normalized * _speed * Time.deltaTime;
            Vector3 nextPosition = transform.position + offset;

            nextPosition.x = Mathf.Clamp(nextPosition.x, -_maxXPos, _maxXPos);

            transform.position = nextPosition;
        }

        private void GameOver()
        {
            _animator.applyRootMotion = true;
            _speed = 0;
            _animator.SetTrigger(Animations.Death);
        }
    }
}

