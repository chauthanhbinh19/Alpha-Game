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

    public async Task<bool> InsertFoodGalleryAsync(string userId, string Id)
    {
        var insertResult = await _foodsGalleryRepository.InsertFoodGalleryAsync(userId, Id, await _foodsService.GetFoodByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusFoodGalleryAsync(string userId, string foodId)
    {
        var updateResult = await _foodsGalleryRepository.UpdateStatusFoodGalleryAsync(userId, foodId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Foods foodGallery = await GetFoodCollectionByIdAsync(userId, foodId) ?? new Foods();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)foodGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusFoodsGalleryAsync(string userId)
    {
        Foods oldFood = await SumPowerFoodsGalleryAsync(userId);

        var updateResult = await _foodsGalleryRepository.UpdateBatchStatusFoodsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Foods newFood = await SumPowerFoodsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Foods> SumPowerFoodsGalleryAsync(string userId)
    {
        return await _foodsGalleryRepository.SumPowerFoodsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarFoodGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _foodsGalleryRepository.UpdateTempStarFoodGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarFoodGalleryAsync(string userId, string foodId)
    {
        Foods oldFood = await GetFoodCollectionByIdAsync(userId, foodId) ?? new Foods();

        var updateResult = await _foodsGalleryRepository.UpdateCurrentStarFoodGalleryAsync(userId, foodId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Foods newFood = await GetFoodCollectionByIdAsync(userId, foodId) ?? new Foods();
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarFoodsGalleryAsync(string userId)
    {
        Foods oldFood = await SumPowerFoodsGalleryAsync(userId);

        var updateResult = await _foodsGalleryRepository.UpdateBatchCurrentStarFoodsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Foods newFood = await SumPowerFoodsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFood - (PowerManager)oldFood;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchFoodsGalleryAsync(string userId, List<Foods> foods)
    {
        var insertResult = await _foodsGalleryRepository.InsertBatchFoodsGalleryAsync(userId, foods);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Foods> GetFoodCollectionByIdAsync(string userId, string foodId)
    {
        var result = await _foodsGalleryRepository.GetFoodCollectionByIdAsync(userId, foodId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateFoodGalleryPowerAsync(string userId, string Id)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _foodsGalleryRepository.UpdateFoodGalleryPowerAsync(userId, Id, await _service.GetFoodByIdAsync(Id));
    }
}
