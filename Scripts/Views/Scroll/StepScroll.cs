using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class StepScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private int totalPanels = 7;
    [SerializeField] private float lerpSpeed = 10f;

    private int currentIndex = 0;
    private float targetVerticalPos = 1f; // 1 là trên cùng, 0 là dưới cùng
    private Coroutine smoothCoroutine;
    private float startDragPos;
    private float lastScrollTime;

    void Update()
    {
        // Handle mouse scroll globally with debounce to allow light scrolls
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0 && Time.time - lastScrollTime > 0.1f)
        {
            if (scroll > 0) // Scroll up
            {
                MoveToPanel(currentIndex - 1);
            }
            else // Scroll down
            {
                MoveToPanel(currentIndex + 1);
            }
            lastScrollTime = Time.time;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragPos = scrollRect.verticalNormalizedPosition;
        if (smoothCoroutine != null) StopCoroutine(smoothCoroutine);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float endPos = scrollRect.verticalNormalizedPosition;
        float delta = startDragPos - endPos;
        float threshold = 0.02f; // Ngưỡng nhỏ để drag nhẹ chuyển

        if (Mathf.Abs(delta) > threshold)
        {
            if (delta > 0) // Kéo xuống (p giảm), chuyển sang panel tiếp theo
            {
                MoveToPanel(currentIndex + 1);
            }
            else // Kéo lên (p tăng), chuyển sang panel trước
            {
                MoveToPanel(currentIndex - 1);
            }
        }
        else
        {
            // Drag quá nhẹ, snap về panel hiện tại
            MoveToPanel(currentIndex);
        }
    }

    private void MoveToPanel(int index)
    {
        if (index < 0 || index >= totalPanels) return;

        currentIndex = index;
        // Tính toán vị trí Normalized (từ 0 đến 1)
        // Với Vertical: Panel 0 ở 1.0, Panel cuối ở 0.0
        targetVerticalPos = 1f - ((float)currentIndex / (totalPanels - 1));
        if (smoothCoroutine != null) StopCoroutine(smoothCoroutine);
        smoothCoroutine = StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove()
    {
        float startPos = scrollRect.verticalNormalizedPosition;
        float duration = 0.3f;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(startPos, targetVerticalPos, time / duration);
            yield return null;
        }
        scrollRect.verticalNormalizedPosition = targetVerticalPos;
    }
}