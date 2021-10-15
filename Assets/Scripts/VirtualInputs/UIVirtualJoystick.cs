using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIVirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Rect References")]
    [SerializeField] private RectTransform containerRect;
    [SerializeField] private RectTransform handleRect;

    [Header("Settings")]
    [SerializeField] private float joystickRange = 50f;
    [SerializeField] private float magnitudeMutiplier = 1f;
    [SerializeField] private bool invertXOutputValue;
    [SerializeField] private bool invertYOutputValue;

    [Header("Output")]
    public UnityEvent<Vector2> joystickOutputEvent;


    private void Start()
    {
        SetupHandle();
        
    }

    private void SetupHandle()
    {
        if (handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera,
            out Vector2 position);

        position = ApplySizeDelta(position);

        Vector2 clampedPosition = ClampValuesToManitude(position);

        Vector2 outputPosition = ApplyInversionFilter(position);

        OutputPointerEventValue(clampedPosition * magnitudeMutiplier);

        if (handleRect)
        {
            UpdateHandleRectPosition(clampedPosition * joystickRange);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OutputPointerEventValue(Vector2.zero);

        if (handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    private void OutputPointerEventValue(Vector2 vector2)
    {
        joystickOutputEvent.Invoke(vector2);
    }

    private void UpdateHandleRectPosition(Vector2 newPosition)
    {
        handleRect.anchoredPosition = newPosition;
    }

    private Vector2 ApplySizeDelta(Vector2 position)
    {
        float x = (position.x / containerRect.sizeDelta.x) * 2.5f;
        float y = (position.y / containerRect.sizeDelta.y) * 2.5f;

        return new Vector2(x, y);
    }

    private Vector2 ClampValuesToManitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }

    private Vector2 ApplyInversionFilter(Vector2 position)
    {
        if (invertXOutputValue)
            position.x = InvertValue(position.x);

        if (invertYOutputValue)
            position.y = InvertValue(position.y);

        return position;
    }

    private float InvertValue(float value)
    {
        return -value;
    }
}
