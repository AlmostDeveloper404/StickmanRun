using UnityEngine;
using System;

namespace Main
{
    [Serializable]
    public struct ShopData
    {
        public int CurrentSelected;
        public int[] OpenedWeapons;
        public int LastCost;
        public bool AllWeaponsBought;
    }

    public class Shop : MonoBehaviour
    {
        public event Action<Cell> OnWeaponChanged;

        private ShopData _shopData;

        public Cell[] Cells { get { return _allWeaponCells; } }

        [SerializeField] private Cell[] _allWeaponCells;

        [SerializeField] private ShopPanal _panal;

        [SerializeField] private int _priceGrowing;

        private void Start()
        {
            Load();
        }

        private void Load()
        {
            _shopData = SaveLoadProgress.LoadData<ShopData>(UniqSavingIdentefiers.ShopData);

            if (!_shopData.Equals(default(ShopData)))
            {
                SetActiveSell(_allWeaponCells[_shopData.CurrentSelected]);
                SetupCells(_shopData.OpenedWeapons);

            }
            else
            {
                _shopData = new ShopData();

                _shopData.OpenedWeapons = new int[1] { 0 };
                _shopData.CurrentSelected = 0;
                _shopData.LastCost = _priceGrowing;
                _shopData.AllWeaponsBought = false;

                SetActiveSell(_allWeaponCells[_shopData.CurrentSelected]);
                SetupCells(_shopData.OpenedWeapons);
                Save(_shopData);
            }
            _panal.SetupShopPanal(_shopData);
        }

        private void SetupCells(int[] openWeaponsIndex)
        {
            for (int i = 0; i < _allWeaponCells.Length; i++)
            {
                Cell cell = _allWeaponCells[i];

                for (int c = 0; c < openWeaponsIndex.Length; c++)
                {
                    if (openWeaponsIndex[c] == cell.CellIndex)
                    {
                        cell.Open();
                        break;
                    }
                    else cell.Close();
                }
            }
        }

        private void Save(ShopData shopData)
        {
            SaveLoadProgress.SaveData(shopData, UniqSavingIdentefiers.ShopData);
        }


        [ContextMenu("Delete Saves")]
        private void DeleteData()
        {
            Debug.Log("Saves deleted!");
            SaveLoadProgress.DeleteData(UniqSavingIdentefiers.ShopData);
        }

        [ContextMenu("UnlockAll")]
        private void UnlockAll()
        {
            _shopData.CurrentSelected = 0;
            _shopData.OpenedWeapons = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            _shopData.AllWeaponsBought = true;
            _shopData.LastCost = 0;
            SaveLoadProgress.SaveData(_shopData, UniqSavingIdentefiers.ShopData);
        }

        public void SetActiveSell(Cell cell)
        {
            _shopData.CurrentSelected = cell.CellIndex;

            SaveLoadProgress.SaveData(_shopData, UniqSavingIdentefiers.ShopData);
            OnWeaponChanged?.Invoke(cell);
        }

        public void BuyNextWeapon()
        {
            ShopData shopData = new ShopData();
            int weaponsCount = _shopData.OpenedWeapons.Length;
            if (weaponsCount + 1 == _allWeaponCells.Length)
            {
                shopData.AllWeaponsBought = true;
                shopData.LastCost = 0;

            }
            else
            {
                shopData.AllWeaponsBought = false;
                shopData.LastCost = _shopData.LastCost + _priceGrowing;
            }

            shopData.OpenedWeapons = new int[weaponsCount + 1];
            for (int i = 0; i < shopData.OpenedWeapons.Length; i++)
            {
                shopData.OpenedWeapons[i] = i;
            }
            shopData.CurrentSelected = _shopData.CurrentSelected;

            SetupCells(shopData.OpenedWeapons);
            Save(shopData);
            GameCurrency.RemoveGold(_shopData.LastCost);
            _panal.SetupShopPanal(shopData);
            _shopData = shopData;

        }
    }
}

