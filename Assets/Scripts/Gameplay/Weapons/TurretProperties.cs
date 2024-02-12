using UnityEngine;

namespace Game
{
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode _Mode;
        public TurretMode Mode => _Mode;

        [SerializeField] private Projectile _ProjectilePrefab;
        public Projectile ProjectilePrefab => _ProjectilePrefab;

        [SerializeField] private float _RateOfFire;
        public float RateOfFire => _RateOfFire;

        [SerializeField] private int _EnergyUsage;
        public int EnergyUsage => _EnergyUsage;

        [SerializeField] private int _AmmoUsage;
        public int AmmoUsage => _AmmoUsage;

        [SerializeField] private float _SpreadDegrees;
        public float SpreadDegrees => _SpreadDegrees;

        [SerializeField] private int _ProjectileCount;
        public int ProjectileCount => _ProjectileCount;

        [SerializeField] private AudioClip _LaunchSFX;
        public AudioClip LaunchSFX => _LaunchSFX;
    }
}
