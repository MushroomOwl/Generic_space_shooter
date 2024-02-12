using Unity.Burst;
using UnityEngine;

namespace Game
{
    public class LevelBoundaries : MonoSingleton<LevelBoundaries>
    {
        [SerializeField] private float _Radius;
        public static float Radius => _Instance._Radius;

        public enum Mode
        {
            StopWall,
            Teleport
        }

        [SerializeField] private Mode _BodundaryMode;
        public static Mode BodundaryMode => _Instance._BodundaryMode;

# if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            GizmosExtensions.DrawCircle(transform.position, _Radius, 20, Color.green);
        }
# endif
    }
}
