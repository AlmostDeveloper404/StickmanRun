using System;
using UnityEngine;
using Zenject;
using UniRx.Triggers;
using UniRx;

namespace Main
{
    [RequireComponent(typeof(Rigidbody))]
    public class StraightForwardEnemy : Enemy, ITakeDamage
    {
        private PlayerController _playerController;

        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _checkForPlayerDistance;

        public float DistanceToCheckForPlayer { get { return _checkForPlayerDistance; } }
        public float Speed { get { return _speed; } }
        public float RotationSpeed { get { return _rotationSpeed; } }



        private EnemyBase<StraightForwardEnemy> _currentState;
        public StraightForwardAttackState StraightForwardAttackState { get; private set; }
        public StraightForwardIdle StraightForwardIdle { get; private set; }

        private float _normalSpeed;
        private float _normalRotation;

        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _checkForKickCollider;

        private CompositeDisposable _playerCheckTrigger = new CompositeDisposable();


        [Inject]
        public void Construct(PlayerController player)
        {
            _playerController = player;
            StraightForwardAttackState = new StraightForwardAttackState(_playerController);
            StraightForwardIdle = new StraightForwardIdle(_playerController);
        }

        private void Start()
        {
            _checkForKickCollider.OnTriggerEnterAsObservable().Where(t => t.gameObject.layer == LayerMask.NameToLayer("Player")).Subscribe(_ => Kick()).AddTo(_playerCheckTrigger);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _currentState = StraightForwardIdle;
            _currentState.EntryState(this, _animator);
        }


        protected override void OnDisable()
        {
            base.OnDisable();
        }
        protected override void StartPreporations()
        {
            _normalSpeed = _speed;
            _normalRotation = _rotationSpeed;

            _speed = 0;
            _rotationSpeed = 0;
        }

        protected override void StartGame()
        {
            _speed = _normalSpeed;
            _rotationSpeed = _normalRotation;

        }

        protected override void CheckUpdate()
        {
            _currentState.UpdateState(this);
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

        public void ChangeState(EnemyBase<StraightForwardEnemy> straightForwardBase)
        {
            _currentState = straightForwardBase;
            _currentState.EntryState(this, _animator);
        }


        public void TakeDamage()
        {
            Death();
        }

        public override void Death()
        {
            _animator.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            _playerCheckTrigger?.Clear();
            _speed = 0;
            _rotationSpeed = 0;
        }

        protected override void GameOver()
        {
            _speed = 0;
            _rotationSpeed = 0;
            _animator.SetTrigger(Animations.GameOver);
        }
    }
}


