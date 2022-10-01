using UnityEngine;
using Zenject;
using UnityEngine.UI;
using Helpers;
using UnityEngine.EventSystems;

namespace Main
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _shootButton;
        [SerializeField] private Button _tapToPlayButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _shopButton;

        [SerializeField] private GameObject _preporationsPanal;
        [SerializeField] private GameObject _gamePanal;
        [SerializeField] private GameObject _shopPanal;

        private PlayerShooting _playerShooting;
        [SerializeField] private EventTrigger _onButtonDown;
        [SerializeField] private EventTrigger _onButtonUp;

        [Inject]
        public void Construct(PlayerShooting playerShooting, Shop shop)
        {
            _playerShooting = playerShooting;
        }

        private void OnEnable()
        {
            GameManager.OnGameOver += GameOver;
            GameManager.OnStatedPreporations += StartPreporations;
            GameManager.OnGameStarted += StartGame;


            _shopButton.onClick.AddListener(() => ToShop());
            _backButton.onClick.AddListener(() => Back());
            _shootButton.onClick.AddListener(() => _playerShooting.Attack());
            _tapToPlayButton.onClick.AddListener(() => GameManager.ChangeGameState(GameState.StartGame));
        }
        private void OnDisable()
        {
            GameManager.OnGameOver -= GameOver;
            GameManager.OnStatedPreporations -= StartPreporations;
            GameManager.OnGameStarted -= StartGame;

            _backButton.onClick.RemoveAllListeners();
            _shootButton.onClick.RemoveAllListeners();
            _tapToPlayButton.onClick.RemoveAllListeners();
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



    }
}


