using System;
using System.Collections.Generic;
using GameControllers.GameSystems;
using GameControllers.Launchers.SystemsProperties;
using GameControllers.PlayerControllers;
using UnityEngine;

namespace GameControllers.Launchers
{
    public class GameSystemsHandler : MonoBehaviour
    {
        private readonly List<IHaveUpdate> _updatesSystems = new ();
        private readonly List<IDisposable> _disposablesSystems = new ();
        private bool _isStopGame;

        private void OnEnable()
        {
            PlayerCollisionHandler.OnLose += StopGame;
            LevelProgressController.OnWin += StopGame;
        }

        private void OnDisable()
        {
            PlayerCollisionHandler.OnLose -= StopGame;
            LevelProgressController.OnWin -= StopGame;
        }
        
        public void RegistrationDisposer(IDisposable disposable) => _disposablesSystems.Add(disposable);
        public void RegistrationUpdater(IHaveUpdate updateSystem) => _updatesSystems.Add(updateSystem);

        private void StopGame()
        {
            _isStopGame = true;
        }

        private void Update()
        {
            if (_isStopGame) return;
            
            foreach (var updateSystem in _updatesSystems)
                updateSystem.Update();
        }

        private void OnDestroy()
        {
            foreach (var disposeSystem in _disposablesSystems)
                disposeSystem.Dispose();
        }
    }
}
