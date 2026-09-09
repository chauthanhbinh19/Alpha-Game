using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FoodsGalleryService : IFoodsGalleryService
{
    private readonly IFoodsGalleryRepository _foodsGalleryRepository;
    private readonly IFoodsService _foodsService;
    private readonly IPowerManagerService _powerManagerService;

    public FoodsGalleryService(
        IFoodsGalleryRepository foodsGalleryRepository,
        IFoodsService foodsService,
        IPowerManagerService powerManagerService)
    {
        _foodsGalleryRepository = foodsGalleryRepository;
        _foodsService = foodsService;
        _powerManagerService = powerManagerService;
    }

    public static IFoodsGalleryService Create() => ServiceContainer.GetService<IFoodsGalleryService>();

    public async Task<List<Foods>> GetFoodsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Foods> list = await _foodsGalleryRepository.GetFoodsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsCountAsync(string search, string rare)
    {
        return await _foodsGalleryRepository.GetFoodsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertFoodGalleryAsync(string userId, string Id)
    {
        var checkResult = await _foodsService.IsFoodDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _foodsGalleryRepository.InsertFoodGalleryAsync(userId, Id, await _foodsService.GetFoodByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusFoodGalleryAsync(string userId, string foodId)
    {
        var checkResult = await _foodsService.IsFoodDeletedOrInactiveAsync(foodId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _foodsGalleryRepository.UpdateStatusFoodGalleryAsync(userId, foodId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Foods foodGallery = await GetFoodCollectionByIdAsync(userId, foodId) ?? new Foods();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)foodGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFoodsGalleryAsync(string userId)
    {
        Foods oldFood = await SumPowerFoodsGalleryAsync(userId);

        var updateResult = await _foodsGalleryRepository.UpdateBatchStatusFoodsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Foods newFood = await SumPowerFoodsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Foods> SumPowerFoodsGalleryAsync(string userId)
    {
        return await _foodsGalleryRepository.SumPowerFoodsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarFoodGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _foodsService.IsFoodDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _foodsGalleryRepository.UpdateTempStarFoodGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarFoodGalleryAsync(string userId, string foodId)
    {
        var checkResult = await _foodsService.IsFoodDeletedOrInactiveAsync(foodId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Foods oldFood = await GetFoodCollectionByIdAsync(userId, foodId) ?? new Foods();

        var updateResult = await _foodsGalleryRepository.UpdateCurrentStarFoodGalleryAsync(userId, foodId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Foods newFood = await GetFoodCollectionByIdAsync(userId, foodId) ?? new Foods();
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarFoodsGalleryAsync(string userId)
    {
        Foods oldFood = await SumPowerFoodsGalleryAsync(userId);

        var updateResult = await _foodsGalleryRepository.UpdateBatchCurrentStarFoodsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Foods newFood = await SumPowerFoodsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchFoodsGalleryAsync(string userId, List<Foods> foods)
    {
        var insertResult = await _foodsGalleryRepository.InsertBatchFoodsGalleryAsync(userId, foods);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Foods> GetFoodCollectionByIdAsync(string userId, string foodId)
    {
        var result = await _foodsGalleryRepository.GetFoodCollectionByIdAsync(userId, foodId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateFoodGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _foodsService.IsFoodDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _foodsGalleryRepository.UpdateFoodGalleryPowerAsync(userId, Id, await _service.GetFoodByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
