using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardBase CardData { get; private set; }
    public int MainPosition { get; private set; } // Vị trí (TeamNumber) cố định của Card này
    public int SubIndex { get; private set; }      // Thứ tự trong danh sách chờ
    public GridCell CurrentCell { get; private set; }

    public void Setup(CardBase data, int mainPos, int subIndex, GridCell cell)
    {
        this.CardData = data;
        this.MainPosition = mainPos;
        this.SubIndex = subIndex;
        this.CurrentCell = cell;

        if (CurrentCell != null)
        {
            // Đăng ký card vào ô và hiển thị MainPosition lên Canvas/PositionText
            CurrentCell.OccupyCell(this);
            CurrentCell.SetTeam(MainPosition);
        }
    }

    // Logic di chuyển 8 hướng
    public void MoveTo(GridCell targetCell)
    {
        if (targetCell == null || !CanMoveTo(targetCell)) return;

        // 1. Dọn dẹp ô cũ
        if (CurrentCell != null)
        {
            CurrentCell.ClearCell();
            CurrentCell.SetTeam(0); // Ẩn số ở ô cũ
        }

        // 2. Cập nhật sang ô mới
        CurrentCell = targetCell;
        CurrentCell.OccupyCell(this);
        CurrentCell.SetTeam(MainPosition); // Hiện MainPosition ở ô mới

        // 3. Cập nhật vị trí thực tế (Visual)
        transform.position = targetCell.transform.position;
    }

    public bool CanMoveTo(GridCell targetCell)
    {
        // 1. Kiểm tra ô đích có tồn tại không
        if (targetCell == null) return false;

        // 2. Kiểm tra ô có bị chặn (vật cản) không
        if (!targetCell.IsWalkable) return false;

        // 3. QUAN TRỌNG: Kiểm tra ô đã có Card nào đứng chưa
        // Nếu CurrentCard != null nghĩa là đã có người chiếm chỗ
        if (targetCell.CurrentCard != null) return false;

        // 4. Kiểm tra khoảng cách 8 hướng (Chebyshev Distance)
        Vector2Int diff = targetCell.GridPos - CurrentCell.GridPos;
        bool isWithinEightDirections = Mathf.Abs(diff.x) <= 1 && Mathf.Abs(diff.y) <= 1;
        bool isNotStandingStill = diff != Vector2Int.zero;

        return isWithinEightDirections && isNotStandingStill;
    }

    public void OnDeath()
    {
        if (CurrentCell != null)
        {
            CurrentCell.ClearCell();
            CurrentCell.SetTeam(0);
        }

        // Khi chết, gọi Manager để lấy Card tiếp theo dựa trên MainPosition 
        // và tăng SubIndex lên (SubIndex + 1)
        GridArenaManager.Instance.SpawnNextCardAtCell(MainPosition, SubIndex + 1);

        Destroy(gameObject);
    }
}