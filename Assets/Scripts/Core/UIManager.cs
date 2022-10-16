using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;

namespace Main
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _shootButton;
        [SerializeField] private Button _tapToPlayButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _granadeButton;

        [SerializeField] private GameObject _preporationsPanal;
        [SerializeField] private GameObject _gamePanal;
        [SerializeField] private GameObject _shopPanal;

        [SerializeField] private TMP_Text _goldText;
        [SerializeField] private TMP_Text _granadeAmountText;

        private PlayerShooting _playerShooting;
        private PlayerStats _playerStats;

        [Inject]
        private void Construct(PlayerShooting playerShooting, PlayerStats playerStats)
        {
            _playerShooting = playerShooting;
            _playerStats = playerStats;
        }

        private void OnEnable()
        {
            GameCurrency.OnGoldAmountChanged += UpdateGold;

            _playerStats.OnGranadeAmountChanged += UpdateGranadeAmount;

            GameManager.OnGameOver += GameOver;
            GameManager.OnStatedPreporations += StartPreporations;
            GameManager.OnGameStarted += StartGame;

            _granadeButton.onClick.AddListener(() => _playerShooting.PrepareForThrowing());
            _shopButton.onClick.AddListener(() => ToShop());
            _backButton.onClick.AddListener(() => Back());
            _tapToPlayButton.onClick.AddListener(() => GameManager.ChangeGameState(GameState.StartGame));
        }
        private void OnDisable()
        {
            GameCurrency.OnGoldAmountChanged -= UpdateGold;

            _playerStats.OnGranadeAmountChanged -= UpdateGranadeAmount;

            GameManager.OnGameOver -= GameOver;
            GameManager.OnStatedPreporations -= StartPreporations;
            GameManager.OnGameStarted -= StartGame;

            _backButton.onClick.RemoveAllListeners();
            _tapToPlayButton.onClick.RemoveAllListeners();
        }

        private void UpdateGold(int amount)
        {
            _goldText.text = amount.ToString();
        }

        private void GameOver()
        {
            _shootButton.interactable = false;
        }

        private void StartPreporations()
        {
            _gamePanal.SetActive(false);
            _preporationsPanal.SetActive(true);
        }

        private void StartGame()
        {
            _gamePanal.SetActive(true);
            _preporationsPanal.SetActive(false);
            UpdateGranadeAmount(0);
        }

        private void ToShop()
        {
            _preporationsPanal.SetActive(false);
            _shopPanal.SetActive(true);
        }

        private void Back()
        {
            _shopPanal.SetActive(false);
            _preporationsPanal.SetActive(true);
        }

        private void UpdateGranadeAmount(int amount)
        {
            _granadeAmountText.outlineColor = amount == 0 ? Color.red : Color.green;
            _granadeAmountText.text = amount.ToString();
        }



    }
}


