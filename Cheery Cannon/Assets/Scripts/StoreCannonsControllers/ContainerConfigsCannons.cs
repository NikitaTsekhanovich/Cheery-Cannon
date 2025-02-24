using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoreCannonsControllers
{
    public static class ContainerConfigsCannons
    {
        public static List<ConfigCannon> CannonsConfigs { get; private set; }

        public static void LoadCannonsConfigs()
        {
            CannonsConfigs = Resources.LoadAll<ConfigCannon>("Configs/CannonsConfigs")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}
