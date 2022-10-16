using UnityEngine;
using UniRx;

namespace Main
{
    public class Enemy : MonoBehaviour
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        public bool IsAttacked { get; set; } = false;

        protected virtual void OnEnable()
        {
            GameManager.OnGameStarted += StartGame;
            GameManager.OnStatedPreporations += StartPreporations;
            GameManager.OnGameOver += GameOver;
            Observable.EveryUpdate().Subscribe(_ => CheckUpdate()).AddTo(_disposables);
        }


        protected virtual void OnDisable()
        {
            GameManager.OnGameStarted -= StartGame;
            GameManager.OnGameOver -= GameOver;
            GameManager.OnStatedPreporations -= StartPreporations;
            _disposables?.Clear();
        }

        protected virtual void CheckUpdate()
        {

        }

        protected virtual void StartPreporations()
        {

        }

        protected virtual void StartGame()
        {

        }
        protected virtual void GameOver()
        {

        }

        public virtual void Death()
        {
            IsAttacked = true;
            _disposables?.Clear();
        }

        public void Disable()
        {
            _disposables?.Clear();
            gameObject.SetActive(false);
        }

    }
}


