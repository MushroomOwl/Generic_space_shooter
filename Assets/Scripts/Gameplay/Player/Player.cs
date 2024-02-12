using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Player : MonoSingleton<Player>
    {
        static ShipProperties _SelectedShip;

        private SpaceShip _ActiveShip;
        public static SpaceShip ActiveShip => _Instance._ActiveShip;

        private FollowingCameraHandler _CameraHandler;

        [SerializeField] private Vector3 _SpawnPoint;

        [SerializeField] private int _Score;
        [SerializeField] private int _KillCount;
        [SerializeField] private int _NumLives;

        private UnityEvent _ScoreChanged = new UnityEvent();
        private UnityEvent _KillsChanged = new UnityEvent();
        private UnityEvent _LivesChanged = new UnityEvent();

        public static UnityEvent ScoreChanged => _Instance._ScoreChanged;
        public static UnityEvent KillsChanged => _Instance._KillsChanged;
        public static UnityEvent LivesChanged => _Instance._LivesChanged;

        public static int Score => _Instance._Score;
        public static int KillCount => _Instance._KillCount;
        public static int NumLives => _Instance._NumLives;

        private void Start()
        {
            if (_SelectedShip == null)
            {
                _SelectedShip = ShipList.Player[0];
            }

            _ScoreChanged.AddListener(HUD.UpdateScoreIndicator);
            _KillsChanged.AddListener(HUD.UpdateKillsIndicator);
            _LivesChanged.AddListener(HUD.UpdateLivesIndicator);

            SpawnShip();
        }

        public static void SetPlayerShip(ShipProperties props)
        {
            _SelectedShip = props;
        }

        public void SetSpawnPoint(Vector3 point)
        {
            _SpawnPoint = point;
        }

        public void SetCamera(FollowingCameraHandler camera)
        {
            _CameraHandler = camera;
        }

        public void HandleShipDestruction()
        {
            _NumLives--;

            if (_NumLives > 0)
            {
                SpawnShip();
            }

            LivesChanged?.Invoke();
        }

        private void SpawnShip()
        {
            _ActiveShip = Instantiate(_SelectedShip.Prefab, _SpawnPoint, Quaternion.identity).GetComponent<SpaceShip>();
            _ActiveShip.ApplyShipParameters(_SelectedShip);

            _ActiveShip.SetExplosionCallback(HandleShipDestruction);

            _ActiveShip.SetEnergyUpdateCallback(HUD.UpdateEnergyIndicator);
            _ActiveShip.SetHealthUpdateCallback(HUD.UpdateHealthIndicator);
            _ActiveShip.SetAmmoUpdateCallback(HUD.UpdateAmmoIndicator);

            _ActiveShip.SetAsPlayerControlled();

            _CameraHandler.SetTarget(_ActiveShip.transform);
            InputHandler.SetTargetToControl(_ActiveShip);
        }

        public static void AddKill()
        {
            _Instance._KillCount++;
            _Instance._KillsChanged?.Invoke();
        }

        public static void AddScore(int amount)
        {
            _Instance._Score += amount;
            _Instance._ScoreChanged?.Invoke();
        }
    }
}

