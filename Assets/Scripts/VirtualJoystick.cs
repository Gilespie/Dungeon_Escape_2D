using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _joystick;

    public Vector3 Value { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_background.rectTransform, eventData.position, eventData.pressEventCamera, out position);

        position.x = position.x / _background.rectTransform.sizeDelta.x;
        position.y = position.y / _background.rectTransform.sizeDelta.y;

        position.x = position.x * 2 - 1;
        position.y = position.y * 2 - 1;

        Value = new Vector3(position.x, position.y, 0);

        if (Value.magnitude > 1)
            Value = Value.normalized;

        float offsetX = _background.rectTransform.sizeDelta.x / 2 - _joystick.rectTransform.sizeDelta.x / 2;
        float offsetY = _background.rectTransform.sizeDelta.y / 2 - _joystick.rectTransform.sizeDelta.y / 2;

        _joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value = Vector3.zero;
        _joystick.rectTransform.anchoredPosition = Vector3.zero;
    }
}