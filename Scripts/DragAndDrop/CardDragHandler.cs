using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UI;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Texture cardTexture;
    public int team_id;
    public object obj;
    public Transform InTeam;
    private Vector2 originalPosition;
    // Delegate để gọi lại hàm
    public System.Action OnDragEnd; // Callback khi kết thúc kéo

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Lưu vị trí ban đầu khi thẻ được khởi tạo
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Quay lại vị trí ban đầu khi kết thúc kéo
        rectTransform.anchoredPosition = originalPosition;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        InTeam.gameObject.SetActive(true);

        // Gọi lại hàm callback
        OnDragEnd?.Invoke();
    }
    // Phương thức để cập nhật team_id
    public void UpdateTeamId(int newTeamId)
    {
        team_id = newTeamId;
    }
}