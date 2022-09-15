using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class WeaponBase : MonoBehaviour
    {
        public int Damage { get; set; }
        public IDoDamage DoDamageWeapon { get; private set; }

        public void TryAttack()
        {
            DoDamageWeapon?.DoDamage(Damage);
        }

        public void SetWeapon(IDoDamage doDamage)
        {
            DoDamageWeapon = doDamage;
        }
    }
}


