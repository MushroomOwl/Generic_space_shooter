using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ColliderTriggerEvent : MonoBehaviour
    {
        [SerializeField] protected UnityEvent _TriggerEnterEvent;
        public UnityEvent TriggerEnterEvent => _TriggerEnterEvent;

        [SerializeField] protected UnityEvent _TriggerExitEvent;
        public UnityEvent TriggerExitEvent => _TriggerExitEvent;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Destructable destructable = collision.GetComponentInParent<Destructable>();
            if (destructable == Player.ActiveShip)
            {
                _TriggerEnterEvent?.Invoke();
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Destructable destructable = collision.GetComponentInParent<Destructable>();
            if (destructable == Player.ActiveShip)
            {
                _TriggerExitEvent?.Invoke();
            }
        }
    }
}
