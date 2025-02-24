using GameControllers.Launchers.SystemsProperties;
using GameControllers.PlayerControllers;
using UnityEngine;

namespace GameControllers.GameSystems
{
    public class InputController : IHaveUpdate
    {
        private readonly PhysicsMovement _physicsMovement;
        private readonly Camera _camera;
        
        public InputController(PhysicsMovement physicsMovement)
        {
            _physicsMovement = physicsMovement;
            
            _camera = Camera.main;
        }
        
        public void Update()
        {
            CheckClickInput();
        }

        private void CheckClickInput()
        {
            if (Input.GetMouseButton(0))
            {
                var clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                var hits = Physics2D.RaycastAll(clickPosition, Vector2.zero);

                foreach (var hit in hits)
                {
                    if (hit.collider != null && hit.collider.CompareTag("ClickField"))
                    {
                        _physicsMovement.InitMovePosition(clickPosition.x);
                    }
                }
            }
        }
    }
}
