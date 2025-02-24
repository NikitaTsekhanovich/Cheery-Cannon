using System;
using DG.Tweening;
using GameControllers.GameSystems;
using GameControllers.PlayerControllers;
using PlayerData;
using SceneControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameControllers.UIControllers
{
    public class MenuController : MonoBehaviour
    {
        [Header("Win screen elements")]
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private TMP_Text _nextLevelText;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TMP_Text _coinsWinText;
        [SerializeField] private TMP_Text _currentCoinsWinText;
        [SerializeField] private AudioSource _winSound;
        
        [Header("Lose screen elements")]
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private TMP_Text _coinsLoseText;
        [SerializeField] private TMP_Text _currentCoinsLoseText;
        [SerializeField] private AudioSource _loseSound;
        
        private Color _basicColorText;
        private CoinsController _coinsController;

        public static Func<bool> OnLoadNextLevel;

        [Inject]
        private void Construct(CoinsController coinsController)
        {
            _coinsController = coinsController;
        }

        private void Start()
        {
            _basicColorText = new Color(0.2901961f, 0.1137255f, 0.3568628f);
        }

        private void OnEnable()
        {
            LevelProgressController.OnWin += ShowWinScreen;
            PlayerCollisionHandler.OnLose += ShowLoseScreen;
        }

        private void OnDisable()
        {
            LevelProgressController.OnWin -= ShowWinScreen;
            PlayerCollisionHandler.OnLose -= ShowLoseScreen;
        }

        public void ClickRestartGame()
        {
            SceneSwitch.Instance.ChangeScene("Game");
        }

        public void ClickNextLevel()
        {
            var canLoad = OnLoadNextLevel.Invoke();
            
            if (canLoad)
                SceneSwitch.Instance.ChangeScene("Game");
            else
            {
                _nextLevelText.enabled = true;
                _nextLevelButton.interactable = false;
            }
        }

        public void ClickBackToMenu()
        {
            SceneSwitch.Instance.ChangeScene("MainMenu");
        }

        private void ShowWinScreen()
        {
            DOTween.Sequence()
                .AppendInterval(GameStateController.DelayWin + 0.3f)
                .AppendCallback(() =>
                {
                    _winSound.Play();
                    AnimationIncreaseCoins(_coinsWinText, _currentCoinsWinText);
                    _winScreen.SetActive(true);
                });
        }

        private void ShowLoseScreen()
        {
            _loseSound.Play();
            AnimationIncreaseCoins(_coinsLoseText, _currentCoinsLoseText);
            _loseScreen.SetActive(true);
        }

        private void AnimationIncreaseCoins(TMP_Text coinsText, TMP_Text currentCoinsText)
        {
            var currentCoins = _coinsController.CurrentCoins;
            var previousCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsKey) - currentCoins;
            coinsText.text = previousCoins.ToString();

            if (currentCoins == 0)
            {
                currentCoinsText.enabled = false;
                return;
            }
            
            currentCoinsText.text = currentCoins.ToString();

            DOTween.Sequence()
                .Append(currentCoinsText.transform.DOMove(coinsText.transform.position, 1f))
                .AppendCallback(() =>
                {
                    currentCoinsText.enabled = false;
                    coinsText.text = (previousCoins + currentCoins).ToString();
                })
                .Append(coinsText.DOColor(Color.green, 0.5f))
                .Append(coinsText.DOColor(_basicColorText, 0.5f));
        }
    }
}
