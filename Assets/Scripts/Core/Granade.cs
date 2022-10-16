using System;
using UnityEngine;
using UniRx;

namespace Main
{

    [RequireComponent(typeof(Rigidbody))]
    public class Granade : MonoBehaviour, IPoolable<Granade>
    {
        private Action<Granade> _returnToPool;

        [SerializeField] private float _height;
        [SerializeField] private float _duration;
        [SerializeField] private float _throwingDistance;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private AnimationCurve _granadeY;
        [SerializeField] private AnimationCurve _zDistance;
        [SerializeField] private AnimationCurve _scale;
        [SerializeField] private AnimationCurve _granadeRotation;

        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private ParticleSystem _boomParticles;
        [SerializeField] private Transform _explosionSpawnPoint;
        [SerializeField] private Transform _boomParticlesSpawnPoint;

        [SerializeField] private float _damageRadius;

        [SerializeField] private int _damage;

        private CompositeDisposable _throwingGranade = new CompositeDisposable();

        private Vector3 _startPosition;

        private float _timer;
        private float _throwingProgress;

        void OnDisable()
        {
            ReturnToPool();
        }

        public void Initialize(Action<Granade> returnAction)
        {
            _explosion.transform.parent = null;
            _boomParticles.transform.parent = null;

            _timer = 0;
            _throwingProgress = 0;

            _returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            _returnToPool?.Invoke(this);
        }

        public void Throw()
        {
            _startPosition = transform.position;
            Observable.EveryUpdate().Subscribe(_ => Throwing()).AddTo(_throwingGranade);
        }


        private void Throwing()
        {
            transform.parent = null;

            _timer += Time.deltaTime;
            _throwingProgress = _timer / _duration;

            if (_throwingProgress < 1)
            {
                transform.position = _startPosition + new Vector3(0, _granadeY.Evaluate(_throwingProgress) * _height, _zDistance.Evaluate(_throwingProgress) * _throwingDistance);
                transform.rotation *= Quaternion.Euler(_granadeRotation.Evaluate(_throwingProgress) * _rotationSpeed, _granadeRotation.Evaluate(_throwingProgress) * _rotationSpeed, _granadeRotation.Evaluate(_throwingProgress) * _rotationSpeed);

                float scale = _scale.Evaluate(_throwingProgress);
                transform.localScale = Vector3.one * scale;

            }
            else
            {
                Explode();
            }

        }

        private void Explode()
        {
            _explosion.transform.position = _explosionSpawnPoint.position;
            _boomParticles.transform.position = _boomParticlesSpawnPoint.position;
            _boomParticles.Play();
            _explosion.Play();

            Collider[] _allColliders = Physics.OverlapSphere(transform.position, _damageRadius);
            foreach (var collider in _allColliders)
            {
                ITakeDamage takeDamage = collider.GetComponent<ITakeDamage>();
                if (takeDamage != null)
                {
                    takeDamage.TakeDamage(_damage);
                }
            }
            _throwingGranade?.Clear();
            gameObject.SetActive(false);
        }
    }
}

