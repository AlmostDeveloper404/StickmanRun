using UnityEngine;

namespace Main
{
    public class BodyguardAnimation : MonoBehaviour
    {
        [SerializeField] private Bodyguard _bodyguard;

        public void DisableAnimator()
        {
            _bodyguard.DisableAnimator();
        }
    }
}


