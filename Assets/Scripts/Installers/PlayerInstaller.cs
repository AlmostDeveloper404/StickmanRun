using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerController;
        [SerializeField] private Transform _spawnPoint;
        public override void InstallBindings()
        {
            var player = Container.InstantiatePrefab(_playerController, _spawnPoint.position, Quaternion.identity, null);
            Container.Bind<PlayerController>().FromInstance(player.GetComponent<PlayerController>()).AsSingle().NonLazy();
            Container.Bind<PlayerShooting>().FromInstance(player.GetComponent<PlayerShooting>()).AsSingle().NonLazy();
        }
    }
}

