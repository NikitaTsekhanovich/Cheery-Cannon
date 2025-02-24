using DG.Tweening;
using UnityEngine;

namespace GameControllers
{
    public class WallCollisionHandler : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private const string CompressionSpeedKey = "_CompressionSpeed";
        private static readonly int CompressionSpeed = Shader.PropertyToID(CompressionSpeedKey);
        private const float AnimationPlayerDelay = 1f;
        
        private void Awake()
        {
            _material.SetFloat(CompressionSpeed, 0);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            _material.SetFloat(CompressionSpeed, 10f);
            _material.DOFloat(0f, CompressionSpeed, AnimationPlayerDelay);
        }
    }
}
