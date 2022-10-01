using UnityEngine;
using Zenject;

namespace Main
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _weaponHolder;

        [SerializeField] private Weapon[] _allWeapons;
        private Weapon _weapon;

        private Shop _shop;

        [Inject]
        public void Construct(Shop shop)
        {
            _shop = shop;
        }

        private void Awake()
        {
            _allWeapons = new Weapon[_weaponHolder.childCount];
            for (int i = 0; i < _allWeapons.Length; i++)
            {
                _allWeapons[i] = _weaponHolder.GetChild(i).GetComponent<Weapon>();
            }
        }

        private void Start()
        {
            ShopData shopData = SaveLoadProgress.LoadData<ShopData>(UniqSavingIdentefiers.ShopData);
            ChangeWeapon(_shop.Cells[shopData.CurrentSelected]);
        }

        private void OnEnable()
        {
            _shop.OnWeaponChanged += ChangeWeapon;
            GameManager.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            _shop.OnWeaponChanged -= ChangeWeapon;
            GameManager.OnGameOver -= GameOver;
        }

        private void GameOver()
        {
            enabled = false;
        }

        private void ChangeWeapon(Cell cell)
        {
            for (int i = 0; i < _allWeapons.Length; i++)
            {
                Weapon weapon = _allWeapons[i];
                if (weapon == cell.Weapon)
                {
                    weapon.gameObject.SetActive(true);
                    SetWeapon(weapon);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Attack()
        {
            _weapon?.Attack();
        }
    }
}


