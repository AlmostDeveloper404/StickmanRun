using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Rifle : Weapon
    {
        public GameObject _bulletPref;
        public Transform _spawnPoint;


        private ObjectPool<Bullet> _bulletPool;


        public virtual void OnEnable()
        {
            _bulletPool = new ObjectPool<Bullet>(_bulletPref);
        }

        public override void Attack()
        {
            base.Attack();
            Bullet bullet = _bulletPool.Pull(_spawnPoint.position, Quaternion.identity);
        }
    }
}



