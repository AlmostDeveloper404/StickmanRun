using UnityEngine;
using Zenject;

namespace Main
{
    public class DropArea : MonoBehaviour
    {
        private bool _isDropUsed = false;

        private PlayerStats _playerStats;

        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        public void AddDrop(DropType dropType, int amount)
        {
            if (_isDropUsed) return;

            _isDropUsed = true;

            switch (dropType)
            {
                case DropType.Bodyguard:
                    _playerStats.AddBodyguard(amount);
                    break;
                case DropType.Money:
                    GameCurrency.AddGold(amount);
                    break;
                case DropType.Granade:
                    _playerStats.AddGranade(amount);
                    break;
                default:
                    break;
            }
        }

    }
}


