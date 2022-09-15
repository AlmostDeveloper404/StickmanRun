using UnityEngine;

namespace Main
{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Guns/New Gun")]
    public class GunsInfo : ScriptableObject
    {
        public GameObject BulletPref;
        public GameObject GunPref;
        public float TimeBetweenShots;
        public string Name;
        public string Description;
    }
}

