using UnityEngine;

namespace Game
{
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float _Force;
        [SerializeField] private float _Radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null)
            {
                return;
            }

            Vector2 dir = transform.position - collision.transform.position;

            float dist = dir.magnitude;

            if (dist < _Radius)
            {
                Vector2 force = dir.normalized * _Force * (dist / _Radius);
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = _Radius;
        }
    }
}
