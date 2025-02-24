using Store;
using UnityEngine;

namespace StoreCannonsControllers
{
    public class DisplayCannonsController : DisplayStoreController
    {
        [SerializeField] private CannonItem[] _cannonItems;
        private CannonItem _currentCannonItem;
        
        protected override void LoadItems()
        {
            _currentCannonItem = _cannonItems[PlayerPrefs.GetInt(CannonsDataKeys.IndexChosenCannonKey)];
            
            foreach (var cannonItem in _cannonItems)
                cannonItem.Init(CanBuyItem, ChooseCannon);
        }

        private void ChooseCannon(int index)
        {
            _currentCannonItem.CheckStateItem();
            _currentCannonItem = _cannonItems[index];
            _currentCannonItem.CheckStateItem();
        }
    }
}
