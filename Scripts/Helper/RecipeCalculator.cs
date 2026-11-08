using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        double maxConfiguredLevel = response.Recipes.Max(r => r.MaxLevel);
        double targetMax = Math.Min(currentLevel + numberOfLevel, maxConfiguredLevel);

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

        double maxConfiguredLevel = response.Recipes.Max(r => r.MaxLevel);

        SimulateUpgrade(response, currentLevel, maxConfiguredLevel, result);

        return result;
    }

    /// <summary>
    /// Logic mô phỏng tính toán chung cho cả 2 hàm
    /// </summary>
    private static void SimulateUpgrade(RecipeResponseDTO response, int currentLevel, double limitLevel, RecipeCalculateDTO result)
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

        double recipeMaxLevel = response.Recipes.Max(r => r.MaxLevel);
        double targetLevel = Math.Min(currentLevel + numberOfLevel, recipeMaxLevel);

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
    /// <summary>
    /// Hàm 1 (Value-based): Nâng thử N cấp dựa trên LevelDTO chứa nhiều khoảng Configs khác nhau.
    /// </summary>
    public static Task<RecipeCalculateDTO> CalculateUpgradeByLevelsValueBasedAsync(
        RecipeResponseDTO response,
        int currentLevel,
        int numberOfLevel,
        double maxLevel,
        LevelDTO levelDto)
    {
        var result = new RecipeCalculateDTO
        {
            TargetLevel = currentLevel,
            GainedLevels = 0,
            TotalDeductions = new List<RecipeItemDetailDTO>()
        };

        if (response == null || response.Recipes == null || response.UserInventory == null || numberOfLevel <= 0)
            return Task.FromResult(result);

        double targetMax = Math.Min(currentLevel + numberOfLevel, maxLevel);

        SimulateUpgradeValueBased(response, currentLevel, targetMax, levelDto, result);

        return Task.FromResult(result);
    }

    /// <summary>
    /// Hàm 2 (Value-based): Nâng MAX cấp tối đa kho đồ chịu được dựa trên LevelDTO.
    /// </summary>
    public static Task<RecipeCalculateDTO> CalculateMaxUpgradeValueBasedAsync(
        RecipeResponseDTO response,
        int currentLevel,
        double maxLevel,
        LevelDTO levelDto)
    {
        var result = new RecipeCalculateDTO
        {
            TargetLevel = currentLevel,
            GainedLevels = 0,
            TotalDeductions = new List<RecipeItemDetailDTO>()
        };

        if (response == null || response.Recipes == null || response.UserInventory == null)
            return Task.FromResult(result);

        SimulateUpgradeValueBased(response, currentLevel, maxLevel, levelDto, result);

        return Task.FromResult(result);
    }

    /// <summary>
    /// Logic mô phỏng tính toán tuần tự theo ObjectValue và tra cứu Config theo khoảng Level
    /// </summary>
    private static void SimulateUpgradeValueBased(
        RecipeResponseDTO response,
        int currentLevel,
        double limitLevel,
        LevelDTO levelDto,
        RecipeCalculateDTO result)
    {
        // Copy kho đồ giả lập để trừ dần theo số lượng thực tế
        var simulatedInventory = new Dictionary<string, double>(response.UserInventory, StringComparer.OrdinalIgnoreCase);
        var deductionMap = new Dictionary<string, RecipeItemDetailDTO>();

        int checkLevel = currentLevel + 1;

        while (checkLevel <= limitLevel)
        {
            // 1. Tìm GameGrowthConfig tương ứng với khoảng level hiện tại (checkLevel nằm trong MinLevel và MaxLevel)
            var currentConfig = levelDto?.Configs?.FirstOrDefault(c => checkLevel >= c.MinLevel && checkLevel <= c.MaxLevel);

            // Nếu không tìm thấy config cho level này thì dừng lại
            if (currentConfig == null)
                break;

            // 2. Tính số lượng / giá trị yêu cầu (exp/value) cho cấp độ này dựa trên FormulaType của config đó
            long requiredValueForThisLevel = CalculateRequiredExp(currentConfig, checkLevel);

            // 3. Tìm công thức nguyên liệu cho level hiện tại
            var recipeForLevel = response.Recipes.FirstOrDefault(r => checkLevel >= r.MinLevel && checkLevel <= r.MaxLevel);

            if (recipeForLevel == null || recipeForLevel.RecipeItemDetails == null || recipeForLevel.RecipeItemDetails.Count == 0)
                break;

            var stepDeductions = new List<(string key, RecipeItemDetailDTO detail, double quantityConsumed, double valueConsumed)>();
            long remainingValueNeeded = requiredValueForThisLevel;

            // 4. XỬ LÝ TUẦN TỰ (Sequential): Duyệt qua từng item để bù đắp đủ requiredValueForThisLevel
            foreach (var detail in recipeForLevel.RecipeItemDetails)
            {
                if (remainingValueNeeded <= 0) break;

                string invKey = GetInventoryKey(simulatedInventory, detail.ObjectType, detail.ObjectId);
                double ownedQty = (invKey != null && simulatedInventory.TryGetValue(invKey, out var qty)) ? qty : 0;

                if (ownedQty <= 0) continue;

                // Giá trị của 1 unit item (ObjectValue)
                double unitValue = detail.ObjectValue > 0 ? detail.ObjectValue : 1;

                // Tổng giá trị user đang có từ item này trong kho giả lập
                double totalAvailableValueFromItem = ownedQty * unitValue;

                // Lấy giá trị cần thiết từ item này
                double valueToTakeFromThisItem = Math.Min(remainingValueNeeded, totalAvailableValueFromItem);

                // Tính quantity tương ứng cần trừ
                double quantityToConsume = Math.Ceiling(valueToTakeFromThisItem / unitValue);
                double actualValueConsumed = quantityToConsume * unitValue;

                // Nếu kho không đủ quantity, vét sạch số lượng hiện có
                if (ownedQty < quantityToConsume)
                {
                    quantityToConsume = ownedQty;
                    actualValueConsumed = quantityToConsume * unitValue;
                }

                simulatedInventory[invKey] -= quantityToConsume;
                remainingValueNeeded -= (long)actualValueConsumed;

                stepDeductions.Add((invKey, detail, quantityToConsume, actualValueConsumed));
            }

            // Nếu duyệt hết các item mà vẫn thiếu giá trị yêu cầu -> Dừng nâng cấp
            if (remainingValueNeeded > 0)
            {
                break;
            }

            // 5. Ghi nhận khấu trừ vào kết quả tổng cộng
            foreach (var step in stepDeductions)
            {
                string mapKey = $"{step.detail.ObjectType}_{step.detail.ObjectId}";
                if (!deductionMap.TryGetValue(mapKey, out var totalDetail))
                {
                    // Lấy số lượng thực tế user đang có trong kho ban đầu (hoặc từ kho giả lập)
                    double initialOwnedQty = response.UserInventory.TryGetValue(step.key, out var q) ? q : 0;

                    totalDetail = new RecipeItemDetailDTO
                    {
                        ObjectId = step.detail.ObjectId,
                        ObjectType = step.detail.ObjectType,
                        ObjectImage = step.detail.ObjectImage,
                        ObjectValue = step.detail.ObjectValue,
                        RequiredQuantity = 0,
                        UserQuantity = initialOwnedQty
                    };
                    deductionMap.Add(mapKey, totalDetail);
                }

                totalDetail.RequiredQuantity += step.quantityConsumed;
            }

            result.GainedLevels++;
            checkLevel++;
        }

        result.TargetLevel = currentLevel + result.GainedLevels;
        result.TotalDeductions = deductionMap.Values.ToList();
    }

    /// <summary>
    /// Hàm tính toán giả lập cấp độ thuần túy cho Preview Mode (Không giới hạn bởi kho đồ, cứ đủ level là cộng dồn nguyên liệu yêu cầu).
    /// </summary>
    public static Task<RecipeCalculateDTO> CalculateUpgradePreviewByLevelsAsync(
        RecipeResponseDTO response, 
        int currentLevel, 
        int numberOfLevel, 
        double maxLevel,
        LevelDTO levelDto) 
    {
        var result = new RecipeCalculateDTO
        {
            TargetLevel = currentLevel,
            GainedLevels = 0,
            TotalDeductions = new List<RecipeItemDetailDTO>()
        };

        if (response == null || response.Recipes == null || numberOfLevel <= 0)
            return Task.FromResult(result);

        double targetMax = Math.Min(currentLevel + numberOfLevel, maxLevel);
        var deductionMap = new Dictionary<string, RecipeItemDetailDTO>();

        int checkLevel = currentLevel + 1;

        while (checkLevel <= targetMax)
        {
            var currentConfig = levelDto?.Configs?.FirstOrDefault(c => checkLevel >= c.MinLevel && checkLevel <= c.MaxLevel);
            if (currentConfig == null) break;

            long requiredValueForThisLevel = CalculateRequiredExp(currentConfig, checkLevel);
            var recipeForLevel = response.Recipes.FirstOrDefault(r => checkLevel >= r.MinLevel && checkLevel <= r.MaxLevel);

            if (recipeForLevel == null || recipeForLevel.RecipeItemDetails == null) break;

            long remainingValueNeeded = requiredValueForThisLevel;

            foreach (var detail in recipeForLevel.RecipeItemDetails)
            {
                if (remainingValueNeeded <= 0) break;

                double unitValue = detail.ObjectValue > 0 ? detail.ObjectValue : 1;
                
                // Ở chế độ Preview: Giả định xem cần bao nhiêu giá trị thì quy đổi ra quantity bấy nhiêu mà không check kho
                double quantityToConsume = Math.Ceiling((double)remainingValueNeeded / unitValue);
                double actualValueConsumed = quantityToConsume * unitValue;

                remainingValueNeeded -= (long)actualValueConsumed;

                string mapKey = $"{detail.ObjectType}_{detail.ObjectId}";
                if (!deductionMap.TryGetValue(mapKey, out var totalDetail))
                {
                    double initialOwnedQty = response.UserInventory != null && response.UserInventory.TryGetValue(detail.ObjectId, out var q) ? q : 0;

                    totalDetail = new RecipeItemDetailDTO
                    {
                        ObjectId = detail.ObjectId,
                        ObjectType = detail.ObjectType,
                        ObjectImage = detail.ObjectImage,
                        ObjectValue = detail.ObjectValue,
                        RequiredQuantity = 0,
                        UserQuantity = initialOwnedQty
                    };
                    deductionMap.Add(mapKey, totalDetail);
                }

                totalDetail.RequiredQuantity += quantityToConsume;
            }

            result.GainedLevels++;
            checkLevel++;
        }

        result.TargetLevel = currentLevel + result.GainedLevels;
        result.TotalDeductions = deductionMap.Values.ToList();
        return Task.FromResult(result);
    }

    /// <summary>
    /// Hàm tính toán giá trị EXP yêu cầu dựa trên công thức cấu hình riêng của từng khoảng level
    /// </summary>
    private static long CalculateRequiredExp(GameGrowthConfig config, int currentLevel)
    {
        if (config == null) return 1000;

        double exp = 0;

        switch (config.FormulaType?.ToUpper())
        {
            case "POWER":
                exp = config.BaseExp +
                      (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) +
                      (currentLevel * config.LinearFactor);
                break;

            case "ROUND_STEP":
                double rawExp = config.BaseExp +
                                (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) +
                                (currentLevel * config.LinearFactor);

                int step = config.StepSize > 0 ? config.StepSize : 1;
                exp = Math.Round(rawExp / step) * step;
                break;

            case "EXPONENTIAL":
                exp = config.BaseExp * Math.Pow(config.BaseMultiplier, currentLevel - 1);
                break;

            default:
                exp = config.BaseExp > 0 ? config.BaseExp : 1000;
                break;
        }

        return (long)Math.Floor(exp);
    }
}