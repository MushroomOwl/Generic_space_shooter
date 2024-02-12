using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LevelList : MonoSingleton<LevelList>
    {
        [SerializeField] private List<LevelProperties> _Levels;
        public static IReadOnlyList<LevelProperties> Levels => Instance._Levels;
    }
}

