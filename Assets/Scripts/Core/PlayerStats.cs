using UnityEngine;
using System;

namespace Main
{
    [Serializable]
    public struct GoldData
    {
        public int Gold;
    }

    public class PlayerStats : MonoBehaviour
    {
        public event Action<int> OnBodyguardAmountChanged;
        public event Action<int> OnGranadeAmountChanged;

        private int _granadeAmount;

        private void Start()
        {
            GameCurrency.Load();
        }

        public void AddGranade(int amount)
        {
            _granadeAmount += amount;
            OnGranadeAmountChanged?.Invoke(_granadeAmount);
        }

        public void AddBodyguard(int amount)
        {
            OnBodyguardAmountChanged?.Invoke(amount);
        }
    }
}


