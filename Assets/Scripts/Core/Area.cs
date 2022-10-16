using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace Main
{
    public enum DropType { Bodyguard, Money, Granade }

    public class Area : MonoBehaviour
    {
        private DropArea _dropArea;
        [SerializeField] private DropType _dropType;
        [SerializeField] private int _amount;

        [SerializeField] private Sprite _bodyguard;
        [SerializeField] private Sprite _coins;
        [SerializeField] private Sprite _granade;

        [SerializeField] private Image _dropImage;
        [SerializeField] private TMP_Text _dropAmount;

        private void Start()
        {
            SetupArea();
        }

        private void SetupArea()
        {
            _dropArea = GetComponentInParent<DropArea>();

            switch (_dropType)
            {
                case DropType.Bodyguard:
                    _dropImage.sprite = _bodyguard;
                    _dropAmount.text = $"+{_amount}";
                    break;
                case DropType.Money:
                    _dropImage.sprite = _coins;
                    _dropAmount.text = $"+{_amount}";
                    break;
                case DropType.Granade:
                    _dropImage.sprite = _granade;
                    _dropAmount.text = $"+{_amount}";
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController)
            {
                _dropArea.AddDrop(_dropType, _amount);
            }
        }

        private void UpdateArea()
        {
            throw new NotImplementedException();
        }
    }
}


