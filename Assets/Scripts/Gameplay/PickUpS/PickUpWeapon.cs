using UnityEngine;

namespace Game
{
    public class PickUpWeapon : PickUp
    {
        [SerializeField] private TurretProperties weapon;

        protected override bool OnPickEffect(SpaceShip ship)
        {
            ship.AssignWeapon(weapon);

            return true;
        }
    }
}
