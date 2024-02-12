using UnityEngine;

namespace Game
{
    public class LevelWinConditionScore : LevelCondition
    {
        [SerializeField] private int _TargetScore;

        private bool _Fulfilled = false;
        public override bool Fulfilled => _Fulfilled;

        private void Start()
        {
            Player.ScoreChanged.AddListener(CheckFulfillment);
        }

        private void CheckFulfillment()
        {
            bool fulfillment = Player.Score >= _TargetScore;
            if (_Fulfilled != fulfillment)
            {
                _Fulfilled = fulfillment;
                _ConditionValueChanged?.Invoke();
            }
        }
    }
}
