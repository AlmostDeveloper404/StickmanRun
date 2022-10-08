using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
            Container.Bind<PlayerShooting>().FromInstance(_playerController.GetComponent<PlayerShooting>()).AsSingle().NonLazy();
            Container.Bind<PlayerBodyguards>().FromInstance(_playerController.GetComponent<PlayerBodyguards>()).AsSingle().NonLazy();
            Container.Bind<PlayerStats>().FromInstance(_playerController.GetComponent<PlayerStats>()).AsSingle().NonLazy();
        }
    }
}

