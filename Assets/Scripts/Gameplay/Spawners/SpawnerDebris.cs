using UnityEngine;

namespace Game
{
    public class SpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructable[] _EntityPrefabs;
        [SerializeField] private CircleArea _Area;
        [SerializeField] private int _NumSpawns;
        [SerializeField] private float _MaxSpawnSpeed;

        private void Start()
        {
            for (int i = 0; i < _NumSpawns; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris()
        {
            Vector2 spawnPoint = _Area.GetRandomInnerPoint();
            int entityIndex = Random.Range(0, _EntityPrefabs.Length);
            Destructable entity = Instantiate(_EntityPrefabs[entityIndex], new Vector3(spawnPoint.x, spawnPoint.y, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            entity.GetComponent<Destructable>().EventOnDeath.AddListener(OnDebrisDestroyed);

            Rigidbody2D entityBody = entity.GetComponent<Rigidbody2D>();

            if (entityBody == null)
            {
                return;
            }

            entityBody.velocity = Random.insideUnitCircle * Random.Range(0, _MaxSpawnSpeed);
        }

        private void OnDebrisDestroyed()
        {
            SpawnDebris();
        }
    }
}
