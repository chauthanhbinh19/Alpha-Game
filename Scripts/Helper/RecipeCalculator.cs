using System;
using System.Collections.Generic;
using System.Linq;

public static class RecipeCalculator
{
    /// <summary>
    /// Hàm 1: Nâng thử N cấp (numberOfLevel)
    /// Điều kiện: (currentLevel + numberOfLevel) <= MaxLevel và đủ nguyên liệu.
    /// </summary>
    public static RecipeCalculateDTO CalculateUpgradeByLevels(RecipeResponseDTO response, int currentLevel, int numberOfLevel)
    {
        var result = new RecipeCalculateDTO
        {
            TargetLevel = currentLevel,
            GainedLevels = 0,
            TotalDeductions = new List<RecipeItemDetailDTO>()
        };

        if (response == null || response.Recipes == null || response.UserInventory == null || numberOfLevel <= 0)
            return result;

        int maxConfiguredLevel = response.Recipes.Max(r => r.MaxLevel);
        int targetMax = Math.Min(currentLevel + numberOfLevel, maxConfiguredLevel);

        SimulateUpgrade(response, currentLevel, targetMax, result);

        return result;
    }

    /// <summary>
    /// Hàm 2: Nâng MAX cấp có thể chịu được.
    /// Điều kiện: Tối đa nguyên liệu đang có và <= MaxLevel.
    /// </summary>
    public static RecipeCalculateDTO CalculateMaxUpgrade(RecipeResponseDTO response, int currentLevel)
    {
        var result = new RecipeCalculateDTO
        {
            TargetLevel = currentLevel,
            GainedLevels = 0,
            TotalDeductions = new List<RecipeItemDetailDTO>()
        };

        if (response == null || response.Recipes == null || response.UserInventory == null)
            return result;

        int maxConfiguredLevel = response.Recipes.Max(r => r.MaxLevel);

        SimulateUpgrade(response, currentLevel, maxConfiguredLevel, result);

        return result;
    }

    /// <summary>
    /// Logic mô phỏng tính toán chung cho cả 2 hàm
    /// </summary>
    private static void SimulateUpgrade(RecipeResponseDTO response, int currentLevel, int limitLevel, RecipeCalculateDTO result)
    {
        // Copy kho đồ giả lập
        var simulatedInventory = new Dictionary<string, double>(response.UserInventory, StringComparer.OrdinalIgnoreCase);
        var deductionMap = new Dictionary<string, RecipeItemDetailDTO>();

        int checkLevel = currentLevel + 1;

        while (checkLevel <= limitLevel)
        {
            // Tìm công thức cho level hiện tại
            var recipeForLevel = response.Recipes.FirstOrDefault(r => checkLevel >= r.MinLevel && checkLevel <= r.MaxLevel);

            if (recipeForLevel == null || recipeForLevel.RecipeItemDetails == null || recipeForLevel.RecipeItemDetails.Count == 0)
                break;

            // Kiểm tra nguyên liệu kho đồ giả lập
            bool canAfford = true;
            var stepDeductions = new List<(string key, RecipeItemDetailDTO detail, double required)>();

            foreach (var detail in recipeForLevel.RecipeItemDetails)
            {
                string invKey = GetInventoryKey(simulatedInventory, detail.ObjectType, detail.ObjectId);
                double ownedQty = (invKey != null && simulatedInventory.TryGetValue(invKey, out var qty)) ? qty : 0;

                if (ownedQty < detail.RequiredQuantity)
                {
                    canAfford = false;
                    break;
                }

                stepDeductions.Add((invKey, detail, detail.RequiredQuantity));
            }

            if (!canAfford)
                break; // Thiếu nguyên liệu -> Dừng nâng cấp

            // Khấu trừ kho giả lập và cộng dồn nguyên liệu tiêu tốn
            foreach (var step in stepDeductions)
            {
                simulatedInventory[step.key] -= step.required;

                string mapKey = $"{step.detail.ObjectType}_{step.detail.ObjectId}";
                if (!deductionMap.TryGetValue(mapKey, out var totalDetail))
                {
                    totalDetail = new RecipeItemDetailDTO
                    {
                        ObjectId = step.detail.ObjectId,
                        ObjectType = step.detail.ObjectType,
                        ObjectImage = step.detail.ObjectImage,
                        ObjectValue = step.detail.ObjectValue,
                        RequiredQuantity = 0
                    };
                    deductionMap.Add(mapKey, totalDetail);
                }

                totalDetail.RequiredQuantity += step.required;
            }

            result.GainedLevels++;
            checkLevel++;
        }

        result.TargetLevel = currentLevel + result.GainedLevels;
        result.TotalDeductions = deductionMap.Values.ToList();
    }

