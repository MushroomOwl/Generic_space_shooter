using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class Bonus : EntityWithTimer
    {
        [SerializeField] protected SpaceShip _Ship;
        [SerializeField] private float TTL = 1.0f;
        [SerializeField] protected Sprite _Icon;

        private static string _TTLTimerName = "ttl";

        private UnityEvent _OnDestroy = new UnityEvent();
        public UnityEvent OnDestroy => _OnDestroy;

        public Sprite Icon => _Icon;

        public float TimeLeftPercent => TimeLeft(_TTLTimerName) / TTL * 100;

        public virtual void Init(SpaceShip ship, float initTTLs, Sprite icon)
        {
            _Ship = ship;
            TTL = initTTLs == 0 ? 1.0f : initTTLs;
            _Icon = icon;

            AddTimer(_TTLTimerName, TTL);
            AddCallback(_TTLTimerName, DisposeActiveBonus);

            if (_Ship == null)
            {
                OnDestroy.AddListener(() => Destroy(gameObject));
                OnDestroy.Invoke();
            }
            else
            {
                _Ship.EventOnDeath.AddListener(Dispose);
            }
        }

        private void Start()
        {
            if (_Ship == null) Dispose();
            ApplyEffect();
        }

        public void RefreshDuration()
        {
            ResetTimer(_TTLTimerName);
        }

        protected abstract void ApplyEffect();
        protected abstract void WearOff();

        private void DisposeActiveBonus()
        {
            WearOff();
            Dispose();
        }

        private void Dispose()
        {
            OnDestroy.AddListener(() => Destroy(gameObject));
            OnDestroy.Invoke();
        }
    }
}