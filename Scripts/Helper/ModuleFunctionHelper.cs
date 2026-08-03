using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class ModuleFunctionHelper
{
    public static async Task<ModulePreviewDTO> PreviewModuleAsync(string featureName, int currentLevel, int maxLevel, int requestedLevels, string userId)
    {
        if (currentLevel >= maxLevel)
        {
            return new ModulePreviewDTO
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
        int currentTargetLevel = currentLevel + 1;

        while (currentTargetLevel <= currentLevel + requestedLevels)
        {
            var recipeItems =
                await RecipeService.Create().GetRecipeItemsAsync(
                    featureName,
                    currentTargetLevel,
                    userId);

            if (recipeItems == null || recipeItems.Count == 0)
                break;

            foreach (var item in recipeItems)
            {
                if (!userItemMap.ContainsKey(item.ItemId))
                {
                    userItemMap[item.ItemId] = item.UserQuantity;
                }
            }

            int rangeEndLevel = recipeItems.Max(item => item.MaxLevel);
            int maxRangeTarget = Math.Min(currentLevel + requestedLevels, rangeEndLevel);
            int blockSize = maxRangeTarget - currentTargetLevel + 1;

            bool canModuleAllLevelsInRange = recipeItems.All(item =>
                userItemMap[item.ItemId] >= item.RequiredQuantity * blockSize);

            if (canModuleAllLevelsInRange)
            {
                foreach (var item in recipeItems)
                {
                    double totalRequired = item.RequiredQuantity * blockSize;

                    userItemMap[item.ItemId] -= totalRequired;

                    if (!totalRequiredMap.ContainsKey(item.ItemId))
                        totalRequiredMap[item.ItemId] = 0;

                    totalRequiredMap[item.ItemId] += totalRequired;
                }

                upgradedLevels += blockSize;
                currentTargetLevel += blockSize;
                continue;
            }

            bool canModuleOneLevel = true;
            foreach (var item in recipeItems)
            {
                if (userItemMap[item.ItemId] < item.RequiredQuantity)
                {
                    canModuleOneLevel = false;
                    break;
                }
            }

            if (!canModuleOneLevel)
                break;

            foreach (var item in recipeItems)
            {
                userItemMap[item.ItemId] -= item.RequiredQuantity;

                if (!totalRequiredMap.ContainsKey(item.ItemId))
                    totalRequiredMap[item.ItemId] = 0;

                totalRequiredMap[item.ItemId] += item.RequiredQuantity;
            }

            upgradedLevels++;
            currentTargetLevel++;
        }

        return new ModulePreviewDTO
        {
            Success = upgradedLevels > 0,
            CurrentLevel = currentLevel,
            TargetLevel = currentLevel + upgradedLevels,
            ModuledLevels = upgradedLevels,
            RequiredItems = totalRequiredMap,
            Message = upgradedLevels > 0
                ? MessageConstants.UPGRADE_PREVIEW_SUCCESS
                : MessageConstants.NOT_ENOUGH_MATERIALS
        };
    }
    public static async Task<ModuleResultDTO> ModuleLevelAsync(string featureName, int currentLevel, int maxLevel, int requestedLevels, string userId)
    {
        try
        {
            var preview =
                await PreviewModuleAsync(
                    featureName,
                    currentLevel,
                    maxLevel,
                    requestedLevels,
                    userId);

            if (!preview.Success)
            {
                return new ModuleResultDTO
                {
                    Success = false,
                    ModuledLevels = 0,
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

            return new ModuleResultDTO
            {
                Success = true,
                ModuledLevels = preview.ModuledLevels,
                Message = string.Format(
                    MessageConstants.UPGRADE_SUCCESS_MULTIPLE,
                    preview.ModuledLevels)
            };
        }
        catch (Exception ex)
        {
            return new ModuleResultDTO
            {
                Success = false,
                Message = string.Format(
                    MessageConstants.SYSTEM_ERROR,
                    ex.Message)
            };
        }
    }
}