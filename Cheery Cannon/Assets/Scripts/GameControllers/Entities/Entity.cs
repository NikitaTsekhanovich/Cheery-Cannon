using System;
using GameControllers.Entities.Properties;
using UnityEngine;

namespace GameControllers.Entities
{
    public abstract class Entity<T> : MonoBehaviour, ICanInitialize<T>
    {
        private Action<Entity<T>> _returnAction;
        
        public virtual void SpawnInit(Action<Entity<T>> returnAction)
        {
            _returnAction = returnAction;
        }

        public virtual void AwakeInit(Transform startPosition)
        {
            transform.position = startPosition.position;
        }
        
        protected void ReturnToPool()
        {
            _returnAction.Invoke(this);
        }

        public void ChangeStateEntity(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
