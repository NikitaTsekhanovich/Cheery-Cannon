using GameControllers.Entities;
using GameControllers.Entities.Bullets;

namespace GameControllers.Factories.Types
{
    public class BulletFactory : Factory<Bullet>
    {
        private readonly int _damage;
        
        public BulletFactory(Entity<Bullet> entity, int damege) : base(entity)
        {
            _damage = damege;
            CreatePool();
        }

        protected override Entity<Bullet> Preload()
        {
            var newBulletEntity = base.Preload();

            var newBullet = (Bullet)newBulletEntity;
            newBullet.SetDamage(_damage);

            return newBulletEntity;
        }
    }
}
