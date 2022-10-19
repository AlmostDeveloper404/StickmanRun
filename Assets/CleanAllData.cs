using UnityEngine;

namespace Main
{
    public class CleanAllData : MonoBehaviour
    {
        [ContextMenu("CleanData")]
        private void CleanData()
        {
            for (int i = 0; i < 4; i++)
            {
                SaveLoadProgress.DeleteData(i);
            }

        }
    }
}

