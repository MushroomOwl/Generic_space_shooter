using UnityEngine;

namespace Game
{
    public class HommingProjectileComponent : MonoBehaviour
    {
        [SerializeField] private Projectile _Projectile;
        [SerializeField] private Destructable _Target;
        [SerializeField] private float _RotationSpeed;

        void Start()
        {
            FindNearestTarget();
        }

        void Update()
        {
            if (_Target == null)
            {
                FindNearestTarget();
            }

            CorrectCourse();
        }

        private void FindNearestTarget()
        {
            Destructable nearestTarget = null;
            float closestSqrDistance = float.MaxValue;

            foreach (var target in Destructable.AllDestructables)
            {
                float sqrDistance = Vector2.SqrMagnitude(transform.position - target.transform.position);
                if (closestSqrDistance > sqrDistance && target.FractionId != _Projectile.Source.FractionId && target.FractionId != 0)
                {
                    nearestTarget = target;
                    closestSqrDistance = sqrDistance;
                }
            }

            if (nearestTarget == null)
            {
                return;
            }

            _Target = nearestTarget.GetComponent<Destructable>();
        }

        private void CorrectCourse()
        {
            if (_Target == null)
            {
                _Projectile.SetAngularVelocityDeg(0);
                return;
            }

            Vector2 direction = _Target.transform.position - transform.position;
            direction.Normalize();
            float rotationRad = Mathf.Asin(Vector3.Cross(transform.up, direction).z);
            _Projectile.SetAngularVelocityDeg(rotationRad * Mathf.Rad2Deg * _RotationSpeed);
        }
    }
}
