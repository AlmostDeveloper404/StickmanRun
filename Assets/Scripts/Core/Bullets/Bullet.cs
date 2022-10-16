using UnityEngine;
using System;
using UniRx;

namespace Main
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private Action<Bullet> _returnToPull;
        [SerializeField] private float _speed;

        public float Speed { get { return _speed; } }

        public ParticleSystem WaveParticles { get { return _waves; } }

        [SerializeField] private ParticleSystem _waves;

        [SerializeField] private int _damage;

        public int Damage { get { return _damage; } }

        private CompositeDisposable _updateDis = new CompositeDisposable();

        private void OnEnable()
        {
            Observable.EveryUpdate().Subscribe(_ => CheckUpdate()).AddTo(_updateDis);
        }

        private void OnDisable()
        {
            _updateDis?.Clear();
            ReturnToPool();
        }

        public void Initialize(Action<Bullet> returnAction)
        {
            _waves.transform.parent = null;
            _returnToPull = returnAction;
        }

        public void ReturnToPool()
        {
            _returnToPull?.Invoke(this);
        }

        protected virtual void CheckUpdate()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        protected virtual void OnTriggerEnter(Collider collider)
        {
            ITakeDamage damagable = collider.GetComponent<ITakeDamage>();

            if (damagable as MonoBehaviour)
            {
                gameObject.SetActive(false);
                damagable.TakeDamage(_damage);
            }

            DropArea dropArea = collider.GetComponentInParent<DropArea>();
            if (dropArea)
            {
                _waves.transform.position = transform.position;
                _waves.Play();
                gameObject.SetActive(false);
            }
        }
    }
}

