using System;
using GameControllers.PlayerControllers;
using LevelControllers;
using UnityEngine;

namespace GameControllers.GameSystems
{
    public class GameStateController : IDisposable
    {
        private readonly int _currentLevelIndex;
        
        public const float DelayWin = 1.5f;
        
        public GameStateController(int currentLevelIndex)
        {
            _currentLevelIndex = currentLevelIndex;
            SubscribeToActions();
        }

        private void Win()
        {
            PlayerPrefs.SetInt(
                $"{LevelsDataKeys.StateLevelKey}{_currentLevelIndex}",
                (int)TypeStateLevel.IsCompleted);

            var stateNextLevel = (TypeStateLevel)PlayerPrefs.GetInt(
                $"{LevelsDataKeys.StateLevelKey}{_currentLevelIndex + 1}");
            
            if (stateNextLevel != TypeStateLevel.IsCompleted)
                PlayerPrefs.SetInt(
                    $"{LevelsDataKeys.StateLevelKey}{_currentLevelIndex + 1}",
                    (int)TypeStateLevel.IsOpen);
        }

        private void Lose()
        {
            
        }
        
        private void SubscribeToActions()
        {
            LevelProgressController.OnWin += Win;
            PlayerCollisionHandler.OnLose += Lose;
        }

        public void Dispose()
        {
            LevelProgressController.OnWin -= Win;
            PlayerCollisionHandler.OnLose -= Lose;
        }
    }
}
