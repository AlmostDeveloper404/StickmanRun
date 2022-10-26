using System;
using UnityEngine;
using UniRx;
using Zenject;
using UniRx.Triggers;

namespace Main
{
    public class Bodyguard : MonoBehaviour, IPoolable<Bodyguard>
    {
        private BodyguardBase _currentState;
        private BodyguardWinning _bodyguardWinningState;
        private BodyguardGameOver _bodyguardGameOverState;
        public BodyguardAttack BodyguardAttackState { get; set; }
        public BodyguardEscort BodyguardSecureState { get; private set; }
        public BodyguardDefend BodyguardDefendState { get; private set; }


        private Action<Bodyguard> _returnToPool;

        [SerializeField] private float _lerpingSpeed;

        public float LerpingSpeed { get { return _lerpingSpeed; } }

        private CompositeDisposable _updateDisposable = new CompositeDisposable();
        private CompositeDisposable _attackDisposable = new CompositeDisposable();

        [SerializeField] private Animator _animator;

        private DynamicJoystick _dynamicJoystick;

        [SerializeField] private Collider _attackRadius;

        private PlayerBodyguards _playerBodyguards;
        private PlayerController _playerController;

        [SerializeField] private float _attackSpeed;

        public float AttackSpeed { get { return _attackSpeed; } }

        [Inject]
        public void Construct(DynamicJoystick dynamicJoystick, PlayerBodyguards playerBodyguards, PlayerController playerController)
        {
            _playerBodyguards = playerBodyguards;
            _dynamicJoystick = dynamicJoystick;
            _playerController = playerController;
        }

        private void OnEnable()
        {
            GameManager.OnLevelCompleted += LevelCompleted;
            GameManager.OnGameOver += GameOver;
            Observable.EveryUpdate().Subscribe(_ => UpdateState()).AddTo(_updateDisposable);
        }


        private void OnDisable()
        {
            GameManager.OnLevelCompleted -= LevelCompleted;
            GameManager.OnGameOver -= GameOver;
            _updateDisposable?.Clear();
            _attackDisposable?.Clear();
            ReturnToPool();
        }
        private void LevelCompleted()
        {
            _bodyguardWinningState = new BodyguardWinning(_animator);
            _currentState = _bodyguardWinningState;
            _currentState.EntryState(this);
        }
        private void GameOver()
        {
            _bodyguardGameOverState = new BodyguardGameOver(_animator, _playerController);
            _currentState = _bodyguardGameOverState;
            _currentState.EntryState(this);
        }

        public void Initialize(Action<Bodyguard> returnAction)
        {
            _returnToPool = returnAction;
            _attackRadius.OnTriggerEnterAsObservable().Where(t => t.gameObject.GetComponent<Enemy>()).Subscribe(_ => CheckForTrigger(_.GetComponent<Enemy>())).AddTo(_attackDisposable);
        }

        public void ReturnToPool()
        {
            _returnToPool?.Invoke(this);
        }

        public void PrepareSecuring(BodyguardPoint bodyguardPoint)
        {
            BodyguardSecureState = new BodyguardEscort(_animator, _dynamicJoystick, bodyguardPoint, _playerBodyguards);
            _currentState = BodyguardSecureState;
            _currentState.EntryState(this);
        }

        private void UpdateState()
        {
            _currentState?.UpdateState(this);
        }

        public void ChangeState(BodyguardBase bodyguardBase)
        {
            _currentState = bodyguardBase;
            _currentState?.EntryState(this);
        }

        public void Defend(Collider collider)
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy)
            {
                BodyguardDefendState = new BodyguardDefend(enemy, _animator, _playerBodyguards);
                ChangeState(BodyguardDefendState);
            }
        }

        private void CheckForTrigger(Enemy enemy)
        {
            _currentState.OnTriggerState(this, enemy);
        }

        public void DisableAnimator()
        {
            _animator.enabled = false;
        }
    }

}