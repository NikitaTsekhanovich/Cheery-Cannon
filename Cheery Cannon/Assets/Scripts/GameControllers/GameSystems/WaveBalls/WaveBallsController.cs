using System.Collections.Generic;
using GameControllers.Entities.Balls;
using GameControllers.Factories.Properties;
using GameControllers.GameSystems.WaveBalls.Properties;
using GameControllers.Launchers.SystemsProperties;
using UnityEngine;

namespace GameControllers.GameSystems.WaveBalls
{
    public class WaveBallsController : IHaveUpdate, ICanChooseBall
    {
        private readonly Transform[] _spawnPoints;
        private readonly float _minSpawnDelay;
        private readonly float _maxSpawnDelay;
        private readonly Queue<ConfigBall> _ballQueue;
        private List<ICanGetEntity<Ball>> _ballsFactories;
        private float _spawnDelay;
        private float _currentTime;
        
        public WaveBallsController(
            Transform[] spawnPoints, 
            ConfigWaveBalls configWaveBalls)
        {
            _spawnPoints = spawnPoints;
            _minSpawnDelay = configWaveBalls.MinDurationSpawnBalls;
            _maxSpawnDelay = configWaveBalls.MaxDurationSpawnBalls;
            _ballQueue = new Queue<ConfigBall>(configWaveBalls.QueueBalls);
        }

        public void InitBallsFactories(List<ICanGetEntity<Ball>> ballsFactories)
        {
            _ballsFactories = ballsFactories;
        }

        public void Update()
        {
            StartWaveBalls();
        }

        private void StartWaveBalls()
        {
            if (_ballQueue.Count <= 0) return;
            
            _currentTime += Time.deltaTime;

            if (_currentTime >= _spawnDelay)
            {
                var levelBall = _ballQueue.Dequeue().LevelBall;
                
                _currentTime = 0f;
                var randomIndex = Random.Range(0, _spawnPoints.Length);
                var randomPoint = _spawnPoints[randomIndex];
                _ballsFactories[levelBall].GetEntity(randomPoint);

                _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay + 1);
            }
        }

        public void ChooseChildBalls(Transform pointSpawn, int levelBall)
        {
            var firstBall = (Ball)_ballsFactories[levelBall].GetEntity(pointSpawn);
            var secondBall = (Ball)_ballsFactories[levelBall].GetEntity(pointSpawn);
            
            firstBall.AwakeChildBall(true, pointSpawn);
            secondBall.AwakeChildBall(false, pointSpawn);
        }
    }
}
