using System;
using System.Collections;
using GameControllers.GameSystems;
using UnityEngine;

namespace GameControllers.PlayerControllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _positionY;
        private Rigidbody2D _rigidbody;
        private Vector3 _movePosition;
        private Cannon _cannon;
        private bool _isDead;
        private bool _gameOver;

        private void OnEnable()
        {
            PlayerCollisionHandler.OnDestroyPlayer += DestroyPlayer;
            LevelProgressController.OnWin += ChangeMoveState;
        }

        private void OnDisable()
        {
            PlayerCollisionHandler.OnDestroyPlayer -= DestroyPlayer;
            LevelProgressController.OnWin -= ChangeMoveState;
        }

        private void Awake()
        {
            InitMovePosition(0);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _cannon = GetComponentInChildren<Cannon>();
        }

        public void InitMovePosition(float clickPositionX)
        {
            _movePosition = new Vector3(clickPositionX, _positionY, 0);
        }
        
        private void FixedUpdate()
        {
            if (_isDead || _gameOver) return;
            
            Move();
        }
        
        private void Move()
        {
            var targetX = Mathf.Lerp(transform.position.x, _movePosition.x, _speed * Time.deltaTime);
            _rigidbody.MovePosition(new Vector2(targetX, transform.position.y));
            
            _cannon.RotateWheel(targetX - transform.position.x);
        }

        private void ChangeMoveState()
        {
            _gameOver = true;
            _rigidbody.velocity = Vector2.zero;
        }

        private void DestroyPlayer(Vector3 enemyPosition)
        {
            _isDead = true;
            ChangePhysic();
            PushPlayer(enemyPosition);
        }

        private void PushPlayer(Vector3 enemyPosition)
        {
            var directionHit = transform.position - enemyPosition;
            var rotatedVector = new Vector2(-directionHit.y, directionHit.x);
            var angleRotatePlayer = Vector2.Angle(transform.position, rotatedVector);

            if (angleRotatePlayer > 90f)
                angleRotatePlayer -= 180f;
            
            StartCoroutine(RotatePlayer(angleRotatePlayer));
            _rigidbody.AddForce(directionHit, ForceMode2D.Impulse);
        }

        private IEnumerator RotatePlayer(float angleRotatePlayer)
        {
            while (Math.Abs(transform.rotation.eulerAngles.z - angleRotatePlayer) >= 2)
            {
                transform.rotation = Quaternion.Euler(
                    Vector3.Lerp(
                        transform.rotation.eulerAngles, 
                        new Vector3(0f, 0f, angleRotatePlayer), 
                        Time.deltaTime));
                
                yield return null;
            }
        }

        private void ChangePhysic()
        {
            _rigidbody.freezeRotation = false;
            _rigidbody.gravityScale = 0.1f;
            _rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            _rigidbody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
