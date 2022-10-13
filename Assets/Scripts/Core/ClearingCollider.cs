using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Main
{
    public class ClearingCollider : MonoBehaviour
    {
        private PlayerController _playerController;
        [SerializeField] private Vector3 _offset;

        private Vector3 _playerPos;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Update()
        {
            _playerPos = _playerController.transform.position;
            _playerPos.x = 0;
            transform.position = _playerPos + _offset;
        }

        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy) enemy.Disable();
        }
    }
}


