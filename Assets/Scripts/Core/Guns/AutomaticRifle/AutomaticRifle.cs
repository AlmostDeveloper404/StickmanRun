using UnityEngine;

namespace Main
{
    public class AutomaticRifle : Weapon
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
            Bullet bullet = _bulletPool.Pull(_spawnPoint.position, Quaternion.identity);
        }
    }
}

