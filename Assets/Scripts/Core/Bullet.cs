﻿using UnityEngine;
using System;

namespace Main
{
    [RequireComponent(typeof(Rigidbody))]
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
            ITakeDamage damagable = collider.GetComponent<ITakeDamage>();

            if (damagable as MonoBehaviour)
            {
                gameObject.SetActive(false);
                damagable.TakeDamage();
            }

            DropArea dropArea = collider.GetComponentInParent<DropArea>();
            if (dropArea) gameObject.SetActive(false);

        }
    }
}

