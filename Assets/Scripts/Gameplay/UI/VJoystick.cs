using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Game
{
    public class VJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _Back;
        [SerializeField] private Image _Stick;

        public Vector3 Value { get; private set; }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_Back.rectTransform, eventData.position, eventData.pressEventCamera, out position);

            // NOTE: Assuming that stick area is circle
            float maxRadius = (_Back.rectTransform.sizeDelta.x - _Stick.rectTransform.sizeDelta.x) / 2;

            if (position.magnitude > maxRadius)
            {
                position = position.normalized * maxRadius;
            }

            Value = position.normalized;

            _Stick.rectTransform.anchoredPosition = position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            _Stick.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
