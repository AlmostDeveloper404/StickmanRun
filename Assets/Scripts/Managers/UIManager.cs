using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace Main
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _shootButton;

        private PlayerShooting _playerShooting;

        [Inject]
        public void Construct(PlayerShooting playerShooting)
        {
            _playerShooting = playerShooting;
        }

        private void OnEnable()
        {
            _shootButton.onClick.AddListener(() => _playerShooting.Attack());
        }

        private void OnDisable()
        {
            _shootButton.onClick.RemoveListener(() => _playerShooting.Attack());
        }
    }
}


