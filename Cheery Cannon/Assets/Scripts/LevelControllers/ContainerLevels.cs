using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelControllers
{
    public static class ContainerLevels
    {
        public static List<ConfigLevel> LevelsConfigs { get; private set; }

        public static void LoadLevelsConfigs()
        {
            LevelsConfigs = Resources.LoadAll<ConfigLevel>("Configs/LevelConfigs")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}
