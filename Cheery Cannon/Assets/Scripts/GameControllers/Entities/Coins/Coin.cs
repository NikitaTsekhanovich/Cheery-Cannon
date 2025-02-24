using System;
using System.Collections;
using DG.Tweening;
using GameControllers.Entities.Coins.Properties;
using GameControllers.GameSystems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameControllers.Entities.Coins
{
    public class Coin : Entity<Coin>, ICanGiveCoin
    {
        [SerializeField] private AudioSource _getCoinSound;
        [SerializeField] private ConfigCoin _configCoin;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        private const float MinSpawnVariance = -1f;
        private const float MaxSpawnVariance = 1f;
        private const float DelayDestroy = 1f;
        private int _currentValue;

        public static Func<Vector3> OnGetPlayerPosition;
        
        public override void SpawnInit(Action<Entity<Coin>> returnAction)
        {
            LevelProgressController.OnWin += ReachPlayer;
            base.SpawnInit(returnAction);
        }
        
        private void OnDestroy()
        {
            LevelProgressController.OnWin -= ReachPlayer;
        }

        public int GiveCoin()
        {
            _getCoinSound.Play();
            DoDestroy();
            return _currentValue;
        }

        public override void AwakeInit(Transform startPosition)
        {
            _spriteRenderer.enabled = true;
            _collider.enabled = true;
            var randomX = Random.Range(MinSpawnVariance, MaxSpawnVariance);
            var randomY = Random.Range(MinSpawnVariance, MaxSpawnVariance);
            var randomRotate = Random.Range(0f, 360f);
            
            transform.rotation = Quaternion.Euler(0f, 0f, randomRotate);
            _rigidbody.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
            base.AwakeInit(startPosition);
        }

        public void SetCoinValue(int extraValue)
        {
            _currentValue = _configCoin.Price + extraValue;
        }

        private void DoDestroy()
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;

            StartCoroutine(WaitDelayDestroy());
        }
        
        private IEnumerator WaitDelayDestroy()
        {
            yield return new WaitForSeconds(DelayDestroy);
            ReturnToPool();
        }

        private void ReachPlayer()
        {
            DOTween.Sequence()
                .Append(transform.DOMove(OnGetPlayerPosition.Invoke(), GameStateController.DelayWin));
        }
    }
}
