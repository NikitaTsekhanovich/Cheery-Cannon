using UnityEngine;

namespace GameControllers.GameSystems.WaveBalls.Properties
{
    public interface ICanChooseBall
    {
        public void ChooseChildBalls(Transform pointSpawn, int levelBall);
    }
}
