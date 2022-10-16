using UnityEngine;

namespace Main
{
    public class BodyguardWinning : BodyguardBase
    {
        private Animator _animator;

        public BodyguardWinning(Animator animator)
        {
            _animator = animator;
        }

        public override void EntryState(Bodyguard bodyguard)
        {
            bodyguard.transform.parent = null;
            _animator.SetTrigger(Animations.LevelCompleted);
        }
        public override void UpdateState(Bodyguard bodyguard)
        {

        }

        public override void OnTriggerState(Bodyguard bodyguard, Enemy enemy)
        {

        }

    }
}

