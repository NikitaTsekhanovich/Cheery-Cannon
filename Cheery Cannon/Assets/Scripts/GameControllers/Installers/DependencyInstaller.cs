using System.Collections.Generic;
using GameControllers.Entities.Balls;
using GameControllers.Entities.Coins;
using GameControllers.Factories.Properties;
using GameControllers.Factories.Types;
using GameControllers.GameSystems;
using GameControllers.GameSystems.CoinsControllers;
using GameControllers.GameSystems.WaveBalls;
using GameControllers.GameSystems.WaveBalls.Properties;
using GameControllers.Launchers;
using GameControllers.PlayerControllers;
using GameControllers.UIControllers;
using SceneControllers;
using StoreBoostControllers;
using UnityEngine;
using Zenject;

namespace GameControllers.Installers
{
    public class DependencyInstaller : MonoInstaller
    {
        [Header("Game Systems Handler")]
        [SerializeField] private GameSystemsHandler _gameSystemsHandler;
        [Header("Game object info")]
        [SerializeField] private DataLaunch _dataLaunch;
        private LevelLaunchData _levelLaunchData;
        
        [Inject]
        private void Construct(LevelLaunchData levelLaunchData)
        {
            _levelLaunchData = levelLaunchData;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            CreateInputControllers();
            CreateGameStateControllers();
            CreateCoinsControllers();
            CreateShootControllers();
            CreateBallsControllers();
            CreateLevelProgressControllers();
        }

        private void Awake()
        {
            Container.Bind<MenuController>().FromComponentInHierarchy().AsSingle();
            LoadLevelInfo();
        }

        private void CreateInputControllers()
        {
            var inputController = new InputController(_dataLaunch.PlayerMovement);
            _gameSystemsHandler.RegistrationUpdater(inputController);
        }
        
        private void CreateGameStateControllers()
        {
            var gameStateController = new GameStateController(_levelLaunchData.ConfigLevel.Index);
            _gameSystemsHandler.RegistrationDisposer(gameStateController);
        }

        private void CreateShootControllers()
        {
            var newCannon = CreateCannon(_levelLaunchData.Cannon);

            var bulletFactory = new BulletFactory(
                _dataLaunch.BulletPrefab, 
                (int)_levelLaunchData.BoostsConfigsDictionary[TypesBoosts.Damage].CurrentBoostValue);
            
            var shootingSystem = new ShootingSystem(
                newCannon.ShootPoints, 
                bulletFactory,
                _levelLaunchData.BoostsConfigsDictionary[TypesBoosts.AttackSpeed],
                _dataLaunch.ShootSound);
            
            _gameSystemsHandler.RegistrationUpdater(shootingSystem);
        }
        
        private Cannon CreateCannon(Cannon cannonPrefab)
        {
            var newCannon = Instantiate(cannonPrefab, _dataLaunch.Player.transform, true);
            newCannon.transform.localPosition = Vector3.zero;

            return newCannon;
        }

        private void CreateCoinsControllers()
        {
            var coinsFactories = new List<ICanGetEntity<Coin>>();
            
            foreach (var coinPrefab in _dataLaunch.CoinPrefabs)
                coinsFactories.Add(
                    new CoinFactory(coinPrefab, 
                    (int)_levelLaunchData.BoostsConfigsDictionary[TypesBoosts.IncreaseCoins].CurrentBoostValue));
            
            var coinsChooser = new CoinsChooser(coinsFactories);
            
            var displayCoinsUpdater = new DisplayCoinsUpdater(_dataLaunch.CoinsText);
            var coinsController = new CoinsController(
                displayCoinsUpdater);

            Container.Bind<CoinsController>()
                .FromInstance(coinsController)
                .AsSingle();
            
            _gameSystemsHandler.RegistrationDisposer(coinsChooser);
            _gameSystemsHandler.RegistrationDisposer(displayCoinsUpdater);
            _gameSystemsHandler.RegistrationDisposer(coinsController);
        }
        
        private void CreateBallsControllers()
        {
            var waveBallsController = new WaveBallsController(
                _dataLaunch.BallSpawnPoints, 
                _levelLaunchData.ConfigLevel.ConfigWaveBalls);
            
            Container.Bind<ICanChooseBall>()
                .FromInstance(waveBallsController)
                .AsSingle();
            
            var ballFactories = new List<ICanGetEntity<Ball>>();
            
            foreach (var ballPrefab in _dataLaunch.BallsPrefabs)
            {
                var ballFactory = new BallFactory(
                    Container, 
                    ballPrefab, 
                    _levelLaunchData.ConfigLevel.ConfigWaveBalls.MinLivesBalls,
                    _levelLaunchData.ConfigLevel.ConfigWaveBalls.MaxLivesBalls);
                ballFactories.Add(ballFactory);
            }
            waveBallsController.InitBallsFactories(ballFactories);
            
            _gameSystemsHandler.RegistrationUpdater(waveBallsController);
        }
        
        private void CreateLevelProgressControllers()
        {
            var displayLevelProgressUpdater = new DisplayLevelProgressUpdater(_dataLaunch.ProgressLevelBar);
            var levelProgressController = new LevelProgressController(
                _levelLaunchData.ConfigLevel.ConfigWaveBalls.QueueBalls, 
                displayLevelProgressUpdater);
            
            _gameSystemsHandler.RegistrationDisposer(levelProgressController);
            _gameSystemsHandler.RegistrationUpdater(displayLevelProgressUpdater);
        }
        
        private void LoadLevelInfo()
        {
            _dataLaunch.CurrentLevelText.text = $"Level {_levelLaunchData.ConfigLevel.Index + 1}";
        }
    }
}
