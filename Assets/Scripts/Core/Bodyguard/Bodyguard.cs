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
        public BodyguardAttack BodyguardAttack { get; set; }
        public BodyguardEscort BodyguardSecure { get; private set; }

        public BodyguardDefend BodyguardDefend { get; private set; }


        private Action<Bodyguard> _returnToPool;
        private BodyguardPoint _point;

        [SerializeField] private float _lerpingSpeed;

        public float LerpingSpeed { get { return _lerpingSpeed; } }

        //private CompositeDisposable _secureDisposable = new CompositeDisposable();
        //private CompositeDisposable _attackDisposable = new CompositeDisposable();
        //private CompositeDisposable _attackTriggerDisposable = new CompositeDisposable();

        private CompositeDisposable _updateDisposable = new CompositeDisposable();
        private CompositeDisposable _attackDisposable = new CompositeDisposable();

        [SerializeField] private Animator _animator;

        private DynamicJoystick _dynamicJoystick;

        private Enemy _targetEnemy;
        [SerializeField] private Collider _attackRadius;

        private PlayerBodyguards _playerBodyguards;

        [SerializeField] private float _attackSpeed;

        public float AttackSpeed { get { return _attackSpeed; } }

        [Inject]
        public void Construct(DynamicJoystick dynamicJoystick, PlayerBodyguards playerBodyguards)
        {
            _playerBodyguards = playerBodyguards;
            _dynamicJoystick = dynamicJoystick;
        }

        private void OnEnable()
        {
            Observable.EveryUpdate().Subscribe(_ => UpdateState()).AddTo(_updateDisposable);
        }

        private void OnDisable()
        {
            _updateDisposable?.Clear();
            ReturnToPool();
        }

        public void Initialize(Action<Bodyguard> returnAction)
        {
            _returnToPool = returnAction;
            //_attackRadius.OnTriggerEnterAsObservable().Where(t => t.gameObject.layer == LayerMask.NameToLayer("Enemy")).Subscribe(_ => AttackTrigger(_)).AddTo(_attackTriggerDisposable);
            _attackRadius.OnTriggerEnterAsObservable().Where(t => t.gameObject.GetComponent<Enemy>()).Subscribe(_ => CheckForTrigger(_.GetComponent<Enemy>())).AddTo(_attackDisposable);
        }

        public void ReturnToPool()
        {
            _returnToPool?.Invoke(this);
        }

        public void PrepareSecuring(BodyguardPoint bodyguardPoint)
        {
            BodyguardSecure = new BodyguardEscort(_animator, _dynamicJoystick, bodyguardPoint, _playerBodyguards);
            _currentState = BodyguardSecure;
            _currentState.EntryState(this);
        }

        private void UpdateState()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(BodyguardBase bodyguardBase)
        {
            _currentState = bodyguardBase;
            _currentState.EntryState(this);
        }

        public void Defend(Collider collider)
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy)
            {
                BodyguardDefend = new BodyguardDefend(enemy, _animator, _playerBodyguards);
                ChangeState(BodyguardDefend);
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
        //    public void PrepareSecuring(BodyguardPoint bodyguardPoint)
        //    {
        //        _animator.SetTrigger(Animations.StartGame);
        //        _point = bodyguardPoint;
        //        Observable.EveryUpdate().Subscribe(_ => Secure()).AddTo(_secureDisposable);
        //    }

        //    private void Secure()
        //    {
        //        _animator.SetFloat(Animations.HorizontalValue, _dynamicJoystick.Horizontal);
        //        transform.position = Vector3.Lerp(transform.position, _point.transform.position, _lerpingSpeed * Time.deltaTime);
        //    }

        //    public void Defend(Collider collider)
        //    {
        //        Enemy enemy = collider.GetComponent<Enemy>();

        //        if (enemy)
        //        {
        //            _targetEnemy = enemy;
        //            Observable.EveryUpdate().Subscribe(_ => Attack()).AddTo(_attackDisposable);
        //        }

        //    }


        //    private void Attack()
        //    {
        //        if (_targetEnemy.IsAttacked)
        //        {
        //            _attackDisposable?.Clear();
        //            Observable.EveryUpdate().Subscribe(_ => Secure()).AddTo(_secureDisposable);
        //            return;
        //        }
        //        RotateTowardsEnemy();
        //        transform.position += transform.forward * _attackSpeed * Time.deltaTime;
        //    }

        //    private void AttackTrigger(Collider collider)
        //    {
        //        Enemy enemy = collider.GetComponent<Enemy>();

        //        if (!enemy) return;

        //        _targetEnemy = enemy;
        //        transform.parent = null;
        //        enemy.IsAttacked = true;
        //        RotateTowardsEnemy();
        //        collider.GetComponent<Enemy>().Death();

        //        _animator.SetTrigger(Animations.AttackHash);
        //        _playerBodyguards.RemoveBodyguard(this);

        //        _secureDisposable?.Clear();
        //        _attackTriggerDisposable?.Clear();
        //        _attackDisposable?.Clear();
        //    }

        //    private void RotateTowardsEnemy()
        //    {
        //        Vector3 direction = _targetEnemy.transform.position - transform.position;
        //        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //        transform.rotation = lookRotation;
        //    }

        //    private void OnDisable()
        //    {
        //        ReturnToPool();
        //    }

        //    void OnDestroy()
        //    {
        //        _secureDisposable?.Clear();
        //    }




    }
}

