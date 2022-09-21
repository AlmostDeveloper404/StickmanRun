using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Wall : MonoBehaviour, ITakeDamage
    {
        public void TakeDamage()
        {
            Debug.Log("I'm a wall");
        }
    }
}

