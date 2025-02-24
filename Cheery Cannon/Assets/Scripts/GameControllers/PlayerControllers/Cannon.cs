using GameControllers.Entities.Coins;
using UnityEngine;

namespace GameControllers.PlayerControllers
{
    public class Cannon : MonoBehaviour
    {
        [field: SerializeField] public Transform[] ShootPoints { get; private set; }
        [SerializeField] private Transform _wheelTransform;
        private const float SpeedRotate = 70f;

        private void OnEnable()
        {
            Coin.OnGetPlayerPosition += GetPosition;
        }

        private void OnDisable()
        {
            Coin.OnGetPlayerPosition -= GetPosition;
        }

        private Vector3 GetPosition() => transform.position;

        public void RotateWheel(float directionX)
        {
            _wheelTransform.Rotate(0, 0, -1 * directionX * SpeedRotate);
        }
    }
}
