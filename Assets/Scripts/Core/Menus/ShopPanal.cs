using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class ShopPanal : PanalObject
    {

        [SerializeField] private Button _backButton;
        private UIManager _uiManager;

        private void Start()
        {
            _uiManager = GetComponentInParent<UIManager>();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(() => Back());
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
        }
        private void Back()
        {
            _uiManager.ChangePanal(_uiManager.PreporationPanal);
        }
    }
}


