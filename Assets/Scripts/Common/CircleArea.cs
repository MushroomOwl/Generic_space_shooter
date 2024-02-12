using UnityEngine;

namespace Game
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float _Radius;

        public Vector2 GetRandomInnerPoint()
        {
            return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * _Radius;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            GizmosExtensions.DrawCircle(gameObject.transform.position, _Radius, 20, Color.yellow);
        }
#endif
    }
}
