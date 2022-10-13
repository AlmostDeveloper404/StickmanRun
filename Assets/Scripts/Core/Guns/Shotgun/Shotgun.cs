using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Shotgun : Weapon
    {
        public GameObject _bulletPref;
        public Transform[] _spawnPoint;


        private ObjectPool<Bullet> _bulletPool;


        public virtual void OnEnable()
        {
            _bulletPool = new ObjectPool<Bullet>(_bulletPref);
        }

        public override void Attack()
        {
            for (int i = 0; i < _spawnPoint.Length; i++)
            {
                Bullet bullet = _bulletPool.Pull(_spawnPoint[i].position, Quaternion.identity);
            }

        }
    }

}

