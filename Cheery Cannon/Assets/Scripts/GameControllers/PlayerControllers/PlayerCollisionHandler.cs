using System;
using GameControllers.Entities.Balls;
using GameControllers.Entities.Coins.Properties;
using UnityEngine;

namespace GameControllers.PlayerControllers
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private Collider2D _playerCollider;
        
        public static Action<int> OnGetCoin;
        public static Action OnLose;
        public static Action<Vector3> OnDestroyPlayer;

        private void Awake()
        {
            _playerCollider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BallCollisionHandler ball))
            {
                OnLose?.Invoke();
                OnDestroyPlayer?.Invoke(ball.gameObject.transform.position);
                _playerCollider.enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ICanGiveCoin coin))
            {
                OnGetCoin.Invoke(coin.GiveCoin());
            }
        }
    }
}
