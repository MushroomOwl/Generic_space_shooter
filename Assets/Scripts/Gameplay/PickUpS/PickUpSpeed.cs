using UnityEngine;

namespace Game
{
    public class PickUpSpeed : PickUp
    {
        [SerializeField] private BonusUI _BonusUIprefab;
        [SerializeField] private Bonus _BonusPrefab;
        [SerializeField] private float _DurationS;
        [SerializeField] private Sprite _BonusIcon;

        protected override bool OnPickEffect(SpaceShip ship)
        {
            Bonus b = ship.BonusList.Find(v => v.GetType() == typeof(BonusSpeed));

            if (b != null)
            {
                b.RefreshDuration();
                return true;
            }

            GameObject panel = GameObject.Find("BonusAnchor");
            if (!panel)
            {
                return false;
            }

            b = Instantiate(_BonusPrefab);
            b.Init(ship, _DurationS, _BonusIcon);
            ship.BonusList.Add(b);

            BonusUI bui = Instantiate(_BonusUIprefab);
            bui.Init(b, panel);

            return true;
        }
    }
}
