using UnityEngine;

namespace Main
{
    public class LCChest : MonoBehaviour
    {
        [SerializeField] private int _amount;

        public void CollectGold()
        {
            GameCurrency.AddGold(_amount);
        }
    }
}

