using UnityEngine;

namespace Main
{
    public class BodyguardDefend : BodyguardBase
    {
        private Enemy _enemy;
        private Animator _animator;
        private PlayerBodyguards _playerBodyguards;

        public BodyguardDefend(Enemy enemy, Animator animator, PlayerBodyguards playerBodyguards)
        {
            _enemy = enemy;
            _animator = animator;
            _playerBodyguards = playerBodyguards;
        }

        public override void EntryState(Bodyguard bodyguard)
        {
            bodyguard.transform.rotation = Quaternion.identity;
        }

        public override void UpdateState(Bodyguard bodyguard)
        {
            Debug.Log(_enemy.IsAttacked);
            if (_enemy.IsAttacked)
            {
                bodyguard.ChangeState(bodyguard.BodyguardSecureState);
                return;
            }


            Vector3 direction = _enemy.transform.position - bodyguard.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            bodyguard.transform.rotation = lookRotation;
            bodyguard.transform.position += bodyguard.transform.forward * bodyguard.AttackSpeed * Time.deltaTime;

        }
        public override void OnTriggerState(Bodyguard bodyguard, Enemy enemy)
        {
            bodyguard.BodyguardAttackState = new BodyguardAttack(_animator, enemy, _playerBodyguards);
            bodyguard.ChangeState(bodyguard.BodyguardAttackState);
        }

    }
}


