using GameControllers.Entities;
using GameControllers.Entities.Properties;
using GameControllers.Factories.Properties;
using GameControllers.PoolObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameControllers.Factories
{
    public abstract class Factory<T> : ICanGetEntity<T>
        where T : ICanInitialize<T>
    {
        private readonly Entity<T> _entity;
        private const int EntityPreloadCount = 10;
        private PoolBase<Entity<T>> _entitiesPool;

        protected Factory(Entity<T> entity)
        {
            _entity = entity;
        }

        protected void CreatePool()
        {
            _entitiesPool = new PoolBase<Entity<T>>(Preload, GetEntityAction, ReturnEntityAction, EntityPreloadCount);
        }

        public Entity<T> GetEntity(Transform spawnPoint)
        {
            var newEntity = _entitiesPool.Get();
            newEntity.AwakeInit(spawnPoint);

            return newEntity;
        }

        private void ReturnEntity(Entity<T> entity) => _entitiesPool.Return(entity);

        protected virtual Entity<T> Preload()
        {
            var newEntity = Object.Instantiate(_entity, new Vector3(0, 20, 0), Quaternion.identity)
                .GetComponent<Entity<T>>();
            newEntity.SpawnInit(ReturnEntity);
            
            return newEntity;
        }

        private void GetEntityAction(Entity<T> entity) => entity.ChangeStateEntity(true);
        
        private void ReturnEntityAction(Entity<T> entity) => entity.ChangeStateEntity(false);
    }
}
