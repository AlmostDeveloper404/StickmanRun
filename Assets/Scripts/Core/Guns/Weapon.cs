using UnityEngine;
using Zenject;

namespace Main
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        public float IntervalBetweenShots { get; protected set; }

        [SerializeField] private AudioClip _clip;

        private SoundManager _soundManager;

        [Inject]
        public void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        public virtual void Attack()
        {
            //_soundManager.PlaySound(_clip);
        }
    }
}

