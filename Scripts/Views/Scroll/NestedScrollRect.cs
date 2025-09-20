using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NestedScrollRect : ScrollRect
{
    private ScrollRect parentScroll;
    private bool routeToParent = false;

    protected override void Start()
    {
        base.Start();
        parentScroll = FindParentScrollRect();
    }

    private ScrollRect FindParentScrollRect()
    {
        Transform t = transform.parent;
        while (t != null)
        {
            ScrollRect sr = t.GetComponent<ScrollRect>();
            if (sr != null && sr != this) return sr;
            t = t.parent;
        }
        return null;
    }

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        base.OnInitializePotentialDrag(eventData);
        if (parentScroll != null) parentScroll.OnInitializePotentialDrag(eventData);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Quyết định sẽ route sang parent hay do child xử lý dựa trên hướng drag ban đầu
        if (parentScroll != null && Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            routeToParent = true;
            parentScroll.OnBeginDrag(eventData);
            // Debug.Log($"BeginDrag -> parent ({parentScroll.name})");
        }
        else
        {
            routeToParent = false;
            base.OnBeginDrag(eventData);
            // Debug.Log("BeginDrag -> child");
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (routeToParent && parentScroll != null)
            parentScroll.OnDrag(eventData);
        else
            base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (routeToParent && parentScroll != null)
            parentScroll.OnEndDrag(eventData);
        else
            base.OnEndDrag(eventData);

        routeToParent = false;
    }

    public override void OnScroll(PointerEventData data)
    {
        bool verticalScroll = Mathf.Abs(data.scrollDelta.y) > Mathf.Abs(data.scrollDelta.x);

        if (verticalScroll && vertical)
        {
            // Nếu con còn có thể di chuyển theo chiều dọc thì con xử lý,
            // nếu đã đến biên (top/bottom) thì truyền cho parent để parent cuộn ngang
            float eps = 0.01f;
            bool atTop = verticalNormalizedPosition >= 1f - eps;
            bool atBottom = verticalNormalizedPosition <= eps;

            if ((data.scrollDelta.y > 0 && atTop) || (data.scrollDelta.y < 0 && atBottom))
            {
                if (parentScroll != null) parentScroll.OnScroll(data);
                else base.OnScroll(data);
            }
            else
            {
                base.OnScroll(data);
            }
        }
        else
        {
            if (parentScroll != null) parentScroll.OnScroll(data);
            else base.OnScroll(data);
        }
    }
}
