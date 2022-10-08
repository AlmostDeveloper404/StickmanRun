using UnityEngine;
using Zenject;
using UniRx;

namespace Main
{
    public class PlayerShooting : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private GameObject _granadePref;
        [SerializeField] private Transform _granadeSpawnPoint;

        private Weapon[] _allWeapons;
        private Weapon _weapon;

        private Weapon _currentWeapon;

        private Shop _shop;
        private PlayerStats _playerStats;


        private CompositeDisposable _updateDisposable = new CompositeDisposable();

        private bool _isHolded = false;
        private float _timer;

        private ObjectPool<Granade> _granadePool;


        private Granade _currentGranade;

        private bool _canThrow;
        private int _granadeAmount;

        [Inject]
        public void Construct(Shop shop, PlayerStats playerStats)
        {
            _shop = shop;
            _playerStats = playerStats;
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
            _canThrow = true;
            _granadePool = new ObjectPool<Granade>(_granadePref, 1);

            ShopData shopData = SaveLoadProgress.LoadData<ShopData>(UniqSavingIdentefiers.ShopData);
            ChangeWeapon(_shop.Cells[shopData.CurrentSelected]);
        }

        private void OnEnable()
        {
            _playerStats.OnGranadeAmountChanged += GranadeAmountChanged;
            _shop.OnWeaponChanged += ChangeWeapon;
            GameManager.OnGameOver += GameOver;
            GameManager.OnGameStarted += StartGame;
        }

        private void OnDisable()
        {
            _playerStats.OnGranadeAmountChanged -= GranadeAmountChanged;
            _shop.OnWeaponChanged -= ChangeWeapon;
            GameManager.OnGameOver -= GameOver;
            GameManager.OnGameStarted -= StartGame;
        }

        private void GranadeAmountChanged(int amount)
        {
            _granadeAmount = amount;
        }

        private void StartGame()
        {
            Observable.EveryUpdate().Subscribe(_ => TryShoot()).AddTo(_updateDisposable);
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

        public void IsHolded(bool result)
        {
            _isHolded = result;
        }

        private void TryShoot()
        {
            if (!_weapon) return;

            _timer += Time.deltaTime;
            if (_timer > _weapon.IntervalBetweenShots && _isHolded)
            {
                _timer = 0;
                Attack();
            }
        }

        public void PrepareForThrowing()
        {
            if (!_canThrow || _granadeAmount <= 0) return;

            _canThrow = false;
            _playerStats.AddGranade(-1);
            _currentGranade = _granadePool.Pull();
            _currentGranade.transform.parent = _granadeSpawnPoint;
            _currentGranade.transform.localPosition = _granadeSpawnPoint.localPosition;
            _currentGranade.transform.localRotation = _granadeSpawnPoint.localRotation;
            _animator.SetTrigger(Animations.ThrowGranade);
        }

        public void ThrowGranade()
        {
            _currentGranade.Throw();
        }

        public void StopShooting()
        {
            _currentWeapon = _weapon;
            _weapon.gameObject.SetActive(false);
            _weapon = null;
        }

        public void StartShooting()
        {
            _weapon = _currentWeapon;
            _weapon.gameObject.SetActive(true);
            _canThrow = true;
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Attack()
        {
            if (_weapon)
            {
                _weapon.Attack();
            }
        }



        private void OnDestroy()
        {
            _updateDisposable?.Clear();
        }
    }
}


