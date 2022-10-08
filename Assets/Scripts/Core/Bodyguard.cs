using System;
using UnityEngine;
using UniRx;
using Zenject;
using UniRx.Triggers;

namespace Main
{
    public class Bodyguard : MonoBehaviour, IPoolable<Bodyguard>
    {

        private Action<Bodyguard> _returnToPool;
        private BodyguardPoint _point;

        [SerializeField] private float _lerpingSpeed;

        private CompositeDisposable _updateDisposable = new CompositeDisposable();
        private CompositeDisposable _attackDisposable = new CompositeDisposable();
        private CompositeDisposable _attackTriggerDisposable = new CompositeDisposable();
        [SerializeField] private Animator _animator;

        private DynamicJoystick _dynamicJoystick;

        private Collider _targetCollider;
        [SerializeField] private Collider _attackRadius;

        private PlayerBodyguards _playerBodyguards;

        [SerializeField] private float _attackSpeed;

        [Inject]
        public void Construct(DynamicJoystick dynamicJoystick, PlayerBodyguards playerBodyguards)
        {
            _playerBodyguards = playerBodyguards;
            _dynamicJoystick = dynamicJoystick;
        }

        public void Initialize(Action<Bodyguard> returnAction)
        {
            _returnToPool = returnAction;
            _attackRadius.OnTriggerEnterAsObservable().Where(t => t.gameObject.layer == LayerMask.NameToLayer("Enemy")).Subscribe(_ => AttackTrigger(_)).AddTo(_attackTriggerDisposable);
        }

        public void ReturnToPool()
        {
            _returnToPool?.Invoke(this);
        }

        public void PrepareSecuring(BodyguardPoint bodyguardPoint)
        {
            _animator.SetTrigger(Animations.StartGame);
            _point = bodyguardPoint;
            Observable.EveryUpdate().Subscribe(_ => Secure()).AddTo(_updateDisposable);
        }

        private void Secure()
        {
            _animator.SetFloat(Animations.HorizontalValue, _dynamicJoystick.Horizontal);
            transform.position = Vector3.Lerp(transform.position, _point.transform.position, _lerpingSpeed * Time.deltaTime);
        }

        public void Defend(Collider collider)
        {
            _targetCollider = collider;
            Observable.EveryUpdate().Subscribe(_ => Attack()).AddTo(_attackDisposable);
        }

        public void DisableAnimator()
        {
            _animator.enabled = false;
        }

        private void Attack()
        {
            RotateTowardsEnemy();
            transform.position += transform.forward * _attackSpeed * Time.deltaTime;
        }

        private void AttackTrigger(Collider collider)
        {
            _targetCollider = collider;
            transform.parent = null;
            RotateTowardsEnemy();
            collider.GetComponent<Enemy>().Death();

            _animator.SetTrigger(Animations.AttackHash);
            _playerBodyguards.RemoveBodyguard(this);

            _updateDisposable?.Clear();
            _attackTriggerDisposable?.Clear();
            _attackDisposable?.Clear();
        }

        private void RotateTowardsEnemy()
        {
            Vector3 direction = _targetCollider.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
        }

        private void OnDisable()
        {
            ReturnToPool();
        }

        void OnDestroy()
        {
            _updateDisposable?.Clear();
        }




    }
}

