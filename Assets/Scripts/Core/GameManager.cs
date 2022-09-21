using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Main
{
    public enum GameState { Preporations, StartGame, StartGameOverCatScene, GameOver }

    public static class GameManager
    {
        private static event Action OnGameOver;
        private static event Action OnGameStarted;
        private static event Action OnStatedPreporations;
        private static event Action OnGameOverCutScene;


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
                default:
                    break;
            }
        }
    }
}

