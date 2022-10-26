using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Main
{
    public class EndGamePanal : PanalObject
    {
        [SerializeField] private Color _winColor;
        [SerializeField] private Color _lostColor;

        [SerializeField] private TMP_Text _endText;
        [SerializeField] private TMP_Text _gainedText;
        [SerializeField] private TMP_Text _buttonText;

        [SerializeField] private Image _endImage;
        [SerializeField] private Button _endButton;

        private UIManager _uiManager;

        private void Awake()
        {
            _uiManager = GetComponentInParent<UIManager>();
        }



        private void OnEnable()
        {

            Setup();
        }


        private void Setup()
        {
            if (_uiManager.IsLost)
            {
                Lost();
            }
            else
            {
                Win();
            }
        }

        private void Win()
        {
            _buttonText.text = "Ñontinue";
            _gainedText.text = $"{_uiManager.GoldEarned}";
            _endButton.onClick.AddListener(() => GameManager.NextLevel());
            _endImage.color = _winColor;
            _endText.text = $"Level Completed!";
        }

        private void Lost()
        {
            _gainedText.text = $"+{_uiManager.GoldEarned}";
            _buttonText.text = "Retry?";
            _endButton.onClick.AddListener(() => GameManager.Restart());
            _endImage.color = _lostColor;
            _endText.text = $"Game Over!";
        }

        private void OnDestroy()
        {
            _endButton.onClick.RemoveAllListeners();
        }
    }
}


