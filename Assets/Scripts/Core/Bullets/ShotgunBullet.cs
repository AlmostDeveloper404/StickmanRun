using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class ShotgunBullet : Bullet
    {
        protected override void CheckUpdate()
        {
            transform.localPosition += transform.forward * Speed * Time.deltaTime;
        }
    }
}

