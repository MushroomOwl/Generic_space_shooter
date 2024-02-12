using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Destructable : MonoBehaviour
    {
        private static HashSet<Destructable> _AllDestructables;
        public static IReadOnlyCollection<Destructable> AllDestructables => _AllDestructables;

        [SerializeField] protected bool _Indestructable;
        public bool IsIndestructable => _Indestructable;

        [SerializeField] protected int _MaxHitPoints;
        protected int _CurrentHitPoints;

        public int MaxHitPoints => _MaxHitPoints;
        public int CurrentHitPoints => _CurrentHitPoints;

        protected UnityEvent _EventOnDeath = new UnityEvent();
        protected UnityEvent _EventOnHealth = new UnityEvent();

        public UnityEvent EventOnDeath => _EventOnDeath;
        public UnityEvent EventOnHealth => _EventOnHealth;

        [SerializeField] private int _FractionId;
        public int FractionId => _FractionId;

        [SerializeField] private int _ScoreOnKill;
        public int ScoreOnKill => _ScoreOnKill;

        [SerializeField] private bool _CountAsKill = true;
        public bool CountAsKill => _CountAsKill;

        private void Awake()
        {
            if (_AllDestructables == null)
            {
                _AllDestructables = new HashSet<Destructable>();
            }
            _AllDestructables.Add(this);
        }

        private void OnDestroy()
        {
            _AllDestructables.Remove(this);
        }

        protected virtual void Start()
        {
            _CurrentHitPoints = _MaxHitPoints;
            _EventOnHealth?.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            if (_Indestructable) return;

            _CurrentHitPoints -= damage;
            _EventOnHealth?.Invoke();

            if (_CurrentHitPoints <= 0) OnDeath();
        }

        public void RestoreHealth(int heal)
        {
            if (_Indestructable) return;

            _CurrentHitPoints += heal;
            if (_CurrentHitPoints > _MaxHitPoints) _CurrentHitPoints = _MaxHitPoints;

            _EventOnHealth?.Invoke();
        }

        public void MakeIndestructable()
        {
            _Indestructable = true;
        }

        public void MakeDestructable()
        {
            _Indestructable = false;
        }

        protected virtual void OnDeath()
        {
            _EventOnDeath.AddListener(() => Destroy(gameObject));
            _EventOnDeath.Invoke();
        }
    }
}