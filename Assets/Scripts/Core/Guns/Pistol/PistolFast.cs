using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class PistolFast : Pistol
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


