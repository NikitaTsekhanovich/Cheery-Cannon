using System.Collections.Generic;
using GameControllers.PlayerControllers;
using LevelControllers;
using StoreBoostControllers;

namespace SceneControllers
{
    public class LevelLaunchData
    {
        public ConfigLevel ConfigLevel { get; private set; }
        public Dictionary<TypesBoosts, ConfigBoost> BoostsConfigsDictionary { get; private set; }
        public Cannon Cannon { get; private set; }
        
        public void SetConfigLevel(ConfigLevel configLevel) 
            => ConfigLevel = configLevel;
        public void SetBoostsConfigs(Dictionary<TypesBoosts, ConfigBoost> boostsConfigs) 
            => BoostsConfigsDictionary = boostsConfigs;
        public void SetCannon(Cannon cannon) 
            => Cannon = cannon;
    }
}
