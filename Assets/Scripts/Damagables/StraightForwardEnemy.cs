using System;
using UnityEngine;
using Zenject;

namespace Main
{
    [RequireComponent(typeof(Rigidbody))]
    public class StraightForwardEnemy : MonoBehaviour, ITakeDamage, IPoolable<StraightForwardEnemy>
    {
        private Action<StraightForwardEnemy> _returnToPull;

        private PlayerController _playerController;
        private CapsuleCollider _capsuleCollider;

        [SerializeField] private Transform _centreOfMass;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private Animator _animator;

        [Inject]
        public void Construct(PlayerController player)
        {
            _playerController = player;
        }

        private void Start()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            MoveTowardsPlayer();
        }

        private void MoveTowardsPlayer()
        {
            Vector3 direction = (transform.position - _playerController.transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);

            transform.position += -transform.forward * _speed * Time.deltaTime;
        }


        public void Initialize(Action<StraightForwardEnemy> returnAction)
        {
            _returnToPull = returnAction;
        }

        public void ReturnToPool()
        {
            _returnToPull?.Invoke(this);
        }

        public void TakeDamage()
        {
            Death();
        }

        private void Death()
        {
            _animator.enabled = false;

            Collider[] collider = Physics.OverlapSphere(_centreOfMass.position, 5f);

            foreach (Collider item in collider)
            {
                Rigidbody rigidbody = item.GetComponent<Rigidbody>();
                if (rigidbody)
                {
                    rigidbody.AddExplosionForce(50f, _centreOfMass.position, 5f);
                }
            }

            _capsuleCollider.enabled = false;
            _speed = 0;
            _rotationSpeed = 0;
        }
    }
}


