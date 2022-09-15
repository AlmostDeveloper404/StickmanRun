using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace Main
{

    public class UIManager : MonoBehaviour
    {
        private PlayerShooting _playerShooting;
        [SerializeField] private Button _shootButton;

        [Inject]
        public void Construct(PlayerShooting playerShooting)
        {
            _playerShooting = playerShooting;
        }

        private void OnEnable()
        {
            _shootButton.onClick.AddListener(() => _playerShooting.Shoot());
        }

        private void OnDisable()
        {
            _shootButton.onClick.RemoveListener(() => _playerShooting.Shoot());
        }
    }
}


