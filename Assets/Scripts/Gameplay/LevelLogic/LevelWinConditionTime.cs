using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(EntityWithTimer))]
    public class LevelWinConditionTime : LevelCondition
    {
        [SerializeField] private EntityWithTimer _TimerHandler;
        [SerializeField] private int _TimeTillEnd;

        private const string _WinConditionTimer = "winconditiontimer";

        private bool _Fulfilled = false;
        public override bool Fulfilled => _Fulfilled;

        private void Start()
        {
            _TimerHandler = GetComponent<EntityWithTimer>();
            _TimerHandler.AddTimer(_WinConditionTimer, _TimeTillEnd);
            _TimerHandler.AddCallback(_WinConditionTimer, () =>
            {
                _Fulfilled = true;
                _TimerHandler.RemoveTimer(_WinConditionTimer);
                _ConditionValueChanged?.Invoke();
            });
        }
    }
}