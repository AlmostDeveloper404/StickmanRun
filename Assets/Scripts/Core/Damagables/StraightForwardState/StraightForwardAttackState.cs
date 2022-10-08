using UnityEngine;

namespace Main
{
    public class StraightForwardAttackState : EnemyBase<StraightForwardEnemy>
    {
        private PlayerController _playerController;

        public StraightForwardAttackState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public override void EntryState(StraightForwardEnemy straightForwardEnemy, Animator animator)
        {
            animator.SetTrigger(Animations.AttackHash);
        }

        public override void UpdateState(StraightForwardEnemy straightForwardEnemy)
        {
            Vector3 direction = (straightForwardEnemy.transform.position - _playerController.transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            straightForwardEnemy.transform.rotation = Quaternion.Lerp(straightForwardEnemy.transform.rotation, lookRotation, straightForwardEnemy.RotationSpeed * Time.deltaTime);

            straightForwardEnemy.transform.position += -straightForwardEnemy.transform.forward * straightForwardEnemy.Speed * Time.deltaTime;
        }
    }
}


