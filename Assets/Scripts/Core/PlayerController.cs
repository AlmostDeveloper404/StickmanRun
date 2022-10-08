using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

namespace Main
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private float _playerSpeed;


        [SerializeField] private float _maxXPos = 2.5f;
        [SerializeField] private Animator _animator;

        private DynamicJoystick _dynamicJoystick;
        private PlayerBodyguards _playerBodyGuards;

        [SerializeField] private Collider _dangerCollider;

        private CompositeDisposable _updateDisposable = new CompositeDisposable();
        private CompositeDisposable _triggerDis = new CompositeDisposable();

        [Inject]
        public void Construct(DynamicJoystick fixedJoystick, PlayerBodyguards playerBodyguards)
        {
            _dynamicJoystick = fixedJoystick;
            _playerBodyGuards = playerBodyguards;
        }

        private void OnEnable()
        {
            _dangerCollider.OnTriggerEnterAsObservable().Where(t => t.gameObject.layer == LayerMask.NameToLayer("Enemy")).Subscribe(_ => _playerBodyGuards.TryUseBodyguard(_)).AddTo(_triggerDis);
            GameManager.OnGameOver += GameOver;
            GameManager.OnStatedPreporations += StartPreporations;
            GameManager.OnGameStarted += StartGame;
        }

        private void StartGame()
        {
            Observable.EveryUpdate().Subscribe(_ => RxUpdate()).AddTo(_updateDisposable);
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

        private void RxUpdate()
        {
            Move();
            MoveAnimation();
        }

        private void MoveAnimation()
        {
            _animator.SetFloat(Animations.HorizontalValue, _dynamicJoystick.Horizontal);
        }

        private void Move()
        {
            Vector3 offset = new Vector3(_dynamicJoystick.Horizontal, 0f, 1f).normalized * _speed * Time.deltaTime;
            Vector3 nextPosition = transform.position + offset;

            nextPosition.x = Mathf.Clamp(nextPosition.x, -_maxXPos, _maxXPos);

            transform.position = nextPosition;
        }

        private void GameOver()
        {
            _updateDisposable?.Clear();
            _animator.applyRootMotion = true;
            _speed = 0;
            _animator.SetTrigger(Animations.Death);
        }

        private void OnDestroy()
        {
            _triggerDis?.Clear();
            _updateDisposable?.Clear();
        }
    }
}

