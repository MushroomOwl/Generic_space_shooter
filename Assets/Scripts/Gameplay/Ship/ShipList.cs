using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ShipList : MonoSingleton<ShipList>
    {
        [SerializeField] private List<ShipProperties> _Player;
        public static IReadOnlyList<ShipProperties> Player => Instance._Player;

        [SerializeField] private List<ShipProperties> _AI;
        public static IReadOnlyList<ShipProperties> AI => Instance._AI;
    }
}

