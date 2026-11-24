using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPlantsService : IUserPlantsService
{
    private readonly IUserPlantsRepository _userPlantsRepository;
    private readonly IPlantsGalleryService _plantsGalleryService;
    private readonly IPlantsService _plantsService;

    public UserPlantsService(
        IUserPlantsRepository userPlantsRepository,
        IPlantsGalleryService plantsGalleryService,
        IPlantsService plantsService)
    {
        _userPlantsRepository = userPlantsRepository;
        _plantsGalleryService = plantsGalleryService;
        _plantsService = plantsService;
    }

    public static IUserPlantsService Create() => ServiceContainer.GetService<IUserPlantsService>();

    public async Task<List<Plants>> GetUserPlantsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Plants> result = await _userPlantsRepository.GetUserPlantsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserPlantsCountAsync(string userId, string search, string rare)
    {
        return await _userPlantsRepository.GetUserPlantsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPlantAsync(string userId, Plants plant)
    {
        var checkPlantResult = await _plantsService.IsPlantDeletedOrInactiveAsync(plant.Id);
        if (checkPlantResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userPlantsRepository.InsertOrUpdateUserPlantAsync(userId, plant);

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

        await _plantsGalleryService.InsertPlantGalleryAsync(userId, plant.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPlantsBatchAsync(string userId, List<Plants> plants)
    {
        var insertOrUpdateResult = await _userPlantsRepository.InsertOrUpdateUserPlantsBatchAsync(userId, plants);

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
            await _plantsGalleryService.InsertBatchPlantsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserPlantLevelAsync(string userId, Plants plant, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (plant == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Plant is required.");
        }

        var checkResult = await _plantsService.IsPlantDeletedOrInactiveAsync(plant.Id);
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

        var updateResult = await _userPlantsRepository.UpdateUserPlantLevelAsync(
            userId,
            plant,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserPlantStarAsync(string userId, Plants plant)
    {
        var checkPlantResult = await _plantsService.IsPlantDeletedOrInactiveAsync(plant.Id);
        if (checkPlantResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userPlantsRepository.UpdateUserPlantStarAsync(userId, plant);

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

        await _plantsGalleryService.UpdateTempStarPlantGalleryAsync(userId, plant.Id, plant.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserPlantQuantityAsync(string userId, Plants plant, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userPlantsRepository.UpdateUserPlantQuantityAsync(userId, plant, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserPlantAffinityAsync(string userId, Plants plant, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (plant == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Plant is required.");
        }

        var checkResult = await _plantsService.IsPlantDeletedOrInactiveAsync(plant.Id);
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

        var updateResult = await _userPlantsRepository.UpdateUserPlantAffinityAsync(
            userId,
            plant,
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

    public async Task<Plants> GetUserPlantByIdAsync(string userId, string Id)
    {
        var result = await _userPlantsRepository.GetUserPlantByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Plants> SumPowerUserPlantsAsync(string userId)
    {
        return await _userPlantsRepository.SumPowerUserPlantsAsync(userId);
    }
}
