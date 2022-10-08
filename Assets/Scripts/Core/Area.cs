using UnityEngine;
using System;

namespace Main
{
    public enum DropType { Bodyguard, Money, Granade }

    public class Area : MonoBehaviour
    {
        [SerializeField] private DropArea _dropArea;
        [SerializeField] private DropType _dropType;
        [SerializeField] private int _amount;

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


