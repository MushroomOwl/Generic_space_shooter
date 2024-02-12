using UnityEngine;

namespace Game
{
    public class LevelWinConditionKills : LevelCondition
    {
        [SerializeField] private int _TargetKills;

        private bool _Fulfilled = false;
        public override bool Fulfilled => _Fulfilled;

        private void Start()
        {
            Player.KillsChanged.AddListener(CheckFulfillment);
        }

        private void CheckFulfillment()
        {
            bool fulfillment = Player.KillCount >= _TargetKills;
            if (_Fulfilled != fulfillment)
            {
                _Fulfilled = fulfillment;
                _ConditionValueChanged?.Invoke();
            }
        }
    }
}