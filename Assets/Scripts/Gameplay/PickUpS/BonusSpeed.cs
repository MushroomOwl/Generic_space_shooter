using UnityEngine;

namespace Game
{
    public sealed class BonusSpeed : Bonus
    {
        [SerializeField] private float MobilityChange;
        [SerializeField] private float ThrustChange;
        [SerializeField] private float MaxLinearChange;
        [SerializeField] private float MaxAngularChange;

        protected override void ApplyEffect()
        {
            _Ship.ChangeMobility(MobilityChange);
            _Ship.ChangeThrust(ThrustChange);
            _Ship.ChangeMaxLinearVelocity(MaxLinearChange);
            _Ship.ChangeMaxAngularVelocity(MaxAngularChange);
        }

        protected override void WearOff()
        {
            _Ship.ChangeMobility(-MobilityChange);
            _Ship.ChangeThrust(-ThrustChange);
            _Ship.ChangeMaxLinearVelocity(-MaxLinearChange);
            _Ship.ChangeMaxAngularVelocity(-MaxAngularChange);
        }
    }
}
