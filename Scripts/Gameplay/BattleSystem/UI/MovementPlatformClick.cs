using UnityEngine;
using UnityEngine.EventSystems;

public class MovementPlatformClick : MonoBehaviour, IPointerClickHandler
{
    private GridCell parentCell;

    void Awake()
    {
        // Lấy GridCell ở cha để truyền dữ liệu ngược lên khi bị click
        parentCell = GetComponentInParent<GridCell>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Khi GameObject này activeSelf = true và có Collider, EventSystem mới nhận diện được cú click
        if (parentCell != null)
        {
            Debug.Log($"Click trực tiếp trúng nền di chuyển của ô {parentCell.GridPosition}");
            GridManager.Instance.HandleCellClick(parentCell);
        }
    }
}