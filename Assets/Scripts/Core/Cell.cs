using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main
{
    [RequireComponent(typeof(Button))]
    public class Cell : MonoBehaviour
    {
        [SerializeField] private GameObject _selectedImage;
        [SerializeField] private GameObject _lockedImage;

        [SerializeField] private Weapon _gunWeapon;

        public Weapon Weapon { get { return _gunWeapon; } }

        private Shop _shop;
        private Button _cellButton;

        public int CellIndex;


        [Inject]
        public void Construct(Shop shop)
        {
            _shop = shop;
        }

        private void Awake()
        {
            _cellButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _shop.OnWeaponChanged += SetCell;
            _cellButton.onClick.AddListener(() => _shop.SetActiveSell(this));
        }

        private void OnDisable()
        {
            _shop.OnWeaponChanged -= SetCell;
            _cellButton.onClick.RemoveAllListeners();
        }
        public void Open()
        {
            _cellButton.interactable = true;
            _lockedImage.SetActive(false);
        }

        public void Close()
        {
            _cellButton.interactable = false;
            _lockedImage.SetActive(true);
        }


        private void SetCell(Cell cell)
        {
            _selectedImage.SetActive(cell == this);
        }

    }
}


