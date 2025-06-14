using UnityEngine;
using UnityEngine.EventSystems;

public class DragToMoveUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 lastPosition;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out lastPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out currentPosition);

        Vector2 delta = currentPosition - lastPosition;
        rectTransform.anchoredPosition += delta;
    }
}
