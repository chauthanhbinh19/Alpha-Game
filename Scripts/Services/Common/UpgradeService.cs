using System.Collections.Generic;
using System.Linq;

public static class UpgradeService
{
    private static List<Items> GetCostForLevel(int currentLevel, Recipe recipe)
    {
        return recipe.ItemRecipes
            .First(r => currentLevel >= r.MinLevel && currentLevel <= r.MaxLevel)
            .Items;
    }

    private static bool CanPayById(Inventory inventory, List<Items> items)
    {
        foreach (var item in items)
            if (inventory.GetQuantityById(item.Id) < item.Quantity)
                return false;
        return true;
    }

    private static bool CanPayByName(Inventory inventory, List<Items> items)
    {
        foreach (var item in items)
            if (inventory.GetQuantityByName(item.Name) < item.Quantity)
                return false;
        return true;
    }

    private static void PayById(Inventory inventory, List<Items> items)
    {
        foreach (var item in items)
            inventory.RemoveById(item.Id, item.Quantity);
    }

    private static void PayByName(Inventory inventory, List<Items> items)
    {
        foreach (var item in items)
            inventory.RemoveByName(item.Name, item.Quantity);
    }

    // 🔹 Up 1 level
    public static bool UpgradeOne(Inventory inventory, ref int level, Recipe recipe)
    {
        if (level >= recipe.MaxLevel)
            return false;

        var costs = GetCostForLevel(level, recipe);

        if (!CanPayByName(inventory, costs))
            return false;

        PayByName(inventory, costs);
        level++;
        return true;
    }

    // 🔹 Up bulk (max)
    public static int UpgradeMax(Inventory inventory, ref int level, Recipe recipe)
    {
        int upgraded = 0;

        while (level < recipe.MaxLevel)
        {
            var costs = GetCostForLevel(level, recipe);

            if (!CanPayByName(inventory, costs))
                break;

            PayByName(inventory, costs);
            level++;
            upgraded++;
        }

        return upgraded;
    }

    // 🔹 Preview bulk (không trừ item thật)
    public static int PreviewUpgradeMax(Inventory inventory, int currentLevel, Recipe recipe)
    {
        var tempInv = inventory.Clone();
        int tempLevel = currentLevel;
        int upgraded = 0;

        while (tempLevel < recipe.MaxLevel)
        {
            var costs = GetCostForLevel(tempLevel, recipe);

            if (!CanPayById(tempInv, costs))
                break;

            PayByName(tempInv, costs);
            tempLevel++;
            upgraded++;
        }

        return upgraded;
    }
}
