using UnityEngine;

namespace StoreBoostControllers
{
    [CreateAssetMenu(fileName = "ConfigBoost", menuName = "Configs Boosts/ ConfigBoost")]
    public class ConfigBoost : ScriptableObject
    {
        [field: SerializeField] public TypesBoosts TypesBoosts { get; set; }
        [field: SerializeField] public int PriceStart { get; private set; }
        [field: SerializeField] public float BoostValue { get; private set; }
        public string LevelBoostKey => $"{BoostsDataKeys.LevelBoostKey}{(int)TypesBoosts}";
        public int LevelValue 
        {
            get
            {
                if (PlayerPrefs.GetInt(LevelBoostKey) == 0)
                {
                    PlayerPrefs.SetInt(LevelBoostKey, 1);
                    return 1;
                }

                return PlayerPrefs.GetInt(LevelBoostKey);
            }
        }
        public float CurrentBoostValue => BoostValue * (LevelValue - 1);
    }
}
