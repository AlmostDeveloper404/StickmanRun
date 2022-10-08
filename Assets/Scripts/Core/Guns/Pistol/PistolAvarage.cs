using UnityEngine;

namespace Main
{
    public class PistolAvarage : Pistol
    {
        [SerializeField] private float _intervalBetweenShots;

        public override void OnEnable()
        {
            base.OnEnable();
            IntervalBetweenShots = _intervalBetweenShots;
        }

        public override void Attack()
        {
            base.Attack();
        }
    }
}


