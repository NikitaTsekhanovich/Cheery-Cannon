using System;
using GridExtension;
using SceneControllers;
using UnityEngine;

namespace LevelControllers
{
    public class DisplayLevelsController : MonoBehaviour
    {
        [SerializeField] private Transform _levelsParent;
        [SerializeField] private LevelItem _levelItemPrefab;
        [SerializeField] private FlexibleGridLayout _levelsGrid;
        [SerializeField] private RectTransform _levelsContent;
        private bool _isLoaded;
        private const float WidthLevelItem = 310f;

        public static Action<ConfigLevel> OnStashConfigLevel;

        private void OnEnable()
        {
            SceneDataLoader.OnLoadDisplayLevels += LoadLevelsItems;
        }

        private void OnDisable()
        {
            SceneDataLoader.OnLoadDisplayLevels -= LoadLevelsItems;
        }

        private void LoadLevelsItems()
        {
            if (_isLoaded) return;
            _isLoaded = true;
            
            LoadGrid();

            foreach (var levelConfig in ContainerLevels.LevelsConfigs)
            {
                var levelItem = Instantiate(_levelItemPrefab, _levelsParent);
                levelItem.Init(levelConfig.Index, levelConfig.StateLevel, StartLevel);
            }
        }

        private void LoadGrid()
        {
            _levelsGrid.rows = ContainerLevels.LevelsConfigs.Count;
            _levelsContent.sizeDelta = new Vector2(
                _levelsContent.sizeDelta.x, 
                ContainerLevels.LevelsConfigs.Count * WidthLevelItem);
        }

        private void StartLevel(int levelIndex)
        {
            OnStashConfigLevel.Invoke(ContainerLevels.LevelsConfigs[levelIndex]);
            SceneSwitch.Instance.ChangeScene("Game");
        }
    }
}
