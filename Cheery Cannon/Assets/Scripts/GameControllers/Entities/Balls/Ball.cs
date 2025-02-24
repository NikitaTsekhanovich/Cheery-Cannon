using System;
using System.Collections;
using DG.Tweening;
using GameControllers.Entities.Balls.Properties;
using GameControllers.GameSystems.WaveBalls.Properties;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GameControllers.Entities.Balls
{
    public class Ball : Entity<Ball>, ICanTakeDamage
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private CircleCollider2D _circleCollider;
        [SerializeField] private TMP_Text _numberLivesText;
        [SerializeField] private ConfigBall _configBall;
        [SerializeField] private Transform _transformPhysicComponent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AudioSource _destroySound;
        private bool _isRightSpawn;
        private int _indexWallLayer;
        private int _currentNumberLives;
        private ICanChooseBall _ballChooser;
        private Sequence _appearingAnimation;
        private int _minLives;
        private int _maxLives;
        private bool _isDead;
        private const float ForceAwakeChildValue = 1f;
        private const float MinStartForce = 0.3f;
        private const float MaxStartForce = 0.8f;
        private const float DeadDelay = 1f;

        public bool IsAppears { get; private set; }

        public static Action<Transform> OnSpawnCoin;
        public static Action OnDestroyBall;

        [Inject]
        private void Construct(ICanChooseBall ballChooser)
        {
            _ballChooser = ballChooser;
        }

        public override void SpawnInit(Action<Entity<Ball>> returnAction)
        {
            IsAppears = true;
            _indexWallLayer = LayerMask.NameToLayer("Wall");
            base.SpawnInit(returnAction);
        }

        public override void AwakeInit(Transform startPosition)
        {
            _transformPhysicComponent.localPosition = Vector3.zero;
            _isRightSpawn = startPosition.rotation.eulerAngles.z != 0;
            
            _currentNumberLives = Random.Range(_minLives, _maxLives + 1);
            _numberLivesText.text = _currentNumberLives.ToString();
            
            StartAppearingAnimation();
            base.AwakeInit(startPosition);
        }

        public void AwakeChildBall(bool isLeftEntity, Transform pointSpawn)
        {
            _isRightSpawn = !isLeftEntity;
            ReachPlayingField();
            PushUpChildBall(pointSpawn);
        }

        public void SetLives(int minLives, int maxLives)
        {
            _minLives = minLives;
            _maxLives = maxLives;
        }

        private void PushUpChildBall(Transform pointSpawn)
        {
            var extraPush = 0f;
            if (pointSpawn.position.y < 0)
                extraPush = pointSpawn.position.y * -0.5f;

            var pushValue = ForceAwakeChildValue + extraPush;
            
            _rigidbody.AddForce(_transformPhysicComponent.up * pushValue, ForceMode2D.Impulse);
        }

        private void Update()
        {
            if (IsAppears)
            {
                if (_isRightSpawn)
                    transform.position += Vector3.left * Time.deltaTime;
                else
                    transform.position += Vector3.right * Time.deltaTime;
            }
        }

        private void EnablePhysicalMovement()
        {
            _appearingAnimation.Kill();
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _rigidbody.gravityScale = 0.4f;
            
            if (_isRightSpawn)
                _rigidbody.AddForce(Vector3.left * Random.Range(MinStartForce, MaxStartForce), ForceMode2D.Impulse);
            else
                _rigidbody.AddForce(Vector3.right * Random.Range(MinStartForce, MaxStartForce), ForceMode2D.Impulse);
        }

        public void ReachPlayingField()
        {
            _circleCollider.excludeLayers = 
                _circleCollider.excludeLayers & ~(1 << _indexWallLayer);
            IsAppears = false;
            EnablePhysicalMovement();
        }

        private void DoDestroy()
        {
            OnDestroyBall.Invoke();
            
            if (_configBall.LevelBall > 0)
                _ballChooser.ChooseChildBalls(_transformPhysicComponent, _configBall.LevelBall - 1);
            else 
                OnSpawnCoin?.Invoke(_transformPhysicComponent);

            _circleCollider.enabled = false;
            _numberLivesText.enabled = false;
            _spriteRenderer.enabled = false;
            
            StartCoroutine(AnimationDeadDelay());
        }

        private IEnumerator AnimationDeadDelay()
        {
            _destroySound.Play();
            yield return new WaitForSeconds(DeadDelay);
            ResetBallValue();
            ReturnToPool();
        }

        private void ResetBallValue()
        {
            _isDead = false;
            _spriteRenderer.enabled = true;
            _circleCollider.enabled = true;
            _numberLivesText.enabled = true;
            _circleCollider.excludeLayers = 
                _circleCollider.excludeLayers | (1 << _indexWallLayer);
            IsAppears = true;
            _rigidbody.gravityScale = 0f;
            _transformPhysicComponent.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void StartAppearingAnimation()
        {
            _appearingAnimation = DOTween.Sequence()
                .Append(_spriteRenderer.DOColor(new Color(1f, 1f, 1f, 0.5f), 0.5f))
                .Append(_spriteRenderer.DOColor(new Color(1f, 1f, 1f, 1f), 0.5f))
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void TakeDamage(int damage)
        {
            if (IsAppears || _isDead) return;
            
            _currentNumberLives -= damage;
            _numberLivesText.text = _currentNumberLives.ToString();
            
            if (_currentNumberLives <= 0)
            {
                _isDead = true;
                DoDestroy();
            }
        }
    }
}
