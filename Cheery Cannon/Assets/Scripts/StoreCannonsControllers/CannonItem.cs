using System;
using TMPro;
using UnityEngine;

namespace StoreCannonsControllers
{
    public class CannonItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private GameObject _chosenBlock;
        [SerializeField] private GameObject _chooseBlock;
        [SerializeField] private GameObject _lockBlock;
        [SerializeField] private ConfigCannon _configCannon;
        [SerializeField] private AudioSource _purchaseSound;
        private Func<int, bool> _canBuyCannon;
        private Action<int> _chooseItem;

        public void Init(Func<int, bool> canBuyCannon, Action<int> chooseItem)
        {
            _canBuyCannon = canBuyCannon;
            _chooseItem = chooseItem;
            _priceText.text = $"{_configCannon.Price}";
            CheckStateItem();
        }
        
        public void CheckStateItem()
        {
            _lockBlock.SetActive(false);
            _chooseBlock.SetActive(false);
            _chosenBlock.SetActive(false);
            
            if (_configCannon.TypeState == TypesStateCannon.Buy)
            {
                _lockBlock.SetActive(true);
            }
            else if (_configCannon.TypeState == TypesStateCannon.Select)
            {
                _chooseBlock.SetActive(true);
            }
            else if (_configCannon.TypeState == TypesStateCannon.Selected)
            {
                _chosenBlock.SetActive(true);
            }
        }

        public void ClickChooseCannon()
        {
            if (_configCannon.TypeState == TypesStateCannon.Buy && _canBuyCannon.Invoke(_configCannon.Price)
                || _configCannon.TypeState == TypesStateCannon.Select)
            {
                if (_configCannon.TypeState == TypesStateCannon.Buy)
                {
                    _purchaseSound.Play();
                }
                
                PlayerPrefs.SetInt($"{CannonsDataKeys.StateCannonKey}" +
                                   $"{PlayerPrefs.GetInt(CannonsDataKeys.IndexChosenCannonKey)}", 
                    (int)TypesStateCannon.Select);
                
                PlayerPrefs.SetInt(CannonsDataKeys.IndexChosenCannonKey, _configCannon.Index);
                PlayerPrefs.SetInt($"{CannonsDataKeys.StateCannonKey}{_configCannon.Index}", 
                    (int)TypesStateCannon.Selected);
                
                _chooseItem.Invoke(_configCannon.Index);
            }
        }
    }
}
