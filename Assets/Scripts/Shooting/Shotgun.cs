using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Shotgun : IWeapon
    {
        private ObjectPool<Bullet> _bullets;

        private Transform _firePoint;

        public Shotgun(GunsInfo gunsInfo, Transform firePoint)
        {
            _bullets = new ObjectPool<Bullet>(gunsInfo.BulletPref);
            _firePoint = firePoint;
        }
        public void Attack()
        {
            Debug.Log("Shotgun");
        }
    }
    
}

