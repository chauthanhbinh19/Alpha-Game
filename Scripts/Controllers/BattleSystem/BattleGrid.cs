using System.Collections.Generic;
using System.Linq;

public enum BattleCellType
{
    None,
    Player,
    Enemy
}

[System.Serializable]
public class BattleCell
{
    public int Position { get; set; }
    public BattleCellType Owner { get; set; }
    public CardModel Occupant { get; set; }

    public int Row => Position >= 0 ? Position / BattleGrid.Columns : -1;
    public int Column => Position >= 0 ? Position % BattleGrid.Columns : -1;

    public bool IsEmpty => Occupant == null;
}

public class BattleGrid
{
    public const int Rows = 7;
    public const int Columns = 7;
    public const int CellCount = Rows * Columns;

    public List<BattleCell> Cells { get; } = new List<BattleCell>(CellCount);

    public BattleGrid()
    {
        InitializeGrid();
    }

    public void InitializeGrid(BattleCellType defaultOwner = BattleCellType.None)
    {
        Cells.Clear();

        for (int index = 0; index < CellCount; index++)
        {
            Cells.Add(new BattleCell
            {
                Position = index,
                Owner = defaultOwner,
                Occupant = null
            });
        }
    }

    public BattleCell GetCell(int position)
    {
        if (position < 0 || position >= CellCount)
            return null;

        return Cells[position];
    }

    public bool AssignCard(CardModel card, int position, BattleCellType owner)
    {
        var cell = GetCell(position);
        if (cell == null || !cell.IsEmpty || card == null)
            return false;

        cell.Owner = owner;
        cell.Occupant = card;
        card.CellOwner = owner;
        card.CellPosition = position;
        return true;
    }

    public IEnumerable<BattleCell> GetCellsByOwner(BattleCellType owner)
    {
        return Cells.Where(cell => cell.Owner == owner);
    }
}
