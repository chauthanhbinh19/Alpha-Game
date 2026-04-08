using UnityEngine;

public class CardController : MonoBehaviour 
{
    public CardBase CardData { get; private set; }
    public int QueueIndex { get; private set; }
    public GridCell CurrentCell { get; private set; }

    public void Setup(CardBase data, int queueIndex, GridCell cell)
    {
        this.CardData = data;
        this.QueueIndex = queueIndex;
        this.CurrentCell = cell;

        // Đăng ký sự kiện chết từ CardBase (nếu bạn có event OnDie trong CardBase)
        // Hoặc kiểm tra HP trong Update
    }

    // Hàm này gọi khi CardBase.CurrentHealth <= 0
    public void OnDeath()
    {
        // 1. Giải phóng ô Grid hiện tại
        CurrentCell.SetType(CellType.Empty);

        // 2. Yêu cầu GridArenaManager thay thế card mới vào đúng QueueIndex này
        GridArenaManager.Instance.SpawnNextCardAtCell(QueueIndex);

        // 3. Xóa prefab hiện tại
        Destroy(gameObject);
    }
}