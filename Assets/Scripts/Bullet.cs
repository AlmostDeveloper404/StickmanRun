using UnityEngine;
using System;

namespace Main
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private Action<Bullet> _returnToPull;

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

        void OnTriggerEnter(Collider collider)
        {
            if (collider)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

