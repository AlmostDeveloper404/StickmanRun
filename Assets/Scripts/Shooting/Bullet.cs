using UnityEngine;
using System;

namespace Main
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private Action<Bullet> _returnToPull;
        [SerializeField] private float _speed;

        private void OnDisable()
        {
            ReturnToPool();
        }

        public void Initialize(Action<Bullet> returnAction)
        {
            _returnToPull = returnAction;
        }

        public void ReturnToPool()
        {
            _returnToPull?.Invoke(this);
        }

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<Enemy>())
            {
                gameObject.SetActive(false);
            }
        }
    }
}

