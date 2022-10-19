using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Main
{
    public class GamePanal : PanalObject
    {
        [SerializeField] private Button _granadeButton;
        [SerializeField] private TMP_Text _granadeAmountText;

        private UIManager _uiManager;

        private void Awake()
        {
            _uiManager = GetComponentInParent<UIManager>();
        }

        private void OnEnable()
        {
            UpdateGranadeAmount(0);

            _uiManager.PlayerStats.OnGranadeAmountChanged += UpdateGranadeAmount;

            _granadeButton.onClick.AddListener(() => _uiManager.PlayerShooting.PrepareForThrowing());
        }
        private void OnDisable()
        {
            _uiManager.PlayerStats.OnGranadeAmountChanged -= UpdateGranadeAmount;

            _granadeButton.onClick.RemoveAllListeners();
        }

        private void UpdateGranadeAmount(int amount)
        {
            _granadeAmountText.outlineColor = amount == 0 ? Color.red : Color.green;
            _granadeAmountText.text = amount.ToString();
        }

    }
}


