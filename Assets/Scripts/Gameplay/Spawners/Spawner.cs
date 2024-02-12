using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Spawner : EntityWithTimer
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private GameObject[] _EntityPrefabs;
        [SerializeField] private CircleArea _Area;
        [SerializeField] private SpawnMode _Mode;
        [SerializeField] private int _SpawnsAtOnce;
        [SerializeField] private float _MaxSpawns;

        [SerializeField] private float _RespawnTime;
        private static string _RespawnTimerName = "rt";

        private List<GameObject> _Spawns;

        private void Awake()
        {
            _Spawns = new List<GameObject>();
        }

        private void Start()
        {
            if (_Mode == SpawnMode.Start)
            {
                SpawnEntities();
                return;
            }

            AddTimer(_RespawnTimerName, _RespawnTime);
            AddCallback(_RespawnTimerName, SpawnEntities);
        }

        private void Update()
        {
            _Spawns.RemoveAll(v => v == null);
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < _SpawnsAtOnce && _Spawns.Count < _MaxSpawns; i++)
            {
                Vector2 spawnPoint = _Area.GetRandomInnerPoint();
                int entityIndex = Random.Range(0, _EntityPrefabs.Length);
                GameObject entity = Instantiate(_EntityPrefabs[entityIndex], new Vector3(spawnPoint.x, spawnPoint.y, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                _Spawns.Add(entity);
            }
        }
    }
}
