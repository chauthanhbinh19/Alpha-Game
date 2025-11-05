using System;
using System.Collections.Generic;
using System.Linq;
public static class EvaluateItem
{
    public static int CalculateMaxMaterialQuantity(int materialQuantity, int currentLevel)
    {
        int levelsPerSkill = 1000;
        int maxLevel = 10000; // Giới hạn level tối đa

        int totalMaterialUsed = 0;
        int levelsGained = 0;

        for (int level = currentLevel; level < maxLevel; level++)
        {
            int requiredMaterial = level % levelsPerSkill;

            // Nếu level = 0, thì cần 1 material để lên cấp
            if (level == 0)
                requiredMaterial = 1;
            else if (requiredMaterial == 0)
                requiredMaterial = levelsPerSkill; // Đảm bảo level bội số vẫn cần 500 material

            if (totalMaterialUsed + requiredMaterial > materialQuantity)
                break; // Dừng nếu không đủ material để lên level tiếp theo

            totalMaterialUsed += requiredMaterial;
            levelsGained++;
        }

        return totalMaterialUsed; // Tổng số material có thể sử dụng
    }
    public static int CalculateMaxMaterialLevel(double materialQuantity, int currentLevel)
    {
        int levelsPerSkill = 1000;
        int maxLevel = 10000; // Giới hạn level tối đa

        int totalMaterialUsed = 0;
        int levelsGained = 0;

        for (int level = currentLevel; level < maxLevel; level++)
        {
            int requiredMaterial = level % levelsPerSkill;

            // Nếu level = 0, thì cần 1 material để lên cấp
            if (level == 0)
                requiredMaterial = 1;
            else if (requiredMaterial == 0)
                requiredMaterial = levelsPerSkill; // Đảm bảo level bội số vẫn cần 500 material

            if (totalMaterialUsed + requiredMaterial > materialQuantity)
                break; // Dừng nếu không đủ material để lên level tiếp theo

            totalMaterialUsed += requiredMaterial;
            levelsGained++;
        }

        return levelsGained; // Tổng số material có thể sử dụng
    }
    // Trả về tổng số lượng item cần để nâng từ rankLevel hiện tại lên thêm "targetLevel" level
    public static int CalculateRequiredQuantityForLevel(int currentLevel, int targetLevel, int levelsPerSkill)
    {
        int total = 0;
        for (int l = 1; l <= targetLevel; l++)
        {
            int materialQuantity = (currentLevel == 0)
                ? 1
                : ((currentLevel + l - 1) % levelsPerSkill == 0
                    ? levelsPerSkill
                    : (currentLevel + l - 1) % levelsPerSkill);

            total += materialQuantity;
        }
        return total;
    }
    public static (double materialLeft, double currencyLeft, int levelsGained, int totalMaterialUsed, int totalCurrencyUsed, string message)
    CalculateLevelUp(double itemQuantity, double currencyQuantity, int materialPerLevel, int currencyPerLevel, int currentLevel, bool isMaxLevel, int maxLevel)
    {
        double materialLeft = itemQuantity;
        double currencyLeft = currencyQuantity;
        int levelsGained = 0;
        int totalMaterialUsed = 0;
        int totalCurrencyUsed = 0;

        // Nếu đã đạt max level
        if (currentLevel >= maxLevel)
        {
            return (materialLeft, currencyLeft, 0, 0, 0, AppConstants.Status.MAX_LEVEL);
        }

        // Nếu chỉ up 1 level
        if (!isMaxLevel)
        {
            if (currentLevel + 1 > maxLevel)
                return (materialLeft, currencyLeft, 0, 0, 0, AppConstants.Status.MAX_LEVEL);
            int targetLevel = currentLevel + 1;

            // đảm bảo requiredMaterial tối thiểu là 1 (không được là 0)
            int requiredMaterial = Math.Max(1, targetLevel * materialPerLevel);
            int requiredCurrency = Math.Max(0, targetLevel * currencyPerLevel);

            totalMaterialUsed += requiredMaterial;
            totalCurrencyUsed += requiredCurrency;

            if (materialLeft >= requiredMaterial && currencyLeft >= requiredCurrency)
            {
                materialLeft -= requiredMaterial;
                currencyLeft -= requiredCurrency;
                levelsGained = 1;
            }
            else
            {
                return (materialLeft, currencyLeft, 0, totalMaterialUsed, totalCurrencyUsed, AppConstants.Status.NOT_ENOUGH_RESOURCE);
            }

            return (materialLeft, currencyLeft, levelsGained, totalMaterialUsed, totalCurrencyUsed, AppConstants.Status.SUCCESS);
        }

        // Nếu up max có thể
        int tempLevel = currentLevel;
        while (tempLevel < maxLevel)
        {
            int nextLevel = tempLevel + 1;
            int requiredMaterial = Math.Max(1, nextLevel * materialPerLevel);
            int requiredCurrency = Math.Max(0, nextLevel * currencyPerLevel);

            totalMaterialUsed += requiredMaterial;
            totalCurrencyUsed += requiredCurrency;

            if (materialLeft >= requiredMaterial && currencyLeft >= requiredCurrency)
            {
                materialLeft -= requiredMaterial;
                currencyLeft -= requiredCurrency;
                levelsGained++;
                tempLevel++;
                totalMaterialUsed += requiredMaterial;
                totalCurrencyUsed += requiredCurrency;
            }
            else
            {
                break;
            }
        }

        return (materialLeft, currencyLeft, levelsGained, totalMaterialUsed, totalCurrencyUsed, levelsGained > 0
            ? AppConstants.Status.SUCCESS
            : AppConstants.Status.NOT_ENOUGH_RESOURCE);
    }
    public static double CalculateToMaterialRequiredForOneUpgrade(int level)
    {
        double material = 10 + (level / 100);
        return material;
    }
    // Thay đổi kiểu trả về thành double
    public static double CalculateTotalMaterialRequiredForMaxUpgrade(int currentLevel, int maxLevel, List<Items> availableItems)
    {
        // 1. Tìm số lượng vật liệu ít nhất (double)
        if (availableItems == null || availableItems.Count == 0) return 0.0;

        // Sử dụng i.Quantity (double) để tìm min
        double minAvailableMaterial = availableItems.Min(i => i.Quantity);

        if (minAvailableMaterial <= 0) return 0.0;

        double totalMaterialRequired = 0.0; // Chi phí tích lũy là double
        int current = currentLevel;

        while (current < maxLevel)
        {
            // Cost per Level vẫn là int
            double costPerLevel = CalculateToMaterialRequiredForOneUpgrade(current);

            // ... (Logic tính toán stepLimit)
            int nextCostStep = (current / 100 + 1) * 100;
            int stepLimit = Math.Min(nextCostStep, maxLevel);

            int levelsInStep = stepLimit - current;
            double totalCostForStep = (double)costPerLevel * levelsInStep; // Nhân với double

            if (totalMaterialRequired + totalCostForStep <= minAvailableMaterial)
            {
                // Đủ vật liệu để hoàn thành toàn bộ bậc chi phí này
                totalMaterialRequired += totalCostForStep;
                current = stepLimit;

                if (current >= maxLevel) break;
            }
            else
            {
                // Hết vật liệu. remainingNeeded là double.
                double remainingNeeded = minAvailableMaterial - totalMaterialRequired;

                // Tính số level có thể mua được (int)
                int levelsPossible = (int)(remainingNeeded / costPerLevel);

                // Tính chi phí chính xác cho số level có thể mua
                totalMaterialRequired += (double)levelsPossible * costPerLevel;
                break;
            }
        }

        // Trả về tổng số lượng vật liệu cần thiết (double)
        return totalMaterialRequired;
    }
}