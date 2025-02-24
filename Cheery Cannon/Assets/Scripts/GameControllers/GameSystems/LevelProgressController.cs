using System;
using System.Collections.Generic;
using GameControllers.Entities.Balls;
using GameControllers.UIControllers;
using UnityEngine;

namespace GameControllers.GameSystems
{
    public class LevelProgressController : IDisposable
    {
        private readonly DisplayLevelProgressUpdater _displayLevelProgressUpdater;
        private float _amountBalls;
        private float _currentAmountBalls;
        private double _amountChildBalls;

        public static Action OnWin;
        
        public LevelProgressController(
            List<ConfigBall> configsBalls, 
            DisplayLevelProgressUpdater displayLevelProgressUpdater)
        {
            _displayLevelProgressUpdater = displayLevelProgressUpdater;
            CheckBalls(configsBalls);
            SubscribeToActions();
        }

        private void CheckBalls(List<ConfigBall> configsBalls)
        {
            foreach (var configBall in configsBalls)
            {
                _amountChildBalls = 0;
                CountAmountBalls(configBall.LevelBall);
                _amountBalls += (float)_amountChildBalls;
            }
            
            _currentAmountBalls = _amountBalls;
        }

        private void CountAmountBalls(int levelBall)
        {
            if (levelBall >= 0)
            {
                _amountChildBalls += Math.Pow(2, levelBall);
                CountAmountBalls(levelBall - 1);
            }
        }

        private void CalculateProgress()
        {
            _currentAmountBalls--;
            var progress = (_amountBalls - _currentAmountBalls) / _amountBalls;
            _displayLevelProgressUpdater.UpdateProgress(progress);
            
            if (_currentAmountBalls <= 0)
                OnWin?.Invoke();
        }
        
        private void SubscribeToActions()
        {
            Ball.OnDestroyBall += CalculateProgress;
        }

        public void Dispose()
        {
            Ball.OnDestroyBall -= CalculateProgress;
        }
    }
}
