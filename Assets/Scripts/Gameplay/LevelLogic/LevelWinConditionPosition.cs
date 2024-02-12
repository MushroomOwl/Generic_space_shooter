using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelWinConditionPosition : LevelCondition
    {
        [SerializeField] private Collider2D coll;
        private bool _Fulfilled = false;

        public override bool Fulfilled => _Fulfilled;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destructable destructable = collision.GetComponentInParent<Destructable>();
            if (destructable == Player.ActiveShip)
            {
                _Fulfilled = true;
                _ConditionValueChanged?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Destructable destructable = collision.GetComponentInParent<Destructable>();
            if (destructable == Player.ActiveShip)
            {
                _Fulfilled = false;
                _ConditionValueChanged?.Invoke();
            }
        }
    }
}
