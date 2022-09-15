using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.EventSystems;

namespace Main
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSencitivity;


        [SerializeField] private float _maxXPos = 2.5f;
        [SerializeField] private float _rotationLerpSpeed;

        [SerializeField] private Animator _animator;

        private FixedJoystick _fixedJoystick;

        private PointerEventData _pointerEventData;

        [Inject]
        public void Construct(FixedJoystick fixedJoystick)
        {
            _fixedJoystick = fixedJoystick;
        }

        private void Update()
        {
            Move();
            MoveAnimation();
        }

        private void MoveAnimation()
        {


            _animator.SetFloat(Animations.HorizontalValue, _fixedJoystick.Horizontal);
        }

        private void Move()
        {
            Vector3 offset = new Vector3(_fixedJoystick.Horizontal, 0f, 1f).normalized * _speed * Time.deltaTime;
            Vector3 nextPosition = transform.position + offset;

            nextPosition.x = Mathf.Clamp(nextPosition.x, -_maxXPos, _maxXPos);

            transform.position = nextPosition;
        }
    }
}

