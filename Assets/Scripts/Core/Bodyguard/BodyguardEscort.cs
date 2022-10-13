using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class BodyguardEscort : BodyguardBase
    {
        private Animator _animator;
        private DynamicJoystick _dynamicJoystick;

        private BodyguardPoint _bodyguardsPoint;
        private PlayerBodyguards _playerBodyguards;

        public BodyguardEscort(Animator animator, DynamicJoystick dynamicJoystick, BodyguardPoint bodyguardPoint, PlayerBodyguards playerBodyguards)
        {
            _animator = animator;
            _dynamicJoystick = dynamicJoystick;
            _bodyguardsPoint = bodyguardPoint;
            _playerBodyguards = playerBodyguards;
        }

        public override void EntryState(Bodyguard bodyguard)
        {
            bodyguard.transform.rotation = Quaternion.identity;
            _animator.SetTrigger(Animations.StartGame);
        }
        public override void UpdateState(Bodyguard bodyguard)
        {
            _animator.SetFloat(Animations.HorizontalValue, _dynamicJoystick.Horizontal);
            bodyguard.transform.position = Vector3.Lerp(bodyguard.transform.position, _bodyguardsPoint.transform.position, bodyguard.LerpingSpeed * Time.deltaTime);
        }

        public override void OnTriggerState(Bodyguard bodyguard, Enemy enemy)
        {
            bodyguard.BodyguardAttack = new BodyguardAttack(_animator, enemy, _playerBodyguards);
            bodyguard.ChangeState(bodyguard.BodyguardAttack);
        }

    }
}

