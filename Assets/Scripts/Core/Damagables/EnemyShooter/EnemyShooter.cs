using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Main
{
    public class EnemyShooter : Enemy, ITakeDamage
    {
        private EnemyBase<EnemyShooter> _currentState;

        public EnemyShooterIdle EnemyShooterIdle { get; private set; }
        public EnemyShooterAttack EnemyShooterAttack { get; private set; }

        [SerializeField] private Animator _animator;

        [SerializeField] private float _checkForPlayerDistance;

        public float DistanceToCheckForPlayer { get { return _checkForPlayerDistance; } }

        private PlayerController _playerController;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
            EnemyShooterIdle = new EnemyShooterIdle(_playerController);
            EnemyShooterAttack = new EnemyShooterAttack(_playerController);
        }


        protected override void OnEnable()
        {
            base.OnEnable();

            _currentState = EnemyShooterIdle;
            _currentState.EntryState(this, _animator);

        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void CheckUpdate()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(EnemyBase<EnemyShooter> enemyShooterBase)
        {
            _currentState = enemyShooterBase;
            _currentState.EntryState(this, _animator);
        }

        protected override void GameOver()
        {
            base.GameOver();
        }
        protected override void StartGame()
        {
            base.StartGame();
        }

        protected override void StartPreporations()
        {
            base.StartPreporations();
        }

        public void TakeDamage()
        {
            Death();
            
        }

        public override void Death()
        {
            base.Death();
            _animator.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}


