using PlayerData;
using TMPro;
using UnityEngine;

namespace Store
{
    public abstract class DisplayStoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        private int _currentCoins;

        public void ClickOpenStore()
        {
            LoadCoinsInfo();
            LoadItems();
        }

        private void LoadCoinsInfo()
        {
            _currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey);
            _coinsText.text = _currentCoins.ToString();
        }

        protected abstract void LoadItems();

        protected bool CanBuyItem(int price)
        {
            return _currentCoins - price >= 0 && BuyItem(price);
        }

        private bool BuyItem(int price)
        {
            _currentCoins -= price;
            PlayerPrefs.SetInt(PlayerDataKeys.CoinsKey, _currentCoins);
            _coinsText.text = _currentCoins.ToString();
            return true;
        }
    }
}