    private static string GetInventoryKey(Dictionary<string, double> inventory, string objectType, string objectId)
    {
        if (inventory.ContainsKey(objectId))
            return objectId;

        string prefixedKey = $"{objectType}_{objectId}";
        if (inventory.ContainsKey(prefixedKey))
            return prefixedKey;

        return null;
    }

    // --- HÀM 1: PREVIEW NÂNG CẤP THEO SỐ LEVEL YÊU CẦU (CHO PREVIEW MODE & SLIDER) ---
    // Không bị dừng lại khi thiếu nguyên liệu, tính toán chính xác tổng nguyên liệu cần cho 'numberOfLevel'
    public static RecipePreviewResultDTO PreviewUpgrade(RecipeResponseDTO response, int currentLevel, int numberOfLevel)
    {
        var result = new RecipePreviewResultDTO
        {
            TargetLevel = currentLevel,
            GainedLevels = 0,
            CanAffordAll = true,
            RequiredMaterials = new List<RecipePreviewDetailDTO>()
        };

        if (response == null || response.Recipes == null || response.UserInventory == null || numberOfLevel <= 0)
            return result;

        int recipeMaxLevel = response.Recipes.Max(r => r.MaxLevel);
        int targetLevel = Math.Min(currentLevel + numberOfLevel, recipeMaxLevel);

        var deductionMap = new Dictionary<string, RecipePreviewDetailDTO>();

        for (int checkLevel = currentLevel + 1; checkLevel <= targetLevel; checkLevel++)
        {
            var recipeForLevel = response.Recipes.FirstOrDefault(r => checkLevel >= r.MinLevel && checkLevel <= r.MaxLevel);
            if (recipeForLevel == null || recipeForLevel.RecipeItemDetails == null) break;

            foreach (var detail in recipeForLevel.RecipeItemDetails)
            {
                string mapKey = $"{detail.ObjectType}_{detail.ObjectId}";
                if (!deductionMap.TryGetValue(mapKey, out var mat))
                {
                    string invKey = GetInventoryKey(response.UserInventory, detail.ObjectType, detail.ObjectId);
                    double ownedQty = (invKey != null && response.UserInventory.TryGetValue(invKey, out var qty)) ? qty : 0;

                    mat = new RecipePreviewDetailDTO
                    {
                        ObjectId = detail.ObjectId,
                        ObjectType = detail.ObjectType,
                        ObjectImage = detail.ObjectImage,
                        TotalRequiredQuantity = 0,
                        CurrentUserQuantity = ownedQty,
                        IsEnough = true
                    };
                    deductionMap.Add(mapKey, mat);
                }

                mat.TotalRequiredQuantity += detail.RequiredQuantity;
                mat.IsEnough = mat.CurrentUserQuantity >= mat.TotalRequiredQuantity;

                if (!mat.IsEnough)
                {
                    result.CanAffordAll = false;
                }
            }

            result.GainedLevels++;
        }

        result.TargetLevel = currentLevel + result.GainedLevels;
        result.RequiredMaterials = deductionMap.Values.ToList();
        return result;
    }

    // --- HÀM 2: PREVIEW NÂNG MAX TỐI ĐA KHO ĐỒ CHI TRẢ ĐƯỢC ---
    public static RecipePreviewResultDTO PreviewMaxUpgrade(RecipeResponseDTO response, int currentLevel)
    {
        // Tận dụng kết quả từ CalculateMaxUpgrade (đã dừng khi thiếu nguyên liệu)
        RecipeCalculateDTO calc = CalculateMaxUpgrade(response, currentLevel);

        return new RecipePreviewResultDTO
        {
            TargetLevel = calc.TargetLevel,
            GainedLevels = calc.GainedLevels,
            CanAffordAll = calc.GainedLevels > 0,
            RequiredMaterials = calc.TotalDeductions.Select(d =>
            {
                string invKey = GetInventoryKey(response.UserInventory, d.ObjectType, d.ObjectId);
                double ownedQty = (invKey != null && response.UserInventory.TryGetValue(invKey, out var qty)) ? qty : 0;

                return new RecipePreviewDetailDTO
                {
                    ObjectId = d.ObjectId,
                    ObjectType = d.ObjectType,
                    ObjectImage = d.ObjectImage,
                    TotalRequiredQuantity = d.RequiredQuantity,
                    CurrentUserQuantity = ownedQty,
                    IsEnough = ownedQty >= d.RequiredQuantity
                };
            }).ToList()
        };
    }
}