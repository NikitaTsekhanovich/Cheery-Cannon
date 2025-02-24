using UnityEngine;

namespace GameControllers.Walls
{
    public class WallPositionController : MonoBehaviour
    {
        [SerializeField] private Transform _leftWall;
        [SerializeField] private Transform _rightWall;
        private const float OffsetWallBlocks = 18f;
        private const float ReferenceWidth = 1080f;
        private const float ReferenceHeight = 1920f;
        private const float Match = 0.5f;
        private float _scaleFactor;
        
        public float HeightCanvas { get; private set; }
        public float WidthCanvas { get; private set; }

        private void Awake()
        {
            CalculateHeightCanvas();
        }

        private void CalculateHeightCanvas()
        {
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            
            var widthScale = screenWidth / ReferenceWidth;
            var heightScale = screenHeight / ReferenceHeight;

            _scaleFactor = Mathf.Pow(widthScale * heightScale, Match);
            HeightCanvas = screenHeight / _scaleFactor;
            WidthCanvas = screenWidth / _scaleFactor;
            
            _leftWall.localPosition -= new Vector3(WidthCanvas / 2 - OffsetWallBlocks, 0f, 0f);
            _rightWall.localPosition += new Vector3(WidthCanvas / 2 - OffsetWallBlocks, 0f, 0f);
        }
    }
}
