using UnityEngine;


namespace Game
{
    public class CollisionDamageDealer : MonoBehaviour
    {
        public static string IngoreTag = "WorldBoundary";

        [SerializeField] private float _VelocityDamageCoef;
        [SerializeField] private float _ConstDamage;
        [SerializeField] private float _TickLength = 0.5f;
        private float _UntilDamageTickEnd;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IngoreTag)
            {
                return;
            }

            Destructable selfDest = transform.root.GetComponent<Destructable>();

            if (selfDest != null)
            {
                selfDest.ApplyDamage((int)_ConstDamage + (int)(_VelocityDamageCoef * collision.relativeVelocity.magnitude));
                _UntilDamageTickEnd = _TickLength;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_UntilDamageTickEnd > 0)
            {
                return;
            }

            if (collision.transform.tag == IngoreTag)
            {
                return;
            }

            Destructable selfDest = transform.root.GetComponent<Destructable>();

            if (selfDest != null)
            {
                selfDest.ApplyDamage((int)_ConstDamage);
            }

            _UntilDamageTickEnd -= Time.deltaTime;
        }
    }
}