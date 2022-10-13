using System;

namespace Main
{
    public static class GameCurrency
    {
        private static int _goldamount;

        public static event Action<int> OnGoldAmountChanged;

        public static int Gold { get { return _goldamount; } }

        public static void AddGold(int amount)
        {
            _goldamount += amount;

            GoldData goldData = new GoldData();
            goldData.Gold = _goldamount;
            Save(goldData);

            OnGoldAmountChanged?.Invoke(_goldamount);
        }

        public static void Load()
        {
            GoldData goldData = SaveLoadProgress.LoadData<GoldData>(UniqSavingIdentefiers.GameCurrency);
            if (goldData.Equals(default(GoldData)))
            {
                AddGold(0);
            }
            else
            {
                AddGold(goldData.Gold);
            }
        }

        public static void Save(GoldData data)
        {
            SaveLoadProgress.SaveData(data, UniqSavingIdentefiers.GameCurrency);
        }
    }
}

