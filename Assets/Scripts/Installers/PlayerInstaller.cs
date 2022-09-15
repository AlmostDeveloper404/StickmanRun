using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _spawnPoint;
        public override void InstallBindings()
        {
            var player = Container.InstantiatePrefabForComponent<PlayerController>(_playerController, _spawnPoint.position, Quaternion.identity, null);
            Container.Bind<PlayerController>().FromInstance(player).AsSingle().NonLazy();
        }
    }
}

