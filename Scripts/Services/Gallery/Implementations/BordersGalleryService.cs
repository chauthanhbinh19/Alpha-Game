using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BordersGalleryService : IBordersGalleryService
{
    private readonly IBordersGalleryRepository _bordersGalleryRepository;
    private readonly IBordersService _bordersService;
    private readonly IPowerManagerService _powerManagerService;

    public BordersGalleryService(
        IBordersGalleryRepository bordersGalleryRepository,
        IBordersService bordersService,
        IPowerManagerService powerManagerService)
    {
        _bordersGalleryRepository = bordersGalleryRepository;
        _bordersService = bordersService;
        _powerManagerService = powerManagerService;
    }

    public static IBordersGalleryService Create() => ServiceContainer.GetService<IBordersGalleryService>();

    public async Task<List<Borders>> GetBordersCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Borders> list = await _bordersGalleryRepository.GetBordersCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBordersCountAsync(string search, string rare)
    {
        return await _bordersGalleryRepository.GetBordersCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBorderGalleryAsync(string userId, string Id)
    {
        var checkResult = await _bordersService.IsBorderDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _bordersGalleryRepository.InsertBorderGalleryAsync(userId, Id, await _bordersService.GetBorderByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusBorderGalleryAsync(string userId, string borderId)
    {
        var checkResult = await _bordersService.IsBorderDeletedOrInactiveAsync(borderId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _bordersGalleryRepository.UpdateStatusBorderGalleryAsync(userId, borderId);

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
        Borders borderGallery = await GetBorderCollectionByIdAsync(userId, borderId) ?? new Borders();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)borderGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBordersGalleryAsync(string userId)
    {
        Borders oldBorder = await SumPowerBordersGalleryAsync(userId);

        var updateResult = await _bordersGalleryRepository.UpdateBatchStatusBordersGalleryAsync(userId);

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

        Borders newBorder = await SumPowerBordersGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBorder - (PowerManager)oldBorder;

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

    public async Task<Borders> SumPowerBordersGalleryAsync(string userId)
    {
        return await _bordersGalleryRepository.SumPowerBordersGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarBorderGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _bordersService.IsBorderDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _bordersGalleryRepository.UpdateTempStarBorderGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBorderGalleryAsync(string userId, string borderId)
    {
        var checkResult = await _bordersService.IsBorderDeletedOrInactiveAsync(borderId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Borders oldBorder = await GetBorderCollectionByIdAsync(userId, borderId) ?? new Borders();

        var updateResult = await _bordersGalleryRepository.UpdateCurrentStarBorderGalleryAsync(userId, borderId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Borders newBorder = await GetBorderCollectionByIdAsync(userId, borderId) ?? new Borders();
        PowerManager deltaPower = (PowerManager)newBorder - (PowerManager)oldBorder;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBordersGalleryAsync(string userId)
    {
        Borders oldBorder = await SumPowerBordersGalleryAsync(userId);

        var updateResult = await _bordersGalleryRepository.UpdateBatchCurrentStarBordersGalleryAsync(userId);

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

        Borders newBorder = await SumPowerBordersGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBorder - (PowerManager)oldBorder;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchBordersGalleryAsync(string userId, List<Borders> borders)
    {
        var insertResult = await _bordersGalleryRepository.InsertBatchBordersGalleryAsync(userId, borders);

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

    public async Task<Borders> GetBorderCollectionByIdAsync(string userId, string borderId)
    {
        var result = await _bordersGalleryRepository.GetBorderCollectionByIdAsync(userId, borderId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBorderGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _bordersService.IsBorderDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        await _bordersGalleryRepository.UpdateBorderGalleryPowerAsync(userId, Id, await _service.GetBorderByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
