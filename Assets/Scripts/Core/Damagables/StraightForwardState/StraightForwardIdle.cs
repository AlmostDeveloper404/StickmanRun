using UnityEngine;

namespace Main
{
    public class StraightForwardIdle : EnemyBase<StraightForwardEnemy>
    {
        private PlayerController _playerController;

        public StraightForwardIdle(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public override void EntryState(StraightForwardEnemy straightForwardEnemy, Animator animator)
        {

        }

        public override void UpdateState(StraightForwardEnemy straightForwardEnemy)
        {
            if (Vector3.Distance(straightForwardEnemy.transform.position, _playerController.transform.position) < straightForwardEnemy.DistanceToCheckForPlayer)
            {
                straightForwardEnemy.ChangeState(straightForwardEnemy.StraightForwardAttackState);
            }
        }
    }
}



