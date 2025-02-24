using GameControllers.Entities.Balls.Properties;
using UnityEngine;

namespace GameControllers.Entities.Bullets
{
    public class Bullet : Entity<Bullet>
    {
        [SerializeField] private ConfigBullet _configBullet;
        [SerializeField] private Rigidbody2D _rigidbody;
        private int _extraDamage;
        private bool _readyToDestroy;

        public void SetDamage(int damage)
        {
            _extraDamage = _configBullet.Damage + damage;
        }
        
        public override void AwakeInit(Transform startPosition)
        {
            _readyToDestroy = false;
            base.AwakeInit(startPosition);
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            _rigidbody.velocity = Vector2.up * _configBullet.Speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckDealDamage(other);
        }

        private void CheckDealDamage(Collider2D other)
        {
            var takeableDamageObject = other.GetComponentInParent<ICanTakeDamage>();
            
            if (takeableDamageObject != null && !_readyToDestroy)
            {
                _readyToDestroy = true;
                DoDestroy();
                takeableDamageObject.TakeDamage(_extraDamage);
            }
        }

        public void DoDestroy()
        {
            ReturnToPool();
        }
    }
}
