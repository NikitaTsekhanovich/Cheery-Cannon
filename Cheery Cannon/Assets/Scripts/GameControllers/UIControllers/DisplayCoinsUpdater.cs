using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GameControllers.UIControllers
{
    public class DisplayCoinsUpdater : IDisposable
    {
        private readonly TMP_Text _coinsText;
        private readonly Color _basicColorText;

        public DisplayCoinsUpdater(TMP_Text coinsText)
        {
            _coinsText = coinsText;
            _basicColorText = new Color(0.2901961f, 0.1137255f, 0.3568628f);
        }

        public void UpdateCoinsText(int coins)
        {
            _coinsText.text = coins.ToString();

            DOTween.Sequence()
                .Append(_coinsText.DOColor(Color.green, 0.5f))
                .Append(_coinsText.DOColor(_basicColorText, 0.5f));
        }

        public void Dispose()
        {
            DOTween.Kill(this);
        }
    }
}
