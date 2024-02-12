using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class LevelController : MonoSingleton<LevelController>
    {
        private UnityEvent _LevelFailed = new UnityEvent();
        public UnityEvent LevelFailed => _LevelFailed;

        private UnityEvent _LevelCompleted = new UnityEvent();
        public UnityEvent LevelCompletd => _LevelCompleted;

        [SerializeField] private LevelCondition[] _WinConditions;
        [SerializeField] private LevelCondition[] _LoseConditions;

        [SerializeField] private LevelProperties _LevelProps;
        public static LevelProperties LevelProps => _Instance?._LevelProps;

        private float _TimeSinceStart = 0f;
        public static float TimeSinceStart => _Instance._TimeSinceStart;

        private bool _isLevelCompleted = false;
        public bool IsLevelCompleted => _isLevelCompleted;

        private bool _isLevelLost = false;
        public bool isLevelLost => _isLevelLost;

        private void Start()
        {
            foreach (var condition in _WinConditions)
            {
                condition.ConditionValueChanged.AddListener(CheckLevelCompletion);
            }
            foreach (var condition in _LoseConditions)
            {
                condition.ConditionValueChanged.AddListener(CheckLevelLosing);
            }
            _LevelCompleted.AddListener(GameManager.FinishLevel);
            _LevelFailed.AddListener(GameManager.FailLevel);
        }

        private void CheckLevelCompletion()
        {
            bool isCompleted = _WinConditions.All(c => c.Fulfilled);

            if (isCompleted && isCompleted != _isLevelCompleted)
            {
                _isLevelCompleted = true;
                _LevelCompleted?.Invoke();
            }
        }

        private void CheckLevelLosing()
        {
            bool isLost = _LoseConditions.Any(c => c.Fulfilled);

            if (isLost && isLost != _isLevelLost)
            {
                _isLevelLost = true;
                _LevelFailed?.Invoke();
            }
        }

        private void Update()
        {
            if (_isLevelCompleted || _isLevelLost)
            {
                return;
            }

            _TimeSinceStart += Time.deltaTime;
        }
    }
}