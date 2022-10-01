using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class PistolAvarage : Pistol
    {
        [SerializeField] private float _intervalBetweenShots;

        public override void Start()
        {
            base.Start();
            IntervalBetweenShots = _intervalBetweenShots;
        }

        public override void Attack()
        {
            base.Attack();
        }
    }
}


