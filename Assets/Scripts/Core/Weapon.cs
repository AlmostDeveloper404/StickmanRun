using UnityEngine;
using System;

namespace Main
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        public float IntervalBetweenShots { get; protected set; }


        public virtual void Attack()
        {
            throw new NotImplementedException();
        }
    }
}

