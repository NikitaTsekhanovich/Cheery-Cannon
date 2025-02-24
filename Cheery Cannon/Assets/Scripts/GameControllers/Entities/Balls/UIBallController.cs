using UnityEngine;

namespace GameControllers.Entities.Balls
{
    public class UIBallController : MonoBehaviour
    {
        [SerializeField] private Transform _ball;

        private void Update()
        {
            transform.position = _ball.position;
        }
    }
}
