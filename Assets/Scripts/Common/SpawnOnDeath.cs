using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Destructable))]
    public class SpawnOnDeath : MonoBehaviour
    {
        [SerializeField] private GameObject[] _Prefabs;

        private void Start()
        {
            Destructable dest = GetComponent<Destructable>();

            int prefabNumber = Random.Range(0, _Prefabs.Length + 1);

            if (prefabNumber == _Prefabs.Length)
            {
                return;
            }

            dest.EventOnDeath.AddListener(() => Instantiate(
                _Prefabs[prefabNumber], dest.transform.position, Quaternion.identity)
            );
        }
    }
}
