using UnityEngine;

namespace Game
{
    public class PickUpStat : PickUp
    {
        public enum StatPickUpType
        {
            Health,
            Energy,
            Ammo
        }

        [SerializeField] private StatPickUpType _PType;
        [SerializeField] private int _Amount;

        protected override bool OnPickEffect(SpaceShip ship)
        {
            switch (_PType)
            {
                case StatPickUpType.Health:
                    ship.RestoreHealth(_Amount);
                    break;
                case StatPickUpType.Energy:
                    ship.RestoreEnergy(_Amount);
                    break;
                case StatPickUpType.Ammo:
                    ship.RestoreAmmo(_Amount);
                    break;
            }

            return true;
        }
    }
}
