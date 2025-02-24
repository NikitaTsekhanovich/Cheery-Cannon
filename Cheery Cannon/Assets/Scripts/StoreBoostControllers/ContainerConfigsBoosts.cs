using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoreBoostControllers
{
    public static class ContainerConfigsBoosts
    {
        private static List<ConfigBoost> _boostsConfigs { get; set; }
        public static Dictionary<TypesBoosts, ConfigBoost> BoostsConfigsDictionary { get; private set; }

        public static void LoadBoostsConfigs()
        {
            _boostsConfigs = Resources.LoadAll<ConfigBoost>("Configs/BoostsConfigs")
                .OrderBy(x => (int)x.TypesBoosts)
                .ToList();
            
            BoostsConfigsDictionary = new Dictionary<TypesBoosts, ConfigBoost>();

            foreach (var boostConfig in _boostsConfigs)
                BoostsConfigsDictionary[boostConfig.TypesBoosts] = boostConfig;

            _boostsConfigs.Clear();
        }
    }
}
