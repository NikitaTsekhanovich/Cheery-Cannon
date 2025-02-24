using GameControllers.Entities.Bullets;
using GameControllers.Factories.Properties;
using GameControllers.Launchers.SystemsProperties;
using StoreBoostControllers;
using UnityEngine;

namespace GameControllers.PlayerControllers
{
    public class ShootingSystem : IHaveUpdate
    {
        private readonly Transform[] _shootPoints;
        private readonly ICanGetEntity<Bullet> _bulletFactory;
        private readonly float _attackSpeed;
        private readonly AudioSource _shootSound;
        private float _currentTime;
        private const float AttackSpeed = 1f;

        public ShootingSystem(
            Transform[] shootPoints, 
            ICanGetEntity<Bullet> bulletFactory,
            ConfigBoost configAttackSpeed,
            AudioSource shootSound)
        {
            _shootPoints = shootPoints;
            _bulletFactory = bulletFactory;
            _attackSpeed = configAttackSpeed.CurrentBoostValue;
            _shootSound = shootSound;
        }

        public void Update()
        {
            _currentTime += Time.deltaTime;
            
            if (_currentTime >= AttackSpeed - _attackSpeed)
            {
                _shootSound.Play();
                
                foreach (var shootPoint in _shootPoints)
                    _bulletFactory.GetEntity(shootPoint);

                _currentTime = 0f;
            }
        }
    }
}
