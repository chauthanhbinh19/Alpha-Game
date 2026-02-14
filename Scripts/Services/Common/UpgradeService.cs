using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UpgradeService : IUpgradeService
{
    private static UpgradeService _instance;
    private readonly IRecipeRepository _recipeRepository;

    public UpgradeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public static UpgradeService Create()
    {
        if (_instance == null)
        {
            _instance = new UpgradeService(new RecipeRepository());
        }
        return _instance;
    }

    // ================================
    // UPGRADE 1 LEVEL
    // ================================
    public async Task<UpgradeResultDTO> UpgradeOneLevelAsync(string featureName, int currentLevel, int maxLevel, string userId)
    {
        if (currentLevel >= maxLevel)
        {
            return new UpgradeResultDTO
            {
                Success = false,
                UpgradedLevels = 0,
                Message = MessageConstants.MAX_LEVEL_REACHED
            };
        }

        var recipeItems = await _recipeRepository
            .GetRecipeItemsAsync(featureName, currentLevel + 1, userId);

        if (recipeItems == null || recipeItems.Count == 0)
        {
            return new UpgradeResultDTO
            {
                Success = false,
                UpgradedLevels = 0,
                Message = MessageConstants.RECIPE_NOT_FOUND
            };
        }

        // Kiểm tra đủ nguyên liệu
        foreach (var item in recipeItems)
        {
            if (item.UserQuantity < item.RequiredQuantity)
            {
                return new UpgradeResultDTO
                {
                    Success = false,
                    UpgradedLevels = 0,
                    Message = string.Format(
                        MessageConstants.NOT_ENOUGH_ITEM,
                        item.ItemId)
                };
            }
        }

        try
        {
            await _recipeRepository.DeductItemsAsync(userId, recipeItems);

            return new UpgradeResultDTO
            {
                Success = true,
                UpgradedLevels = 1,
                Message = MessageConstants.UPGRADE_SUCCESS_ONE
            };
        }
        catch (Exception ex)
        {
            return new UpgradeResultDTO
            {
                Success = false,
                UpgradedLevels = 0,
                Message = string.Format(
                    MessageConstants.SYSTEM_ERROR,
                    ex.Message)
            };
        }
    }

    // ================================
    // UPGRADE MAX LEVEL
    // ================================
    public async Task<UpgradeResultDTO> UpgradeMaxLevelAsync(string featureName, int currentLevel, int maxLevel, string userId)
    {
        if (currentLevel >= maxLevel)
        {
            return new UpgradeResultDTO
            {
                Success = false,
                UpgradedLevels = 0,
                Message = MessageConstants.MAX_LEVEL_REACHED
            };
        }

        try
        {
            int upgradedCount = 0;
            int checkLevel = currentLevel + 1;

            var userItemMap = new Dictionary<string, double>();
            var totalDeductMap = new Dictionary<string, double>();

            List<RecipeItemDto> currentRecipeItems = null;
            int currentRecipeMaxLevel = 0;

            while (checkLevel <= maxLevel)
            {
                if (currentRecipeItems == null || checkLevel > currentRecipeMaxLevel)
                {
                    currentRecipeItems = await _recipeRepository
                        .GetRecipeItemsAsync(featureName, checkLevel, userId);

                    if (currentRecipeItems == null || currentRecipeItems.Count == 0)
                        break;

                    currentRecipeMaxLevel = currentRecipeItems.First().MaxLevel;

                    foreach (var item in currentRecipeItems)
                    {
                        if (!userItemMap.ContainsKey(item.ItemId))
                            userItemMap[item.ItemId] = item.UserQuantity;
                    }
                }

                bool canUpgrade = true;

                foreach (var item in currentRecipeItems)
                {
                    if (userItemMap[item.ItemId] < item.RequiredQuantity)
                    {
                        canUpgrade = false;
                        break;
                    }
                }

                if (!canUpgrade)
                    break;

                // Trừ tạm trong RAM + cộng dồn tổng trừ
                foreach (var item in currentRecipeItems)
                {
                    userItemMap[item.ItemId] -= item.RequiredQuantity;

                    if (!totalDeductMap.ContainsKey(item.ItemId))
                        totalDeductMap[item.ItemId] = 0;

                    totalDeductMap[item.ItemId] += item.RequiredQuantity;
                }

                upgradedCount++;
                checkLevel++;
            }

            if (upgradedCount == 0)
            {
                return new UpgradeResultDTO
                {
                    Success = false,
                    UpgradedLevels = 0,
                    Message = MessageConstants.NOT_ENOUGH_MATERIALS
                };
            }

            // 🔥 Convert sang list gộp sẵn
            var finalDeductList = totalDeductMap
                .Select(x => new RecipeItemDto
                {
                    ItemId = x.Key,
                    RequiredQuantity = x.Value
                })
                .ToList();

            // Gọi hàm deduct 1 lần
            await _recipeRepository
                .DeductItemsAsync(userId, finalDeductList);

            return new UpgradeResultDTO
            {
                Success = true,
                UpgradedLevels = upgradedCount,
                Message = string.Format(
                    MessageConstants.UPGRADE_SUCCESS_MULTIPLE,
                    upgradedCount)
            };
        }
        catch (Exception ex)
        {
            return new UpgradeResultDTO
            {
                Success = false,
                UpgradedLevels = 0,
                Message = string.Format(
                    MessageConstants.SYSTEM_ERROR,
                    ex.Message)
            };
        }
    }

}
