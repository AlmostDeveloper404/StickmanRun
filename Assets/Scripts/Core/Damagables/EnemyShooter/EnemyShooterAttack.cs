using UnityEngine;

namespace Main
{
    public class EnemyShooterAttack : EnemyBase<EnemyShooter>
    {
        private PlayerController _playerController;

        public EnemyShooterAttack(PlayerController playerController)
        {
            _playerController = playerController;
        }


        public override void EntryState(EnemyShooter straightForwardEnemy, Animator animator)
        {
            animator.SetTrigger(Animations.AttackHash);
        }

        public override void UpdateState(EnemyShooter straightForwardEnemy)
        {
            Vector3 direction = straightForwardEnemy.transform.position - _playerController.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

            straightForwardEnemy.transform.rotation = lookRotation;
        }
    }
}


