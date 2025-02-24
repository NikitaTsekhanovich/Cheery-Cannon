using System;
using TMPro;
using UnityEngine;

namespace StoreBoostControllers
{
    public class BoostItem : MonoBehaviour
    
    {
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private AudioSource _purchaseSound;
        [SerializeField] private ConfigBoost _configBoost;
        private Func<int, bool> _canBuyBoost;
        private int _currentLevelBoost;

        public void Init(Func<int, bool> canBuyBoost) 
        {
            _canBuyBoost = canBuyBoost;
            _currentLevelBoost = PlayerPrefs.GetInt(_configBoost.LevelBoostKey);
            UpdateText();
        }

        private void UpdateText()
        {
            _priceText.text = $"{_configBoost.PriceStart * _configBoost.LevelValue}";
            _currentLevelText.text = $"Lvl: {_configBoost.LevelValue}";
        }

        public void ClickBuyBoost()
        {
            if (_canBuyBoost.Invoke(_configBoost.PriceStart * _currentLevelBoost))
            {
                _purchaseSound.Play();
                _currentLevelBoost++;
                PlayerPrefs.SetInt(_configBoost.LevelBoostKey, _currentLevelBoost);
                UpdateText();
            }
        }
    }
}
