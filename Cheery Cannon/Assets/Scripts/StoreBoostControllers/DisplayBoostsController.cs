using Store;
using UnityEngine;

namespace StoreBoostControllers
{
    public class DisplayBoostsController : DisplayStoreController
    {
        [SerializeField] private BoostItem[] _boostItems;
        
        protected override void LoadItems()
        {
            foreach (var boostItem in _boostItems)
                boostItem.Init(CanBuyItem);
        }
    }
}
