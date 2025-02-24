using UnityEngine;

namespace GameControllers.Entities.Balls
{
    [CreateAssetMenu(fileName = "ConfigBall", menuName = "Entities Configs/ConfigBall")]
    public class ConfigBall : ScriptableObject
    {
        [field: Range(0, 3)]
        [field: SerializeField] public int LevelBall { get; private set; }
    }
}
