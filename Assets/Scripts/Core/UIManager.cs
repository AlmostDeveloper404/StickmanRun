using UnityEngine;
using Zenject;
using TMPro;

namespace Main
{
    public class UIManager : MonoBehaviour
    {


        [SerializeField] private ShopPanal _shopPanal;
        [SerializeField] private PreporationPanal _preporationsPanal;
        [SerializeField] private GamePanal _gamePanal;
        [SerializeField] private EndGamePanal _endPanal;

        [SerializeField] private TMP_Text _goldText;



        private PanalObject _currentOpenedPanal;
        public ShopPanal ShopPanal { get { return _shopPanal; } }
        public PreporationPanal PreporationPanal { get { return _preporationsPanal; } }
        public GamePanal GamePanal { get { return _gamePanal; } }
        public EndGamePanal EndGamePanal { get { return _endPanal; } }

        private PlayerShooting _playerShooting;
        private PlayerStats _playerStats;

        public PlayerShooting PlayerShooting { get { return _playerShooting; } }
        public PlayerStats PlayerStats { get { return _playerStats; } }

        private bool _isLost;

        public bool IsLost { get { return _isLost; } }

        [Inject]
        private void Construct(PlayerShooting playerShooting, PlayerStats playerStats)
        {
            _playerShooting = playerShooting;
            _playerStats = playerStats;
        }


        private void OnEnable()
        {
            GameCurrency.OnGoldAmountChanged += UpdateGold;
            GameManager.OnGameOver += GameOver;
            GameManager.OnStatedPreporations += StartPreporations;
            GameManager.OnGameStarted += StartGame;
            GameManager.OnLevelCompleted += LevelCompleted;
        }

        private void OnDisable()
        {
            GameCurrency.OnGoldAmountChanged -= UpdateGold;
            GameManager.OnGameOver -= GameOver;
            GameManager.OnStatedPreporations -= StartPreporations;
            GameManager.OnGameStarted -= StartGame;
            GameManager.OnLevelCompleted -= LevelCompleted;

        }
        private void StartPreporations()
        {
            ChangePanal(PreporationPanal);
        }

        private void StartGame()
        {
            ChangePanal(GamePanal);
        }

        private void LevelCompleted()
        {
            _isLost = false;
        }
        private void GameOver()
        {
            _isLost = true;
        }

        private void UpdateGold(int amount)
        {
            _goldText.text = amount.ToString();
        }



        public void ChangePanal(PanalObject panalObject)
        {
            _currentOpenedPanal?.gameObject.SetActive(false);
            _currentOpenedPanal = panalObject;
            _currentOpenedPanal.gameObject.SetActive(true);
        }



    }
}


