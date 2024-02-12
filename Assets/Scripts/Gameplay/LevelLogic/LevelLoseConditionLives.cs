using UnityEngine;

namespace Game
{
    public class LevelLoseConditionKills : LevelCondition
    {
        private bool _Fulfilled = false;
        public override bool Fulfilled => _Fulfilled;

        private void Start()
        {
            Player.LivesChanged.AddListener(CheckFulfillment);
        }

        private void CheckFulfillment()
        {
            bool fulfillment = Player.NumLives <= 0;
            if (_Fulfilled != fulfillment)
            {
                _Fulfilled = fulfillment;
                _ConditionValueChanged?.Invoke();
            }
        }
    }
}