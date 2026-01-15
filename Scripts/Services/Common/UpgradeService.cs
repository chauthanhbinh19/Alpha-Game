using System.Collections.Generic;
using System.Linq;

public static class UpgradeService
{
    private static List<Items> GetCostForLevel(int currentLevel, Recipe config)
    {
        return config.ItemRecipes
            .First(r => currentLevel >= r.MinLevel && currentLevel <= r.MaxLevel)
            .Items;
    }

    private static bool CanPayById(Inventory inv, List<Items> costs)
    {
        foreach (var c in costs)
            if (inv.GetQuantityById(c.Id) < c.Quantity)
                return false;
        return true;
    }

    private static bool CanPayByName(Inventory inv, List<Items> costs)
    {
        foreach (var c in costs)
            if (inv.GetQuantityByName(c.Name) < c.Quantity)
                return false;
        return true;
    }

    private static void PayById(Inventory inv, List<Items> costs)
    {
        foreach (var c in costs)
            inv.RemoveById(c.Id, c.Quantity);
    }

    private static void PayByName(Inventory inv, List<Items> costs)
    {
        foreach (var c in costs)
            inv.RemoveByName(c.Name, c.Quantity);
    }

    // 🔹 Up 1 level
    public static bool UpgradeOne(Inventory inv, ref int level, Recipe config)
    {
        if (level >= config.MaxLevel)
            return false;

        var costs = GetCostForLevel(level, config);

        if (!CanPayByName(inv, costs))
            return false;

        PayByName(inv, costs);
        level++;
        return true;
    }

    // 🔹 Up bulk (max)
    public static int UpgradeMax(Inventory inv, ref int level, Recipe config)
    {
        int upgraded = 0;

        while (level < config.MaxLevel)
        {
            var costs = GetCostForLevel(level, config);

            if (!CanPayByName(inv, costs))
                break;

            PayByName(inv, costs);
            level++;
            upgraded++;
        }

        return upgraded;
    }

    // 🔹 Preview bulk (không trừ item thật)
    public static int PreviewUpgradeMax(Inventory inv, int currentLevel, Recipe config)
    {
        var tempInv = inv.Clone();
        int tempLevel = currentLevel;
        int upgraded = 0;

        while (tempLevel < config.MaxLevel)
        {
            var costs = GetCostForLevel(tempLevel, config);

            if (!CanPayById(tempInv, costs))
                break;

            PayByName(tempInv, costs);
            tempLevel++;
            upgraded++;
        }

        return upgraded;
    }
}
