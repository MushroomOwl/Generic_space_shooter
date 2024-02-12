using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public sealed class ShipProperties : ScriptableObject
    {
        [SerializeField] private SpaceShip _Prefab;
        public SpaceShip Prefab => _Prefab;

        [SerializeField] private Sprite _Icon;
        public Sprite Icon => _Icon;

        [SerializeField] private string _Name;
        public string Name => _Name;

        [SerializeField] private int _MaxHP;
        public int MaxHP => _MaxHP;

        [SerializeField] private int _MaxEnergy;
        public int MaxEnergy => _MaxEnergy;

        [SerializeField] private int _MaxAmmo;
        public int MaxAmmo => _MaxAmmo;

        [SerializeField] private float _EnergyRegen;
        public float EnergyRegen => _EnergyRegen;

        [SerializeField] private TurretProperties _DefaultWeapon;
        public TurretProperties DefaultWeapon => _DefaultWeapon;

        [SerializeField] private float _Thrust;
        public float Thrust => _Thrust;

        [SerializeField] private float _Mobility;
        public float Mobility => _Mobility;

        [SerializeField] private float _MaxLinearVelocity;
        public float MaxLinearVelocity => _MaxLinearVelocity;

        [SerializeField] private float _MaxAngularVelocity;
        public float MaxAngularVelocity => _MaxAngularVelocity;
    }
}
