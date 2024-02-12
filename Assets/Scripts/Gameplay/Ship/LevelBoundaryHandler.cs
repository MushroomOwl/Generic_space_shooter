using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class LevelBoundaryHandler : MonoBehaviour
    {
        private UnityEvent _OnTeleport = new UnityEvent();
        public UnityEvent TeleportEvent => _OnTeleport;

        public void FixedUpdate()
        {
            if (!LevelBoundaries.Initialized)
            {
                return;
            }

            var r = LevelBoundaries.Radius;

            if (transform.position.magnitude >= r)
            {
                if (LevelBoundaries.BodundaryMode == LevelBoundaries.Mode.StopWall)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (LevelBoundaries.BodundaryMode == LevelBoundaries.Mode.Teleport)
                {
                    transform.position = transform.position - 2 * transform.position.normalized * r;
                    _OnTeleport.Invoke();
                }
            }
        }
    }
}
