using System;
using UnityEngine;
using Zenject;
using UniRx.Triggers;
using UniRx;

namespace Main
{
    [RequireComponent(typeof(Rigidbody))]
    public class StraightForwardEnemy : MonoBehaviour, ITakeDamage, IPoolable<StraightForwardEnemy>
    {
        private Action<StraightForwardEnemy> _returnToPull;

        private PlayerController _playerController;

        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private float _normalSpeed;
        private float _normalRotation;

        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private Collider _checkForKickCollider;

        private CompositeDisposable _updateDisposable = new CompositeDisposable();
        private CompositeDisposable _playerCheckTrigger = new CompositeDisposable();

        [Inject]
        public void Construct(PlayerController player)
        {
            _playerController = player;
        }

        private void Start()
        {
            _checkForKickCollider.OnTriggerEnterAsObservable().Where(t => t.gameObject.layer == LayerMask.NameToLayer("Player")).Subscribe(_ => Kick()).AddTo(_playerCheckTrigger);
        }

        private void OnEnable()
        {
            GameManager.OnGameStarted += StartGame;
            GameManager.OnStatedPreporations += StartPreporations;
            GameManager.OnGameOver += GameOver;
            Observable.EveryUpdate().Subscribe(_ => CheckUpdate()).AddTo(_updateDisposable);
        }


        private void OnDisable()
        {
            GameManager.OnGameStarted -= StartGame;
            GameManager.OnGameOver -= GameOver;
            GameManager.OnStatedPreporations -= StartPreporations;
            _updateDisposable?.Clear();
        }
        private void StartPreporations()
        {
            _normalSpeed = _speed;
            _normalRotation = _rotationSpeed;

            _speed = 0;
            _rotationSpeed = 0;
        }

        private void StartGame()
        {
            _speed = _normalSpeed;
            _rotationSpeed = _normalRotation;
            _animator.SetTrigger(Animations.StartGame);
        }

        private void CheckUpdate()
        {
            MoveTowardsPlayer();
        }

        private void Kick()
        {
            _playerCheckTrigger?.Clear();
            GameManager.OnGameOver -= GameOver;

            _speed = 0;
            _rotationSpeed = 0;

            _animator.SetTrigger(Animations.AttackHash);
            GameManager.ChangeGameState(GameState.GameOver);
        }

        private void MoveTowardsPlayer()
        {
            Vector3 direction = (transform.position - _playerController.transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);

            transform.position += -transform.forward * _speed * Time.deltaTime;
        }


        public void Initialize(Action<StraightForwardEnemy> returnAction)
        {
            _returnToPull = returnAction;
        }

        public void ReturnToPool()
        {
            _returnToPull?.Invoke(this);
        }

        public void TakeDamage()
        {
            Death();
        }

        private void Death()
        {
            _animator.enabled = false;

            _playerCheckTrigger?.Clear();
            _speed = 0;
            _rotationSpeed = 0;
        }

        private void GameOver()
        {
            _speed = 0;
            _rotationSpeed = 0;
            _animator.SetTrigger(Animations.GameOver);
        }
    }
}


