using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Main
{
    public class ShopPanal : PanalObject
    {

        [SerializeField] private Button _backButton;
        [SerializeField] private Button _buyButton;

        [SerializeField] private TMP_Text _costAmountText;
        private UIManager _uiManager;

        private Shop _shop;

        [Inject]
        private void Construct(Shop shop)
        {
            _shop = shop;
        }

        private void Start()
        {
            _uiManager = GetComponentInParent<UIManager>();
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(() => _shop.BuyNextWeapon());
            _backButton.onClick.AddListener(() => Back());
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
        }
        private void Back()
        {
            _uiManager.ChangePanal(_uiManager.PreporationPanal);
        }

        public void SetupShopPanal(ShopData shopData)
        {
            _buyButton.gameObject.SetActive(shopData.AllWeaponsBought ? false : true);

            _buyButton.interactable = GameCurrency.Gold >= shopData.LastCost ? true : false;
            _costAmountText.text = shopData.LastCost.ToString();
        }
    }
}


