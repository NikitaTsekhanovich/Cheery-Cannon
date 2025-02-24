using System.Collections.Generic;
using GameControllers.Entities.Balls;
using UnityEngine;

namespace GameControllers.GameSystems.WaveBalls
{
    [CreateAssetMenu(fileName = "ConfigWaveBalls", menuName = "Config Spawners/ConfigWaveBalls")]
    public class ConfigWaveBalls : ScriptableObject
    {
        [field: SerializeField] public float MinDurationSpawnBalls { get; private set; }
        [field: SerializeField] public float MaxDurationSpawnBalls { get; private set; }
        [field: SerializeField] public int MinLivesBalls { get; private set; }
        [field: SerializeField] public int MaxLivesBalls { get; private set; }
        [field: SerializeField] public List<ConfigBall> QueueBalls { get; private set; }
    }
}
