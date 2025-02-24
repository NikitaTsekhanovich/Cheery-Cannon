using System;
using GameControllers.PlayerControllers;
using GameControllers.UIControllers;
using PlayerData;
using StoreBoostControllers;
using UnityEngine;

namespace GameControllers.GameSystems
{
    public class CoinsController : IDisposable
    {
        private readonly DisplayCoinsUpdater _displayCoinsUpdater;
        private readonly int _amountCoins;
        private readonly int _coinValueMultiplier;

        public int CurrentCoins { get; private set; }

        public CoinsController(
            DisplayCoinsUpdater displayCoinsUpdater)
        {
            _displayCoinsUpdater = displayCoinsUpdater;
            SubscribeToActions();
            _amountCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey);
        }

        private void GetCoin(int coinsCount)
        {
            CurrentCoins += coinsCount;
            PlayerPrefs.SetInt(PlayerDataKeys.CoinsKey, _amountCoins + CurrentCoins);
            _displayCoinsUpdater.UpdateCoinsText(CurrentCoins);
        }

        private void SubscribeToActions()
        {
            PlayerCollisionHandler.OnGetCoin += GetCoin;
        }

        public void Dispose()
        {
            PlayerCollisionHandler.OnGetCoin -= GetCoin;
        }
    }
}
