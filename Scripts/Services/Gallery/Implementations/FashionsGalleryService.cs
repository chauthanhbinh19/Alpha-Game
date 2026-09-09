using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FashionsGalleryService : IFashionsGalleryService
{
    private readonly IFashionsGalleryRepository _fashionsGalleryRepository;
    private readonly IFashionsService _fashionsService;
    private readonly IPowerManagerService _powerManagerService;

    public FashionsGalleryService(
        IFashionsGalleryRepository fashionsGalleryRepository,
        IFashionsService fashionsService,
        IPowerManagerService powerManagerService)
    {
        _fashionsGalleryRepository = fashionsGalleryRepository;
        _fashionsService = fashionsService;
        _powerManagerService = powerManagerService;
    }

    public static IFashionsGalleryService Create() => ServiceContainer.GetService<IFashionsGalleryService>();

    public async Task<List<Fashions>> GetFashionsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _fashionsGalleryRepository.GetFashionsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsCountAsync(string search, string type, string rare)
    {
        return await _fashionsGalleryRepository.GetFashionsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertFashionGalleryAsync(string userId, string Id)
    {
        var checkResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _fashionsGalleryRepository.InsertFashionGalleryAsync(userId, Id, await _fashionsService.GetFashionByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusFashionGalleryAsync(string userId, string fashionId)
    {
        var checkResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(fashionId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _fashionsGalleryRepository.UpdateStatusFashionGalleryAsync(userId, fashionId);

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
        Fashions fashionGallery = await GetFashionCollectionByIdAsync(userId, fashionId) ?? new Fashions();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)fashionGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFashionsGalleryAsync(string userId)
    {
        Fashions oldFashion = await SumPowerFashionsGalleryAsync(userId);

        var updateResult = await _fashionsGalleryRepository.UpdateBatchStatusFashionsGalleryAsync(userId);

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

        Fashions newFashion = await SumPowerFashionsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

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

    public async Task<Fashions> SumPowerFashionsGalleryAsync(string userId)
    {
        return await _fashionsGalleryRepository.SumPowerFashionsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarFashionGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _fashionsGalleryRepository.UpdateTempStarFashionGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarFashionGalleryAsync(string userId, string fashionId)
    {
        var checkResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(fashionId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Fashions oldFashion = await GetFashionCollectionByIdAsync(userId, fashionId) ?? new Fashions();

        var updateResult = await _fashionsGalleryRepository.UpdateCurrentStarFashionGalleryAsync(userId, fashionId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Fashions newFashion = await GetFashionCollectionByIdAsync(userId, fashionId) ?? new Fashions();
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarFashionsGalleryAsync(string userId)
    {
        Fashions oldFashion = await SumPowerFashionsGalleryAsync(userId);

        var updateResult = await _fashionsGalleryRepository.UpdateBatchCurrentStarFashionsGalleryAsync(userId);

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

        Fashions newFashion = await SumPowerFashionsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchFashionsGalleryAsync(string userId, List<Fashions> fashions)
    {
        var insertResult = await _fashionsGalleryRepository.InsertBatchFashionsGalleryAsync(userId, fashions);

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

    public async Task<Fashions> GetFashionCollectionByIdAsync(string userId, string fashionId)
    {
        var result = await _fashionsGalleryRepository.GetFashionCollectionByIdAsync(userId, fashionId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateFashionGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _fashionsService.IsFashionDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        await _fashionsGalleryRepository.UpdateFashionGalleryPowerAsync(userId, Id, await _service.GetFashionByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
