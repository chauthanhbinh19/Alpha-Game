using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class UpgradeFunctionHelper
{
    public static async Task<UpgradePreviewDTO> PreviewUpgradeAsync(string featureName, int currentLevel, int maxLevel, int requestedLevels, string userId)
    {
        if (currentLevel >= maxLevel)
        {
            return new UpgradePreviewDTO
            {
                Success = false,
                Message = MessageConstants.MAX_LEVEL_REACHED
            };
        }

        int maxPossibleLevels = maxLevel - currentLevel;

        requestedLevels = Math.Min(
            requestedLevels,
            maxPossibleLevels);

        var userItemMap = new Dictionary<string, double>();
        var totalRequiredMap = new Dictionary<string, double>();

        int upgradedLevels = 0;

        for (int level = currentLevel + 1;
             level <= currentLevel + requestedLevels;
             level++)
        {
            var recipeItems =
                await RecipeService.Create().GetRecipeItemsAsync(
                    featureName,
                    level,
                    userId);

            if (recipeItems == null || recipeItems.Count == 0)
                break;

            foreach (var item in recipeItems)
            {
                if (!userItemMap.ContainsKey(item.ItemId))
                {
                    userItemMap[item.ItemId] =
                        item.UserQuantity;
                }
            }

            bool canUpgrade = recipeItems.All(item =>
                userItemMap[item.ItemId] >= item.RequiredQuantity);

            if (!canUpgrade)
                break;

            foreach (var item in recipeItems)
            {
                userItemMap[item.ItemId] -= item.RequiredQuantity;

                if (!totalRequiredMap.ContainsKey(item.ItemId))
                    totalRequiredMap[item.ItemId] = 0;

                totalRequiredMap[item.ItemId] += item.RequiredQuantity;
            }

            upgradedLevels++;
        }

        return new UpgradePreviewDTO
        {
            Success = upgradedLevels > 0,
            CurrentLevel = currentLevel,
            TargetLevel = currentLevel + upgradedLevels,
            UpgradedLevels = upgradedLevels,
            RequiredItems = totalRequiredMap,
            Message = upgradedLevels > 0
                ? MessageConstants.UPGRADE_PREVIEW_SUCCESS
                : MessageConstants.NOT_ENOUGH_MATERIALS
        };
    }
    public static async Task<UpgradeResultDTO> UpgradeLevelAsync(string featureName, int currentLevel, int maxLevel, int requestedLevels, string userId)
    {
        try
        {
            var preview =
                await PreviewUpgradeAsync(
                    featureName,
                    currentLevel,
                    maxLevel,
                    requestedLevels,
                    userId);

            if (!preview.Success)
            {
                return new UpgradeResultDTO
                {
                    Success = false,
                    UpgradedLevels = 0,
                    Message = preview.Message
                };
            }

            var deductList =
                preview.RequiredItems
                    .Select(x => new RecipeItemDto
                    {
                        ItemId = x.Key,
                        RequiredQuantity = x.Value
                    })
                    .ToList();

            await RecipeService.Create()
                .DeductItemsAsync(
                    userId,
                    deductList);

            return new UpgradeResultDTO
            {
                Success = true,
                UpgradedLevels = preview.UpgradedLevels,
                Message = string.Format(
                    MessageConstants.UPGRADE_SUCCESS_MULTIPLE,
                    preview.UpgradedLevels)
            };
        }
        catch (Exception ex)
        {
            return new UpgradeResultDTO
            {
                Success = false,
                Message = string.Format(
                    MessageConstants.SYSTEM_ERROR,
                    ex.Message)
            };
        }
    }
}