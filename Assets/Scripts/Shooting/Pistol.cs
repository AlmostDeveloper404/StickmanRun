using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Pistol : IWeapon
    {
        private ObjectPool<Bullet> _bullets;

        private Transform _firePoint;
        private float _timeBetweenShots;

        public Pistol(GunsInfo gunsInfo, Transform firePoint)
        {
            _bullets = new ObjectPool<Bullet>(gunsInfo.BulletPref);
            _firePoint = firePoint;
            _timeBetweenShots = gunsInfo.TimeBetweenShots;
        }

        public void Attack()
        {
            Bullet bullet = _bullets.Pull(_firePoint.position);

        }
    }
}


