using UnityEngine;

namespace GameControllers.Entities.Balls
{
    public class BallCollisionHandler : MonoBehaviour
    {
        [SerializeField] private Ball _ball;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("TriggerEndSpawn") && _ball.IsAppears)
            {
                _ball.ReachPlayingField();
            }
        }
    }
}
