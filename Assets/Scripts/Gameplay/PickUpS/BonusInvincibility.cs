using UnityEngine.UI;

namespace Game
{
    public sealed class BonusInvincibility : Bonus
    {
        protected override void ApplyEffect()
        {
            _Ship.MakeIndestructable();
        }

        protected override void WearOff()
        {
            _Ship.MakeDestructable();
        }
    }
}
