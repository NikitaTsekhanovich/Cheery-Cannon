using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneControllers
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster _loadingScreenBlockClick;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField] private Image _logo;
        private Coroutine _loadingTextAnimation;
        private const float DelayAnimation = 0.7f;

        public static LoadingScreenController Instance;
        
        private void OnEnable()
        {
            SceneDataLoader.OnInitGlobalControllers += Init;
        }

        private void OnDisable()
        {
            SceneDataLoader.OnInitGlobalControllers -= Init;
        }

        private void Init()
        {
            Instance = this;
        }

        public void StartAnimationFade(Action loadScene)
        {
            _loadingScreenBlockClick.enabled = true;
            _loadingTextAnimation = StartCoroutine(StartLoadingTextAnimation());
            _loadingText.DOFade(1f, DelayAnimation);
            _logo.DOFade(1f, DelayAnimation);

            DOTween.Sequence()
                .Append(_background.DOFade(1f, DelayAnimation))
                .AppendInterval(1.5f)
                .AppendCallback(loadScene.Invoke)
                .AppendInterval(0.3f)
                .OnComplete(EndAnimationFade);
        }

        private void EndAnimationFade()
        {
            _logo.DOFade(0f, DelayAnimation);
            _loadingText.DOFade(0f, DelayAnimation);

            DOTween.Sequence()
                .Append(_background.DOFade(0f, DelayAnimation))
                .AppendCallback(() => StopCoroutine(_loadingTextAnimation))
                .AppendCallback(() => _loadingScreenBlockClick.enabled = false);
        }

        private IEnumerator StartLoadingTextAnimation()
        {
            while (true)
            {
                _loadingText.text = "Loading.";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = "Loading..";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = "Loading...";
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
