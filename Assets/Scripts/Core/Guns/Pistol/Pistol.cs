using UnityEngine;


namespace Main
{
    public class Pistol : Weapon
    {
        public GameObject _bulletPref;
        public Transform _spawnPoint;


        public ObjectPool<Bullet> _bulletPool;


        public virtual void Start()
        {
            _bulletPool = new ObjectPool<Bullet>(_bulletPref);
        }

        public override void Attack()
        {
            Bullet bullet = _bulletPool.Pull(_spawnPoint.position, Quaternion.identity);
        }
    }
}

