using System.Collections.Generic;
using UnityEngine;

public static class CombatTargetSelector
{
    /// <summary>
    /// Quét toàn bộ ô cờ chịu ảnh hưởng dựa trên dữ liệu Patterns (đọc từ DB) của kỹ năng
    /// </summary>
    /// <param name="castTargetCell">Ô cờ được click chọn làm tâm (hoặc ô mục tiêu chỉ định)</param>
    /// <param name="skillPattern">Dữ liệu cấu trúc Pattern bóc từ kỹ năng</param>
    public static List<GridCell> GetCellsInPattern(GridCell castTargetCell, Patterns skillPattern)
    {
        List<GridCell> affectedCells = new List<GridCell>();

        if (castTargetCell == null) return affectedCells;

        // Nếu kỹ năng không cấu hình Pattern (hoặc danh sách ô trống), mặc định chỉ ảnh hưởng ô tâm
        if (skillPattern == null || skillPattern.Cells == null || skillPattern.Cells.Count == 0)
        {
            affectedCells.Add(castTargetCell);
            return affectedCells;
        }

        // Lấy tọa độ Vector2Int của ô cờ thực tế đang được chọn làm tâm trên bàn cờ
        Vector2Int centerGridPos = castTargetCell.GridPosition;

        // Duyệt qua từng ô định dạng Offset (X, Y) được nạp từ Database
        foreach (var patternCell in skillPattern.Cells)
        {
            // Tọa độ ô trên bàn cờ = Tọa độ ô Tâm + Độ lệch (OffsetX/OffsetY) của chiêu thức
            int targetX = centerGridPos.x + patternCell.OffsetX;
            int targetY = centerGridPos.y + patternCell.OffsetY;

            Vector2Int targetGridPos = new Vector2Int(targetX, targetY);

            // Gọi GridManager kiểm tra xem tọa độ này có tồn tại ô cờ thực tế hay không
            GridCell cellOnBoard = GridManager.Instance.GetCellAt(targetX, targetY);

            if (cellOnBoard != null)
            {
                affectedCells.Add(cellOnBoard);
            }
        }

        return affectedCells;
    }
    public static List<CardBase> GetAffectedTargets(string targetId, GridCell casterCell, GridCell castTargetCell, Patterns skillPattern)
    {
        List<CardBase> targetList = new List<CardBase>();

        switch (targetId.ToUpper())
        {
            case "CASTER":
                if (casterCell != null && casterCell.OccupiedCard != null)
                    targetList.Add(casterCell.OccupiedCard);
                break;

            case "CAST_TARGET":
                if (castTargetCell != null && castTargetCell.OccupiedCard != null)
                    targetList.Add(castTargetCell.OccupiedCard);
                break;

            case "PATTERN_ALL":
                // Lấy toàn bộ các ô nằm trong vùng ảnh hưởng cấu hình từ DB
                List<GridCell> allPatternCells = GetCellsInPattern(castTargetCell, skillPattern);
                foreach (var cell in allPatternCells)
                {
                    if (cell.OccupiedCard != null)
                        targetList.Add(cell.OccupiedCard);
                }
                break;

            case "PATTERN_SPLASH":
                // Chỉ lấy các ô phụ xung quanh, bỏ qua ô chính tâm (nơi có OffsetX = 0 và OffsetY = 0 hoặc có IsMain)
                List<GridCell> splashCells = GetCellsInPattern(castTargetCell, skillPattern);
                foreach (var cell in splashCells)
                {
                    // Tìm vị trí tương đối so với ô tâm để biết ô này có phải ô chính hay không
                    int currentOffsetX = cell.GridPosition.x - castTargetCell.GridPosition.x;
                    int currentOffsetY = cell.GridPosition.y - castTargetCell.GridPosition.y;

                    // Nếu trùng đúng vị trí tâm (0,0), bỏ qua không tính sát thương lan (Splash)
                    if (currentOffsetX == 0 && currentOffsetY == 0) continue;

                    if (cell.OccupiedCard != null)
                        targetList.Add(cell.OccupiedCard);
                }
                break;

                // Các logic phe phái ALL_ENEMIES, ALL_ALLIES giữ nguyên...
        }

        return targetList;
    }
}