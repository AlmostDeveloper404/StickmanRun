using UnityEngine;
using System;
using Zenject;

namespace Main
{
    [Serializable]
    public struct PlayerStatsData
    {
        public int GranadeAmount;
        public int MoneyAmount;
        public int BodyguardsAmount;
    }

    public class PlayerStats : MonoBehaviour
    {
        public event Action<int> OnBodyguardAmountChanged;
        public event Action<int> OnGranadeAmountChanged;
        public event Action<int> OnMoneyAmountChanged;

        private int _granadeAmount;
        private int _money;

        public void AddGranade(int amount)
        {
            _granadeAmount += amount;
            OnGranadeAmountChanged?.Invoke(_granadeAmount);
        }

        public void AddBodyguard(int amount)
        {
            OnBodyguardAmountChanged?.Invoke(amount);
        }

        public void AddMoney(int amount)
        {
            _money += amount;
            OnMoneyAmountChanged?.Invoke(_money);
        }
    }
}


