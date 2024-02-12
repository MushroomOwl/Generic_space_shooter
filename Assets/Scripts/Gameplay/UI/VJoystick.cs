using UnityEngine;
using UnityEngine.EventSystems;
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

            position.x = 2 * position.x / _Back.rectTransform.sizeDelta.x - 1;
            position.y = 2 * position.y / _Back.rectTransform.sizeDelta.y - 1;

            Value = new Vector3(position.x, position.y, 0);

            if (Value.magnitude > 1) Value = Value.normalized;

            float coefx = (_Back.rectTransform.sizeDelta.x - _Stick.rectTransform.sizeDelta.x) / 2;
            float coefy = (_Back.rectTransform.sizeDelta.y - _Stick.rectTransform.sizeDelta.y) / 2;

            _Stick.rectTransform.anchoredPosition = new Vector2(Value.x * coefx, Value.y * coefy); ;
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
