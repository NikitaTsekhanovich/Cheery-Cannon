using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelControllers
{
    public class LevelItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberLevelText;
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private Image _playButtonImage;
        [SerializeField] private Sprite _openLevelSprite;
        [SerializeField] private Sprite _closeLevelSprite;
        [SerializeField] private Sprite _completeLevelSprite;
        [SerializeField] private AudioSource _clickSound;
        private Action<int> _startLevelAction;
        private int _indexLevel;

        public void Init(int indexLevel, TypeStateLevel stateLevel, Action<int> startLevel)
        {
            _startLevelAction = startLevel;
            _indexLevel = indexLevel;
            _numberLevelText.text = $"Level {_indexLevel + 1}";

            CheckStateLevel(stateLevel);
        }

        private void CheckStateLevel(TypeStateLevel stateLevel)
        {
            if (stateLevel == TypeStateLevel.IsClosed)
            {
                _startLevelButton.interactable = false;
                _playButtonImage.sprite = _closeLevelSprite;
            }
            else if (stateLevel == TypeStateLevel.IsOpen)
            {
                _startLevelButton.interactable = true;
                _playButtonImage.sprite = _openLevelSprite;
            }
            else if (stateLevel == TypeStateLevel.IsCompleted)
            {
                _startLevelButton.interactable = true;
                _playButtonImage.sprite = _completeLevelSprite;
            }
        }

        public void ClickStartLevel()
        {
            _clickSound.Play();
            _startLevelAction.Invoke(_indexLevel);
        }
    }
}
