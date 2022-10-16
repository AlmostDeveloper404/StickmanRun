using UnityEngine;
using System.Linq;

namespace Main
{
    public class Activailable : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objectsToActivate;
        [SerializeField] private MonoBehaviour[] _behaviours;

        private void Awake()
        {
            Deactivate();
        }

        public void Activate()
        {
            Debug.Log("Yep");
            foreach (var obj in _objectsToActivate)
            {
                obj.SetActive(true);
            }
            foreach (var item in _behaviours)
            {
                item.enabled = true;
            }
        }

        public void Deactivate()
        {
            foreach (var obj in _objectsToActivate)
            {
                obj.SetActive(false);
            }
            foreach (var item in _behaviours)
            {
                item.enabled = false;
            }
        }
    }
}

