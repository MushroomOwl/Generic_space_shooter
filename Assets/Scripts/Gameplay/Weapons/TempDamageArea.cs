using UnityEngine;

namespace Game
{
    public class TempDamageArea : TempEntity
    {
        [SerializeField] private int _AreaDamage;
        [SerializeField] private Destructable _Parent;

        public void SetParent(Destructable parent)
        {
            _Parent = parent;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            var destructable = collision.GetComponentInParent<Destructable>();

            if (destructable != null)
            {
                destructable.ApplyDamage(_AreaDamage);

                if (_Parent == Player.ActiveShip && destructable.CurrentHitPoints <= 0)
                {
                    if (destructable.CountAsKill) Player.AddKill();
                    Player.AddScore(destructable.ScoreOnKill);
                }
            }
        }
    }
}
