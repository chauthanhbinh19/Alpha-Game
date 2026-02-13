using System;
using System.Threading.Tasks;

public class UpgradeService
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
                    Message = string.Format(MessageConstants.NOT_ENOUGH_ITEM, item.ItemId)
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
                Message = string.Format(MessageConstants.SYSTEM_ERROR, ex.Message)
            };
        }
    }

    // ================================
    // UPGRADE MAX LEVEL
    // ================================
    public async Task<UpgradeResultDTO> UpgradeMaxLevelAsync(string featureName,int currentLevel,int maxLevel,string userId)
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

        int upgradedCount = 0;

        try
        {
            while (currentLevel < maxLevel)
            {
                var recipeItems = await _recipeRepository
                    .GetRecipeItemsAsync(featureName, currentLevel + 1, userId);

                // 🚨 Không tìm thấy recipe = lỗi data → return ngay
                if (recipeItems == null || recipeItems.Count == 0)
                {
                    return new UpgradeResultDTO
                    {
                        Success = false,
                        UpgradedLevels = upgradedCount,
                        Message = MessageConstants.RECIPE_NOT_FOUND
                    };
                }

                // 🚨 Check đủ nguyên liệu không
                foreach (var item in recipeItems)
                {
                    if (item.UserQuantity < item.RequiredQuantity)
                    {
                        // Nếu chưa upgrade được level nào
                        if (upgradedCount == 0)
                        {
                            return new UpgradeResultDTO
                            {
                                Success = false,
                                UpgradedLevels = 0,
                                Message = MessageConstants.NOT_ENOUGH_MATERIALS
                            };
                        }

                        // Nếu đã upgrade được vài level rồi
                        return new UpgradeResultDTO
                        {
                            Success = true,
                            UpgradedLevels = upgradedCount,
                            Message = string.Format(MessageConstants.UPGRADE_SUCCESS_MULTIPLE,upgradedCount)
                        };
                    }
                }

                await _recipeRepository.DeductItemsAsync(userId, recipeItems);

                upgradedCount++;
                currentLevel++;
            }

            return new UpgradeResultDTO
            {
                Success = true,
                UpgradedLevels = upgradedCount,
                Message = string.Format(MessageConstants.UPGRADE_SUCCESS_MULTIPLE,upgradedCount)
            };
        }
        catch (Exception ex)
        {
            return new UpgradeResultDTO
            {
                Success = false,
                UpgradedLevels = 0,
                Message = string.Format(MessageConstants.SYSTEM_ERROR,ex.Message)
            };
        }
    }
}
