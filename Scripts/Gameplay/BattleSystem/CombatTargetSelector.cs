using System.Collections.Generic;
using UnityEngine;

public static class CombatTargetSelector
{
    public static List<GridCell> GetCellsInPattern(GridCell castTargetCell, Patterns skillPattern)
    {
        List<GridCell> affectedCells = new List<GridCell>();
        if (castTargetCell == null) return affectedCells;

        if (skillPattern == null || skillPattern.Cells == null || skillPattern.Cells.Count == 0)
        {
            affectedCells.Add(castTargetCell);
            return affectedCells;
        }

        if (GridManager.Instance == null) return affectedCells;

        HashSet<GridCell> uniqueCells = new HashSet<GridCell>();
        foreach (var patternCell in skillPattern.Cells)
        {
            GridCell cell = GridManager.Instance.GetCellAt(
                castTargetCell.GridPosition.x + patternCell.OffsetX,
                castTargetCell.GridPosition.y + patternCell.OffsetY);

            if (cell != null && uniqueCells.Add(cell))
            {
                affectedCells.Add(cell);
            }
        }

        return affectedCells;
    }

    public static List<CardBase> GetAffectedTargets(string targetId, GridCell casterCell, GridCell castTargetCell, Patterns skillPattern)
    {
        List<CardBase> targets = new List<CardBase>();
        HashSet<CardBase> uniqueTargets = new HashSet<CardBase>();
        string normalizedTargetId = targetId != null ? targetId.Trim().ToUpperInvariant() : string.Empty;

        switch (normalizedTargetId)
        {
            case AppConstants.Target.CASTER:
            case AppConstants.Target.SELF:
                AddTarget(casterCell != null ? casterCell.OccupiedCard : null, targets, uniqueTargets);
                break;

            case "CAST_TARGET":
            case AppConstants.Target.PATTERN_MAIN:
                AddTarget(castTargetCell != null ? castTargetCell.OccupiedCard : null, targets, uniqueTargets);
                break;

            case AppConstants.Target.PATTERN_ALL:
                AddPatternTargets(GetCellsInPattern(castTargetCell, skillPattern), targets, uniqueTargets, castTargetCell, false);
                break;

            case AppConstants.Target.PATTERN_SPLASH:
                AddPatternTargets(GetCellsInPattern(castTargetCell, skillPattern), targets, uniqueTargets, castTargetCell, true);
                break;

            case AppConstants.Target.ALLY:
            case AppConstants.Target.ENEMY:
                AddTeamTargets(normalizedTargetId, casterCell, targets, uniqueTargets);
                break;

            default:
                Debug.LogWarning($"[CombatTargetSelector] Unsupported or empty target id '{targetId}'.");
                break;
        }

        return targets;
    }

    private static void AddPatternTargets(
        IEnumerable<GridCell> cells,
        List<CardBase> targets,
        HashSet<CardBase> uniqueTargets,
        GridCell centerCell,
        bool excludeCenter)
    {
        foreach (GridCell cell in cells)
        {
            if (cell == null || (excludeCenter && cell == centerCell)) continue;
            AddTarget(cell.OccupiedCard, targets, uniqueTargets);
        }
    }

    private static void AddTeamTargets(
        string targetId,
        GridCell casterCell,
        List<CardBase> targets,
        HashSet<CardBase> uniqueTargets)
    {
        CardBase caster = casterCell != null ? casterCell.OccupiedCard : null;
        if (caster == null || TurnManager.Instance == null || TurnManager.Instance.AllCards == null) return;

        bool targetAllies = targetId == AppConstants.Target.ALLY;
        foreach (CardBase card in TurnManager.Instance.AllCards)
        {
            if (card == null || card == caster || !card.IsAlive) continue;

            bool isSameTeam = card.Team == caster.Team;
            if (isSameTeam == targetAllies)
            {
                AddTarget(card, targets, uniqueTargets);
            }
        }
    }

    private static void AddTarget(CardBase target, List<CardBase> targets, HashSet<CardBase> uniqueTargets)
    {
        if (target != null && target.IsAlive && uniqueTargets.Add(target))
        {
            targets.Add(target);
        }
    }
}
