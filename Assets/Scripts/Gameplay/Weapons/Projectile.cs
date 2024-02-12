using UnityEngine;

namespace Game
{
    public class Projectile : TempEntity
    {
        [SerializeField] private float _Velocity;
        [SerializeField] private float _AngularVelocityDeg;
        [SerializeField] private int _Damage;
        [SerializeField] private GameObject _ImpactEffectPrefab;
        [SerializeField] private Destructable _Parent;
        [SerializeField] private bool _PassObstacles;

        [SerializeField] private bool _HasCollider;

        public Destructable Source => _Parent;

        void Start()
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                _HasCollider = true;
            }
        }

        private void Update()
        {
            float stepLength = Time.deltaTime * _Velocity;

            if (_AngularVelocityDeg != 0)
            {
                float angleDeg = _AngularVelocityDeg * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angleDeg);
            }

            Vector2 step = transform.up * stepLength;

            if (_HasCollider)
            {
                transform.position += new Vector3(step.x, step.y, 0);
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                Destructable dest = hit.collider.GetComponentInParent<Destructable>();

                if (dest == null && _PassObstacles)
                {
                    transform.position += new Vector3(step.x, step.y, 0);
                    return;
                }

                if (dest != null)
                {
                    if (dest.FractionId == _Parent.FractionId)
                    {
                        transform.position += new Vector3(step.x, step.y, 0);
                        return;
                    }

                    dest.ApplyDamage(_Damage);

                    if (_Parent == Player.ActiveShip && dest.CurrentHitPoints <= 0)
                    {
                        if (dest.CountAsKill) Player.AddKill();
                        Player.AddScore(dest.ScoreOnKill);
                    }
                }

                transform.position += new Vector3(hit.point.x - transform.position.x, hit.point.y - transform.position.y, 0);
                OnProjectileImpact(hit.collider, hit.point);

                return;
            }
            else
            {
                transform.position += new Vector3(step.x, step.y, 0);
            }
        }

        public void SetAngularVelocityDeg(float vel)
        {
            _AngularVelocityDeg = vel;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destructable dest = collision.GetComponentInParent<Destructable>();

            if (dest == null && _PassObstacles)
            {
                return;
            }

            if (dest != null)
            {
                if (dest.FractionId == _Parent.FractionId) return;

                dest.ApplyDamage(_Damage);
                if (_Parent == Player.ActiveShip && dest.CurrentHitPoints <= 0)
                {
                    if (dest.CountAsKill) Player.AddKill();
                    Player.AddScore(dest.ScoreOnKill);
                }
            }

            OnProjectileImpact(collision, transform.position);
        }

        private void OnProjectileImpact(Collider2D col, Vector2 pos)
        {
            GameObject impact = Instantiate(_ImpactEffectPrefab, transform);
            impact.transform.parent = null;

            TempDamageArea damageArea = impact.GetComponentInChildren<TempDamageArea>();
            if (damageArea != null)
            {
                damageArea.SetParent(_Parent);
            }

            Destroy(gameObject);
        }

        public void SetParent(Destructable parent)
        {
            _Parent = parent;
        }
    }
}
