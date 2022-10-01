using UnityEngine;
using System;

namespace Main
{
    [Serializable]
    public struct ShopData
    {
        public int CurrentSelected;
        public int[] OpenedWeapons;
    }

    public class Shop : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        public event Action<Cell> OnWeaponChanged;

        private ShopData _shopData;

        public Cell[] Cells { get { return _allWeaponCells; } }

        [SerializeField] private Cell[] _allWeaponCells;

        private void Start()
        {
            Load();
        }


        private void Save(ShopData shopData)
        {
            SaveLoadProgress.SaveData(shopData, UniqSavingIdentefiers.ShopData);
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

                SetActiveSell(_allWeaponCells[_shopData.CurrentSelected]);
                SetupCells(_shopData.OpenedWeapons);
                Save(_shopData);

            }
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
            SaveLoadProgress.SaveData(_shopData, UniqSavingIdentefiers.ShopData);
        }

        public void SetActiveSell(Cell cell)
        {
            _shopData.CurrentSelected = cell.CellIndex;

            SaveLoadProgress.SaveData(_shopData, UniqSavingIdentefiers.ShopData);
            OnWeaponChanged?.Invoke(cell);
        }
    }
}

