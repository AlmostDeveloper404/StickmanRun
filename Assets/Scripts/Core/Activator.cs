using UnityEngine;
using UniRx.Triggers;
using UniRx;
using Zenject;

namespace Main
{
    public class Activator : MonoBehaviour
    {
        [SerializeField] private Collider _activatorSquareCollider;

        private CompositeDisposable _onTriggerEnterDisposable = new CompositeDisposable();
        private CompositeDisposable _onTriggerExitDisposable = new CompositeDisposable();

        private PlayerController _playerController;

        private Vector3 _currentPos;

        [Inject]
        private void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }


        private void Start()
        {
            _activatorSquareCollider.OnTriggerEnterAsObservable().Where(t => t.gameObject.GetComponent<Activailable>()).Subscribe(_ => Activate(_)).AddTo(_onTriggerEnterDisposable);
            _activatorSquareCollider.OnTriggerExitAsObservable().Where(t => t.gameObject.GetComponent<Activailable>()).Subscribe(_ => Deactivate(_)).AddTo(_onTriggerExitDisposable);
        }

        private void Update()
        {
            _currentPos = transform.position;
            _currentPos.z = _playerController.transform.position.z;
            transform.position = _currentPos;
        }

        private void Activate(Collider collider)
        {
            Activailable activailable = collider.GetComponent<Activailable>();
            activailable.Activate();
        }

        private void Deactivate(Collider collider)
        {
            Activailable activailable = collider.GetComponent<Activailable>();
            activailable.Deactivate();
        }

        private void OnDestroy()
        {
            _onTriggerEnterDisposable?.Clear();
            _onTriggerExitDisposable?.Clear();
        }

    }
}

