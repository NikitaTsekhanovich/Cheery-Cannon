using UnityEngine;

namespace GameControllers.Entities.Coins
{
    [CreateAssetMenu(fileName = "ConfigCoin", menuName = "Entities Configs/ConfigCoin")]
    public class ConfigCoin : ScriptableObject
    {
        [field: SerializeField] public int Price { get; private set; }
    }
}
