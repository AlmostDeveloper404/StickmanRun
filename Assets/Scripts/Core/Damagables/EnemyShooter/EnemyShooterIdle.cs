using UnityEngine;

namespace Main
{
    public class EnemyShooterIdle : EnemyBase<EnemyShooter>
    {
        private PlayerController _playerController;

        public EnemyShooterIdle(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public override void EntryState(EnemyShooter straightForwardEnemy, Animator animator)
        {

        }

        public override void UpdateState(EnemyShooter straightForwardEnemy)
        {
            if (Vector3.Distance(straightForwardEnemy.transform.position, _playerController.transform.position) < straightForwardEnemy.DistanceToCheckForPlayer)
            {
                straightForwardEnemy.ChangeState(straightForwardEnemy.EnemyShooterAttack);
            }
        }
    }
}


