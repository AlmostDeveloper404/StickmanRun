using UnityEngine;
using Zenject;


namespace Main
{
    public class CameraMovement : MonoBehaviour
    {
        private PlayerController _playerController;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void LateUpdate()
        {
            transform.position = _playerController.transform.position;
        }

    }

}

