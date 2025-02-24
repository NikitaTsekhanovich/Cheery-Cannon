using System;
using GameControllers.Entities.Balls;
using GameControllers.Entities.Coins;
using GameControllers.PlayerControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject.SpaceFighter;
using Bullet = GameControllers.Entities.Bullets.Bullet;

namespace GameControllers.Launchers
{
    [Serializable]
    public class DataLaunch
    {
        [field: Header("Player data")]
        [field: SerializeField] public PhysicsMovement PlayerMovement { get; private set; }
        [field: SerializeField] public GameObject Player { get; private set; }
        [field: SerializeField] public AudioSource ShootSound { get; private set; }
        
        
        [field: Header("Bullet data")]
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        
        
        [field: Header("Balls data")]
        [field: SerializeField] public Ball[] BallsPrefabs { get; private set; }
        [field: SerializeField] public Transform[] BallSpawnPoints { get; private set; }
        
        
        [field: Header("Coin data")]
        [field: SerializeField] public Coin[] CoinPrefabs { get; private set; }
        
        
        [field: Header("Player Interface Data")]
        [field: SerializeField] public TMP_Text CoinsText { get; private set; }
        [field: SerializeField] public Image ProgressLevelBar { get; private set; }
        [field: SerializeField] public TMP_Text CurrentLevelText { get; private set; }
    }
}
