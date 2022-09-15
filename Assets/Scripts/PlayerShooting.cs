using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerShooting
    {
        private float _timeBetweenShots;
        private GameObject _bulletPref;
        private Transform _spawnPoint;

        private ObjectPool<Bullet> pooledBullet;

        private float _timer;

        public PlayerShooting(GameObject bulletPref, float timeBetweenShots, Transform spawnPoint)
        {
            _timeBetweenShots = timeBetweenShots;
            _spawnPoint = spawnPoint;
            _bulletPref = bulletPref;

            pooledBullet = new ObjectPool<Bullet>(_bulletPref);
        }

        public void Shoot()
        {
            Debug.Log("Yep");
        }

    }
}


