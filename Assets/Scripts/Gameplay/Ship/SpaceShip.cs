using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructable
    {
        private Rigidbody2D _Rigid;

        [SerializeField] private bool _IgnoreBoundary;

        [SerializeField] private float _Mass;
        [SerializeField] private float _Thrust;
        [SerializeField] private float _Mobility;
        [SerializeField] private float _MaxLinearVelocity;
        [SerializeField] private float _MaxAngularVelocity;
        [SerializeField] private Turret[] _Turrets;
        [SerializeField] private GameObject _ShipExplosionPrefab;

        private bool _IsPlayerControlled;
        public bool IsPlayerControlled => _IsPlayerControlled;

        private UnityAction _ExplosionCallback;

        [SerializeField] private int _MaxEnergy;
        [SerializeField] private int _MaxAmmo;
        [SerializeField] private float _Energy;
        [SerializeField] private int _Ammo;
        [SerializeField] private float _EnergyRegenPerSecond;

        public int CurrentAmmo => _Ammo;
        public float CurrentEnergyPercent => 100 * _Energy / _MaxEnergy;
        public float CurrentHealthPercent => 100 * CurrentHitPoints / MaxHitPoints;

        private UnityEvent _EventOnEnergy = new UnityEvent();
        private UnityEvent _EventOnAmmo = new UnityEvent();
        public UnityEvent EventOnEnergy => _EventOnEnergy;
        public UnityEvent EventOnAmmo => _EventOnAmmo;

        private UnityEvent _LoadoutChanged = new UnityEvent();
        public UnityEvent LoadoutChanged => _LoadoutChanged;

        private List<Bonus> _BonusList;
        public List<Bonus> BonusList => _BonusList;

        public float ThrustControl { get; set; }
        public float TorqueControl { get; set; }

        protected override void Start()
        {
            base.Start();

            _BonusList = new List<Bonus>();

            EventOnDeath.AddListener(SpawnDeathAnimation);

            _Rigid = GetComponent<Rigidbody2D>();
            _Rigid.mass = _Mass;

            _Rigid.inertia = 1;

            _Energy = _MaxEnergy;
            _Ammo = 0;

            EventOnEnergy?.Invoke();
            EventOnHealth?.Invoke();
            EventOnAmmo?.Invoke();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            RegenEnergy();
            _BonusList.RemoveAll(v => v == null);
        }

        public void ApplyShipParameters(ShipProperties prop)
        {
            _MaxHitPoints = prop.MaxHP;
            _CurrentHitPoints = _MaxHitPoints;
            _MaxEnergy = prop.MaxEnergy;
            _Energy = _MaxEnergy;
            _MaxAmmo = prop.MaxAmmo;
            _Ammo = _MaxAmmo;
            _MaxLinearVelocity = prop.MaxLinearVelocity;
            _MaxAngularVelocity = prop.MaxAngularVelocity;
            _Thrust = prop.Thrust;
            _Mobility = prop.Mobility;
            _EnergyRegenPerSecond = prop.EnergyRegen;
            AssignWeapon(prop.DefaultWeapon);
        }

        public void SetExplosionCallback(UnityAction explosionCallback)
        {
            _ExplosionCallback = explosionCallback;
        }

        public void SetAsPlayerControlled()
        {
            _IsPlayerControlled = true;
        }

        public void SetHealthUpdateCallback(UnityAction cb)
        {
            _EventOnHealth.AddListener(cb);
        }

        public void SetEnergyUpdateCallback(UnityAction cb)
        {
            _EventOnEnergy.AddListener(cb);
        }

        public void SetAmmoUpdateCallback(UnityAction cb)
        {
            _EventOnAmmo.AddListener(cb);
        }

        private void SpawnDeathAnimation()
        {
            ShipExplosion shipExplosion = Instantiate(_ShipExplosionPrefab).GetComponent<ShipExplosion>();
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            shipExplosion.Init(sprite.sprite);
            shipExplosion.transform.position = transform.position;
            if (_ExplosionCallback != null)
            {
                shipExplosion.EventOnEnd.AddListener(_ExplosionCallback);
            }
        }

        public void ChangeMobility(float amt)
        {
            _Mobility += amt;
        }

        public void ChangeThrust(float amt)
        {
            _Thrust += amt;
        }

        public void ChangeMaxLinearVelocity(float amt)
        {
            _MaxLinearVelocity += amt;
        }

        public void ChangeMaxAngularVelocity(float amt)
        {
            _MaxAngularVelocity += amt;
        }

        private void UpdateRigidBody()
        {
            _Rigid.AddForce(ThrustControl * _Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            _Rigid.AddForce(-_Rigid.velocity * (_Thrust / _MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            _Rigid.AddTorque(TorqueControl * _Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            _Rigid.AddTorque(-_Rigid.angularVelocity * (_Mobility / _MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < _Turrets.Length; i++)
            {
                if (_Turrets[i].Mode == mode)
                {
                    _Turrets[i].Fire();
                }
            }
        }

        public void AssignWeapon(TurretProperties weapon)
        {
            for (int i = 0; i < _Turrets.Length; i++)
            {
                if (_Turrets[i].Mode == weapon.Mode)
                {
                    _Turrets[i].AssignLoadout(weapon);
                }
            }

            LoadoutChanged?.Invoke();
        }

        public bool TryConsumeEnergy(float amt)
        {
            if (_Energy >= amt)
            {
                _Energy -= amt;
                EventOnEnergy?.Invoke();
                return true;
            }

            return false;
        }

        public bool TryConsumeAmmo(int amt)
        {
            if (_Ammo >= amt)
            {
                _Ammo -= amt;
                EventOnAmmo?.Invoke();
                return true;
            }

            return false;
        }

        public void RestoreEnergy(float amt)
        {
            _Energy += amt;
            if (_Energy > _MaxEnergy) _Energy = _MaxEnergy;
            EventOnEnergy?.Invoke();
        }

        public void RestoreAmmo(int amt)
        {
            _Ammo += amt;
            if (_Ammo > _MaxAmmo) _Ammo = _MaxAmmo;
            EventOnAmmo?.Invoke();
        }

        public void RegenEnergy()
        {
            RestoreEnergy(_EnergyRegenPerSecond * Time.deltaTime);
        }

        public List<TurretProperties> GetCurrentLoadout()
        {
            List<TurretProperties> list = new List<TurretProperties>();

            foreach(var turret in _Turrets)
            {
                list.Add(turret.Loadout);
            }

            return list;
        }
    }
}
