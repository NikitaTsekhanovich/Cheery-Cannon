using GameControllers.GameSystems.WaveBalls;
using UnityEngine;

namespace LevelControllers
{
    [CreateAssetMenu(fileName = "ConfigLevel", menuName = "Config levels/ConfigLevel")]
    public class ConfigLevel : ScriptableObject
    {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public ConfigWaveBalls ConfigWaveBalls { get; private set; }

        public TypeStateLevel StateLevel => 
            (TypeStateLevel)PlayerPrefs.GetInt($"{LevelsDataKeys.StateLevelKey}{Index}");
    }
}
