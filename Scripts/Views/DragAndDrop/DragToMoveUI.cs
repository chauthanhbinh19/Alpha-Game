using UnityEngine;
using UnityEngine.EventSystems;

public class DragToMoveUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 LastPosition;
    private RectTransform RectTransform;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            RectTransform, eventData.position, eventData.pressEventCamera, out LastPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            RectTransform, eventData.position, eventData.pressEventCamera, out currentPosition);

        Vector2 delta = currentPosition - LastPosition;
        RectTransform.anchoredPosition += delta;
    }
}
