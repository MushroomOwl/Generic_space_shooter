using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _Hold;
        public bool IsHold => _Hold;

        void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            _Hold = true;
        }

        void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
        {
            _Hold = false;
        }
    }
}
