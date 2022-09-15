using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerShootingInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _bulletPref;
        [SerializeField] private float _timeToShoot;
        [SerializeField] private Transform _spawnPoint;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerShooting>().AsSingle();
            Container.Bind<GameObject>().FromInstance(_bulletPref);
            Container.Bind<float>().FromInstance(_timeToShoot);
            Container.Bind<Transform>().FromInstance(_spawnPoint);
        }

    }
}


