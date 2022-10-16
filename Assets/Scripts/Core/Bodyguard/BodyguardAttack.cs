using UnityEngine;

namespace Main
{
    public class BodyguardAttack : BodyguardBase
    {
        private Animator _animator;
        private Enemy _enemy;
        private PlayerBodyguards _playerBodyguards;


        public BodyguardAttack(Animator animator, Enemy enemy, PlayerBodyguards playerBodyguards)
        {
            _animator = animator;
            _enemy = enemy;
            _playerBodyguards = playerBodyguards;
        }

        public override void EntryState(Bodyguard bodyguard)
        {
            bodyguard.transform.parent = null;

            Vector3 direction = _enemy.transform.position - bodyguard.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            bodyguard.transform.rotation = lookRotation;

            _enemy.IsAttacked = true;
            _enemy.Death();

            _animator.SetTrigger(Animations.AttackHash);
            _playerBodyguards.RemoveBodyguard(bodyguard);

        }


        public override void UpdateState(Bodyguard bodyguard)
        {
            if (_enemy.IsAttacked)
            {
                return;
            }
            else
            {
                bodyguard.ChangeState(bodyguard.BodyguardSecureState);
            }
        }


        public override void OnTriggerState(Bodyguard bodyguard, Enemy enemy)
        {

        }
    }
}

