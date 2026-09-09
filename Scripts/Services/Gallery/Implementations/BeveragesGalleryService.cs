using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BeveragesGalleryService : IBeveragesGalleryService
{
    private readonly IBeveragesGalleryRepository _beveragesGalleryRepository;
    private readonly IBeveragesService _beveragesService;
    private readonly IPowerManagerService _powerManagerService;

    public BeveragesGalleryService(
        IBeveragesGalleryRepository beveragesGalleryRepository,
        IBeveragesService beveragesService,
        IPowerManagerService powerManagerService)
    {
        _beveragesGalleryRepository = beveragesGalleryRepository;
        _beveragesService = beveragesService;
        _powerManagerService = powerManagerService;
    }

    public static IBeveragesGalleryService Create() => ServiceContainer.GetService<IBeveragesGalleryService>();

    public async Task<List<Beverages>> GetBeveragesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _beveragesGalleryRepository.GetBeveragesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesCountAsync(string search, string rare)
    {
        return await _beveragesGalleryRepository.GetBeveragesCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBeverageGalleryAsync(string userId, string Id)
    {
        var checkResult = await _beveragesService.IsBeverageDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _beveragesGalleryRepository.InsertBeverageGalleryAsync(userId, Id, await _beveragesService.GetBeverageByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusBeverageGalleryAsync(string userId, string beverageId)
    {
        var checkResult = await _beveragesService.IsBeverageDeletedOrInactiveAsync(beverageId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _beveragesGalleryRepository.UpdateStatusBeverageGalleryAsync(userId, beverageId);

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
        Beverages beverageGallery = await GetBeverageCollectionByIdAsync(userId, beverageId) ?? new Beverages();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)beverageGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBeveragesGalleryAsync(string userId)
    {
        Beverages oldBeverage = await SumPowerBeveragesGalleryAsync(userId);

        var updateResult = await _beveragesGalleryRepository.UpdateBatchStatusBeveragesGalleryAsync(userId);

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

        Beverages newBeverage = await SumPowerBeveragesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

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

    public async Task<Beverages> SumPowerBeveragesGalleryAsync(string userId)
    {
        return await _beveragesGalleryRepository.SumPowerBeveragesGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarBeverageGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _beveragesService.IsBeverageDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _beveragesGalleryRepository.UpdateTempStarBeverageGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBeverageGalleryAsync(string userId, string beverageId)
    {
        var checkResult = await _beveragesService.IsBeverageDeletedOrInactiveAsync(beverageId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Beverages oldBeverage = await GetBeverageCollectionByIdAsync(userId, beverageId) ?? new Beverages();

        var updateResult = await _beveragesGalleryRepository.UpdateCurrentStarBeverageGalleryAsync(userId, beverageId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Beverages newBeverage = await GetBeverageCollectionByIdAsync(userId, beverageId) ?? new Beverages();
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBeveragesGalleryAsync(string userId)
    {
        Beverages oldBeverage = await SumPowerBeveragesGalleryAsync(userId);

        var updateResult = await _beveragesGalleryRepository.UpdateBatchCurrentStarBeveragesGalleryAsync(userId);

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

        Beverages newBeverage = await SumPowerBeveragesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchBeveragesGalleryAsync(string userId, List<Beverages> beverages)
    {
        var insertResult = await _beveragesGalleryRepository.InsertBatchBeveragesGalleryAsync(userId, beverages);

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

    public async Task<Beverages> GetBeverageCollectionByIdAsync(string userId, string beverageId)
    {
        var result = await _beveragesGalleryRepository.GetBeverageCollectionByIdAsync(userId, beverageId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBeverageGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _beveragesService.IsBeverageDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        await _beveragesGalleryRepository.UpdateBeverageGalleryPowerAsync(userId, Id, await _service.GetBeverageByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
