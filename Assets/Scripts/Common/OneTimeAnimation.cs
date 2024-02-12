using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class OneTimeAnimation : MonoBehaviour
    {
        private UnityEvent _EventOnEnd = new UnityEvent();
        public UnityEvent EventOnEnd => _EventOnEnd;

        public void EOL()
        {
            _EventOnEnd?.Invoke();
            Destroy(gameObject);
        }
    }
}
