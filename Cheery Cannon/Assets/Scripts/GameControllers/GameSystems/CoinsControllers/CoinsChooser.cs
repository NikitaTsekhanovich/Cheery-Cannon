using System;
using System.Collections.Generic;
using GameControllers.Entities.Balls;
using GameControllers.Entities.Coins;
using GameControllers.Factories.Properties;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameControllers.GameSystems.CoinsControllers
{
    public class CoinsChooser : IDisposable
    {
        private readonly List<ICanGetEntity<Coin>> _coinsFactories;
        private const int MinCoins = 1;
        private const int MaxCoins = 4;
        
        public CoinsChooser(List<ICanGetEntity<Coin>> coinsFactories)
        {
            _coinsFactories = coinsFactories;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            Ball.OnSpawnCoin += ChooseCoins;
        }

        private void ChooseCoins(Transform spawnPoint)
        {
            var numberCoins = Random.Range(MinCoins, MaxCoins);

            for (var i = 0; i < numberCoins; i++)
            {
                var randomIndexCoinFactory = Random.Range(0, _coinsFactories.Count);
                _coinsFactories[randomIndexCoinFactory].GetEntity(spawnPoint);
            }
        }

        public void Dispose()
        {
            Ball.OnSpawnCoin -= ChooseCoins;
        }
    }
}
