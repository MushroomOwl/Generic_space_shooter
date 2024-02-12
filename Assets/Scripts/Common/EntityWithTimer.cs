using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EntityWithTimer : MonoBehaviour
    {
        private class SimpleTimer
        {
            public float Current { get; set; }
            public float Initial { get; set; }

            public UnityEvent EventOnEnd;

            public SimpleTimer(float current, float initial)
            {
                Current = current;
                Initial = initial;
                EventOnEnd = new UnityEvent();
            }
        }

        private Dictionary<string, SimpleTimer> _Timers = new Dictionary<string, SimpleTimer>();

        protected float TimeLeft(string timerName)
        {
            if (!_Timers.ContainsKey(timerName))
            {
                return 0;
            }

            return _Timers[timerName].Current;
        }

        public bool AddTimer(string timerName, float time)
        {
            if (_Timers.ContainsKey(timerName))
            {
                return false;
            }

            _Timers.Add(timerName, new SimpleTimer(time, time));

            return true;
        }

        protected bool ResetTimer(string timerName)
        {
            if (!_Timers.ContainsKey(name))
            {
                return false;
            }

            var timer = _Timers[timerName];
            timer.Current = timer.Initial;
            return true;
        }

        public bool RemoveTimer(string timerName)
        {
            if (!_Timers.ContainsKey(timerName))
            {
                return false;
            }

            _Timers[timerName].EventOnEnd.RemoveAllListeners();
            _Timers.Remove(timerName);

            return true;
        }

        public bool AddCallback(string timerName, UnityAction callback)
        {
            if (!_Timers.ContainsKey(timerName))
            {
                return false;
            }

            _Timers[timerName].EventOnEnd?.AddListener(callback);

            return true;
        }

        protected virtual void FixedUpdate()
        {
            // To avoid error when timer keys changed due to event
            List<string> keys = _Timers.Keys.ToList<string>();

            foreach (var timerName in keys)
            {
                if (!_Timers.ContainsKey(timerName))
                {
                    continue;
                }

                var timer = _Timers[timerName];
                timer.Current -= Time.deltaTime;

                if (timer.Current < 0)
                {
                    timer.EventOnEnd?.Invoke();
                    timer.Current = timer.Initial;
                }
            }
        }
    }
}
