using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class TimeTriger : EntityWithTimer
    {
        [SerializeField] private float _Time;
        private const string _TriggerTimerName = "triggertimer";

        [SerializeField] private UnityEvent _TriggerEnd;
        public UnityEvent TriggerEnd => _TriggerEnd;

        private void Start()
        {
            AddTimer(_TriggerTimerName, _Time);
            AddCallback(_TriggerTimerName, () =>
            {
                _TriggerEnd?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}