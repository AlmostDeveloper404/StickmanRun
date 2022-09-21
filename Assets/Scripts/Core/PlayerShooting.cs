using UnityEngine;

namespace Main
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GunsInfo[] _allWeapon;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Animator _animator;
        private IWeapon _weapon;

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


