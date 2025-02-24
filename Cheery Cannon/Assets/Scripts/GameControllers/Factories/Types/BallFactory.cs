using GameControllers.Entities;
using GameControllers.Entities.Balls;
using Zenject;

namespace GameControllers.Factories.Types
{
    public class BallFactory : Factory<Ball>
    {
        private readonly DiContainer _container;
        private readonly int _minLivesBalls;
        private readonly int _maxLivesBalls;
        
        public BallFactory(
            DiContainer container, 
            Entity<Ball> entity,
            int minLivesBalls,
            int maxLivesBalls) : base(entity)
        {
            _container = container;
            _minLivesBalls = minLivesBalls;
            _maxLivesBalls = maxLivesBalls;
            CreatePool();
        }

        protected override Entity<Ball> Preload()
        {
            var newEntityBall = base.Preload();
            _container.Inject(newEntityBall);
            
            var newBall = (Ball)newEntityBall;
            newBall.SetLives(_minLivesBalls, _maxLivesBalls);
            
            return newEntityBall;
        }
    }
}
