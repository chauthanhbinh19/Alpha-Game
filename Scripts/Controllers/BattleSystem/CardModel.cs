using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class CardModel : CardBase
{
    public BattleCellType CellOwner { get; set; } = BattleCellType.None;
    public int CellPosition { get; set; } = -1;

    public int Row => CellPosition >= 0 ? CellPosition / BattleGrid.Columns : -1;
    public int Column => CellPosition >= 0 ? CellPosition % BattleGrid.Columns : -1;

    public static CardModel CreateFrom(CardBase source)
    {
        if (source == null)
            return null;

        var model = new CardModel();
        CopyProperties(source, model);
        return model;
    }

    protected override CardBase ChooseTarget(PlayerController opponent)
    {
        if (opponent == null)
            return null;

        var targets = opponent.GetCards()
            .Where(card => card != null && card.IsAlive)
            .ToList();

        if (targets.Count == 0)
            return null;

        if (CellPosition < 0)
            return targets.First();

        var sameRowTargets = targets.Where(card => card.Row == Row).ToList();
        if (sameRowTargets.Count > 0)
            return ChooseClosestByColumn(sameRowTargets);

        var nearestRowTargets = targets
            .OrderBy(card => Math.Abs(card.Row - Row))
            .ThenBy(card => Math.Abs(card.Column - Column))
            .ThenBy(card => card.CurrentHealth)
            .ToList();

        return nearestRowTargets.First();
    }

    private CardBase ChooseClosestByColumn(IEnumerable<CardModel> targets)
    {
        return targets
            .OrderBy(card => Math.Abs(card.Column - Column))
            .ThenBy(card => card.CurrentHealth)
            .FirstOrDefault();
    }

    private static void CopyProperties(CardBase source, CardModel target)
    {
        var type = typeof(CardBase);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (!property.CanRead || !property.CanWrite)
                continue;

            var value = property.GetValue(source);
            property.SetValue(target, value);
        }
    }
}
