using GameControllers.Entities;
using GameControllers.Entities.Coins;

namespace GameControllers.Factories.Types
{
    public class CoinFactory : Factory<Coin>
    {
        private readonly int _extraValue;
        
        public CoinFactory(Entity<Coin> entity, int extraValue) : base(entity)
        {
            _extraValue = extraValue;
            CreatePool();
        }
        
        protected override Entity<Coin> Preload()
        {
            var newCoinEntity = base.Preload();

            var newCoin = (Coin)newCoinEntity;
            newCoin.SetCoinValue(_extraValue);

            return newCoinEntity;
        }
    }
}
