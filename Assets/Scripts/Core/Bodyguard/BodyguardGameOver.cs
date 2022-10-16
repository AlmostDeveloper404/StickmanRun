using UnityEngine;

namespace Main
{
    public class BodyguardGameOver : BodyguardBase
    {
        private Animator _animator;
        private PlayerController _playerController;

        public BodyguardGameOver(Animator animator, PlayerController playerController)
        {
            _animator = animator;
            _playerController = playerController;
        }

        public override void EntryState(Bodyguard bodyguard)
        {
            Vector3 direction = _playerController.transform.position - bodyguard.transform.position;
            bodyguard.transform.rotation = Quaternion.LookRotation(direction);

            _animator.SetTrigger(Animations.GameOver);
        }
        public override void UpdateState(Bodyguard bodyguard)
        {
            
        }

        public override void OnTriggerState(Bodyguard bodyguard, Enemy enemy)
        {
            
        }

    }
}


