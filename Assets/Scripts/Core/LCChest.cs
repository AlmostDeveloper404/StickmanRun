using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class LCChest : MonoBehaviour
    {
        private const int Amount = 10;


        public void CollectGold()
        {
            GameCurrency.AddGold(Amount * SceneManager.GetActiveScene().buildIndex);
        }
    }
}

