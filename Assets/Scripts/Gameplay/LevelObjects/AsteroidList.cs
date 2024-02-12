using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AsteroidList : MonoSingleton<AsteroidList>
    {
        [SerializeField] private List<DestAsteroid> _Medium;
        [SerializeField] private List<DestAsteroid> _Small;

        public static DestAsteroid GetMediumAsteroidPrefab()
        {
            int index = Random.Range(0, _Instance._Medium.Count);
            return _Instance._Medium[index];
        }

        public static DestAsteroid GetSmallAsteroidPrefab()
        {
            int index = Random.Range(0, _Instance._Small.Count);
            return _Instance._Small[index];
        }
    }
}
