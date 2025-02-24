using GameControllers.PlayerControllers;
using UnityEngine;

namespace StoreCannonsControllers
{
    [CreateAssetMenu(fileName = "ConfigCannon", menuName = "Config Cannons/ ConfigCannon")]
    public class ConfigCannon : ScriptableObject
    {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Cannon CannonPrefab { get; private set; }

        public TypesStateCannon TypeState =>
            PlayerPrefs.GetInt(CannonsDataKeys.IndexChosenCannonKey) == Index
                ? TypesStateCannon.Selected
                : (TypesStateCannon) PlayerPrefs.GetInt($"{CannonsDataKeys.StateCannonKey}{Index}");
    }
}
