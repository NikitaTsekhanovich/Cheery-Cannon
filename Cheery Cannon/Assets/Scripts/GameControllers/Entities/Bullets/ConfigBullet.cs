using UnityEngine;

namespace GameControllers.Entities.Bullets
{
    [CreateAssetMenu(fileName = "ConfigBullet", menuName = "Entities Configs/ConfigBullet")]
    public class ConfigBullet : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
