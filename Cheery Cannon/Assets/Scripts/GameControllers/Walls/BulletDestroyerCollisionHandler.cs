using GameControllers.Entities.Bullets;
using UnityEngine;

namespace GameControllers.Walls
{
    public class BulletDestroyerCollisionHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet bullet))
                bullet.DoDestroy();
        }
    }
}
