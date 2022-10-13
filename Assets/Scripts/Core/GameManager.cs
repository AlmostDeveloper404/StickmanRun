using System;

namespace Main
{
    public enum GameState { Preporations, StartGame, StartGameOverCatScene, BossFight, LevelCompleted, GameOver }


    public static class GameManager
    {
        public static event Action OnGameOver;
        public static event Action OnGameStarted;
        public static event Action OnStatedPreporations;
        public static event Action OnGameOverCutScene;
        public static event Action OnBossFightPreporations;
        public static event Action OnLevelCompleted;


        public static void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Preporations:
                    OnStatedPreporations?.Invoke();
                    break;
                case GameState.StartGame:
                    OnGameStarted?.Invoke();
                    break;
                case GameState.StartGameOverCatScene:
                    OnGameOverCutScene?.Invoke();
                    break;
                case GameState.GameOver:
                    OnGameOver?.Invoke();
                    break;
                case GameState.BossFight:
                    OnBossFightPreporations?.Invoke();
                    break;
                case GameState.LevelCompleted:
                    OnLevelCompleted?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}

