using UnityEngine;

namespace Main
{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Guns/New Gun")]
    public class GunsInfo : ScriptableObject
    {
        public Sprite GunSprite;
        public GameObject GunPref;
        public GameObject BulletPref;
        public float TimeBetweenShots;
        public string Name;
        public string Description;
    }
}

