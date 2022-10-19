using UnityEngine;
using System.Linq;

namespace Main
{
    public class Activailable : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objectsToActivate;
        [SerializeField] private MonoBehaviour[] _behaviours;

        [SerializeField] private bool _disableSelf = false;

        private void Awake()
        {
            Deactivate();
        }

        public void Activate()
        {
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
            if (_disableSelf) gameObject.SetActive(false);

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

