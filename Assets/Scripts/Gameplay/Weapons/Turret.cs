using UnityEngine;

namespace Game
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode _Mode;
        public TurretMode Mode => _Mode;

        [SerializeField] private TurretProperties _TurretProperties;

        public TurretProperties Loadout => _TurretProperties;

        private float _RefireTimer;

        public bool CanFire => _RefireTimer <= 0;

        private SpaceShip _Ship;

        private void Start()
        {
            _Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (_RefireTimer > 0)
            {
                _RefireTimer -= Time.deltaTime;
            }
        }

        public void Fire()
        {
            if (_TurretProperties == null) return;

            if (!CanFire) return;

            if (!_TurretProperties.ProjectilePrefab) return;

            if (_Ship == null) return;

            bool enoughResources = false;

            switch (_Mode)
            {
                case TurretMode.Primary:
                    enoughResources = _Ship.TryConsumeEnergy(_TurretProperties.EnergyUsage);
                    break;
                case TurretMode.Secondary:
                    enoughResources = _Ship.TryConsumeAmmo(_TurretProperties.AmmoUsage);
                    break;
            }

            if (!enoughResources)
            {
                return;
            }

            for (int i = 0; i < _TurretProperties.ProjectileCount; i++)
            {
                Projectile projectile = Instantiate(_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
                float spreadDegrees = _TurretProperties.SpreadDegrees;
                float rotation = Random.Range(0, spreadDegrees) - spreadDegrees / 2;

                Quaternion rotationQuaternion = Quaternion.Euler(0, 0, rotation);

                projectile.transform.position = transform.position;
                projectile.transform.up = rotationQuaternion * transform.up;
                projectile.SetParent(_Ship);
            }

            _RefireTimer = _TurretProperties.RateOfFire;
        }

        public void AssignLoadout(TurretProperties props)
        {
            if (_Mode != props.Mode) return;

            _RefireTimer = 0;
            _TurretProperties = props;
        }
    }
}
