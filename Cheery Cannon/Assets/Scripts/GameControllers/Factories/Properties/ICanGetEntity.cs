using GameControllers.Entities;
using GameControllers.Entities.Properties;
using UnityEngine;

namespace GameControllers.Factories.Properties
{
    public interface ICanGetEntity<T>
        where T : ICanInitialize<T>
    {
        public Entity<T> GetEntity(Transform spawnPoint);
    }
}
