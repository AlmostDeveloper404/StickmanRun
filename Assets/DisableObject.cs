using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class DisableObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.SetActive(false);
        }
    }

}
