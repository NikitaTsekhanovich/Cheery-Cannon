using System;
using GameControllers.UIControllers;
using LevelControllers;
using StoreBoostControllers;
using StoreCannonsControllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SceneControllers
{
    public class SceneDataLoader : MonoBehaviour
    {
        private LevelLaunchData _levelLaunchData;

        public static Action OnInitGlobalControllers;
        public static Action OnLoadDisplayLevels;

        [Inject]
        private void Construct(LevelLaunchData levelLaunchData)
        {
            _levelLaunchData = levelLaunchData;
        }
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DisplayLevelsController.OnStashConfigLevel += PreLoadLevel;
            MenuController.OnLoadNextLevel += LoadNextLevel;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            DisplayLevelsController.OnStashConfigLevel -= PreLoadLevel;
            MenuController.OnLoadNextLevel -= LoadNextLevel;
        }

        private void PreLoadLevel(ConfigLevel configLevel)
        {
            _levelLaunchData.SetConfigLevel(configLevel);
            _levelLaunchData.SetBoostsConfigs(ContainerConfigsBoosts.BoostsConfigsDictionary);
            _levelLaunchData.SetCannon(ContainerConfigsCannons.CannonsConfigs[
                PlayerPrefs.GetInt(CannonsDataKeys.IndexChosenCannonKey)].CannonPrefab);
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "StartUp")
            {
                ContainerLevels.LoadLevelsConfigs();
                ContainerConfigsBoosts.LoadBoostsConfigs();
                ContainerConfigsCannons.LoadCannonsConfigs();
                OnInitGlobalControllers.Invoke();
                CheckFirstLaunch();
                SceneSwitch.Instance.ChangeScene("MainMenu");
            }
            else if (scene.name == "MainMenu")
            {
                OnLoadDisplayLevels.Invoke();
            }
            else if (scene.name == "Game")
            {
                
            }
        }

        private bool LoadNextLevel()
        {
            if (ContainerLevels.LevelsConfigs.Count > _levelLaunchData.ConfigLevel.Index + 1)
            {
                _levelLaunchData.SetConfigLevel(ContainerLevels.LevelsConfigs[_levelLaunchData.ConfigLevel.Index + 1]);
                return true;
            }

            return false;
        }

        private void CheckFirstLaunch()
        {
            if (PlayerPrefs.GetInt(LaunchDataKeys.TypeLaunchDataKey) == (int)TypeLaunch.IsFirstLaunch)
            {
                PlayerPrefs.SetInt($"{LevelsDataKeys.StateLevelKey}{0}", 
                    (int)TypeStateLevel.IsOpen);
                PlayerPrefs.SetInt($"{CannonsDataKeys.StateCannonKey}{0}", (int)TypesStateCannon.Select);
                PlayerPrefs.SetInt(CannonsDataKeys.IndexChosenCannonKey, 0);
                
                PlayerPrefs.SetInt(LaunchDataKeys.TypeLaunchDataKey, (int)TypeLaunch.IsSecondLaunch);
            }
            
        }
    }
}
