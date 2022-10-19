using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Main
{
    public class PreporationPanal : PanalObject
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _tapToPlayButton;

        [SerializeField] private TMP_Text _levelText;

        private UIManager _uiManager;

        private void Start()
        {
            _uiManager = GetComponentInParent<UIManager>();
            UpdateLevelText();
        }

        private void OnEnable()
        {

            _tapToPlayButton.onClick.AddListener(() => GameManager.ChangeGameState(GameState.StartGame));
            _shopButton.onClick.AddListener(() => ToShop());
        }

        private void OnDisable()
        {

            _tapToPlayButton.onClick.RemoveAllListeners();
            _shopButton.onClick.RemoveAllListeners();
        }

        private void ToShop()
        {
            _uiManager.ChangePanal(_uiManager.ShopPanal);
        }

        private void UpdateLevelText()
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            _levelText.text = $"Level {currentLevel}";
        }
    }
}


