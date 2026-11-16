using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBuildingsService : IUserBuildingsService
{
    private readonly IUserBuildingsRepository _userBuildingsRepository;
    private readonly IBuildingsGalleryService _buildingsGalleryService;
    private readonly IBuildingsService _buildingsService;

    public UserBuildingsService(
        IUserBuildingsRepository userBuildingsRepository,
        IBuildingsGalleryService buildingsGalleryService,
        IBuildingsService buildingsService)
    {
        _userBuildingsRepository = userBuildingsRepository;
        _buildingsGalleryService = buildingsGalleryService;
        _buildingsService = buildingsService;
    }

    public static IUserBuildingsService Create() => ServiceContainer.GetService<IUserBuildingsService>();

    public async Task<List<Buildings>> GetUserBuildingsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> result = await _userBuildingsRepository.GetUserBuildingsAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserBuildingsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userBuildingsRepository.GetUserBuildingsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingAsync(string userId, Buildings building)
    {
        var checkBuildingResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(building.Id);
        if (checkBuildingResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userBuildingsRepository.InsertOrUpdateUserBuildingAsync(userId, building);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _buildingsGalleryService.InsertBuildingGalleryAsync(userId, building.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingsBatchAsync(string userId, List<Buildings> buildings)
    {
        var insertOrUpdateResult = await _userBuildingsRepository.InsertOrUpdateUserBuildingsBatchAsync(userId, buildings);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _buildingsGalleryService.InsertBatchBuildingsGalleryAsync(userId, newlyInsertedCards);
        }

        return insertOrUpdateResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBuildingLevelAsync(string userId, Buildings building, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (building == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Building is required.");
        }

        var checkResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(building.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userBuildingsRepository.UpdateUserBuildingLevelAsync(
            userId,
            building,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBuildingStarAsync(string userId, Buildings building)
    {
        var checkBuildingResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(building.Id);
        if (checkBuildingResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userBuildingsRepository.UpdateUserBuildingStarAsync(userId, building);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        await _buildingsGalleryService.UpdateTempStarBuildingGalleryAsync(userId, building.Id, building.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBuildingQuantityAsync(string userId, Buildings building, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userBuildingsRepository.UpdateUserBuildingQuantityAsync(userId, building, totalDeductions);

        return result;
    }

    public async Task<Buildings> GetUserBuildingByIdAsync(string userId, string Id)
    {
        var result = await _userBuildingsRepository.GetUserBuildingByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Buildings> SumPowerUserBuildingsAsync(string userId)
    {
        return await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);
    }
}
