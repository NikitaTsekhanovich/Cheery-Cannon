using GameControllers.Launchers.SystemsProperties;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.UIControllers
{
    public class DisplayLevelProgressUpdater : IHaveUpdate
    {
        private readonly Image _progressBar;
        private float _currentProgress;

        public DisplayLevelProgressUpdater(Image progressBar)
        {
            _progressBar = progressBar;
        }

        public void UpdateProgress(float progress)
        {
            _currentProgress = progress;
            
            if (_currentProgress >= 1f)
                _progressBar.fillAmount = progress;
        }

        public void Update()
        {
            _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, _currentProgress, Time.deltaTime);
        }
    }
}
