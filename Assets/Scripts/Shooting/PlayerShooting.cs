using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerShooting : MonoBehaviour
    {



        [SerializeField] private GunsInfo[] _allWeapon;
        [SerializeField] private Transform _firePoint;
        private IWeapon _weapon;

        private void Start()
        {
            IWeapon weapon = new Pistol(_allWeapon[0], _firePoint);
            SetWeapon(weapon);
        }

        public void SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public void Attack()
        {
            _weapon?.Attack();
        }
    }
}


