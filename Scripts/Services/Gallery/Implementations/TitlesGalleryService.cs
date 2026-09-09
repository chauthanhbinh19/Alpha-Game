using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TitlesGalleryService : ITitlesGalleryService
{
    private readonly ITitlesGalleryRepository _titlesGalleryRepository;
    private readonly ITitlesService _titlesService;
    private readonly IPowerManagerService _powerManagerService;

    public TitlesGalleryService(
        ITitlesGalleryRepository titlesGalleryRepository,
        ITitlesService titlesService,
        IPowerManagerService powerManagerService)
    {
        _titlesGalleryRepository = titlesGalleryRepository;
        _titlesService = titlesService;
        _powerManagerService = powerManagerService;
    }

    public static ITitlesGalleryService Create() => ServiceContainer.GetService<ITitlesGalleryService>();

    public async Task<List<Titles>> GetTitlesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _titlesGalleryRepository.GetTitlesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTitlesCountAsync(string search, string rare)
    {
        return await _titlesGalleryRepository.GetTitlesCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertTitleGalleryAsync(string userId, string Id)
    {
        var checkResult = await _titlesService.IsTitleDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _titlesGalleryRepository.InsertTitleGalleryAsync(userId, Id, await _titlesService.GetTitleByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusTitleGalleryAsync(string userId, string titleId)
    {
        var checkResult = await _titlesService.IsTitleDeletedOrInactiveAsync(titleId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _titlesGalleryRepository.UpdateStatusTitleGalleryAsync(userId, titleId);

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
        Titles titleGallery = await GetTitleCollectionByIdAsync(userId, titleId) ?? new Titles();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)titleGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTitlesGalleryAsync(string userId)
    {
        Titles oldTitle = await SumPowerTitlesGalleryAsync(userId);

        var updateResult = await _titlesGalleryRepository.UpdateBatchStatusTitlesGalleryAsync(userId);

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

        Titles newTitle = await SumPowerTitlesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

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

    public async Task<Titles> SumPowerTitlesGalleryAsync(string userId)
    {
        return await _titlesGalleryRepository.SumPowerTitlesGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarTitleGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _titlesService.IsTitleDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _titlesGalleryRepository.UpdateTempStarTitleGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarTitleGalleryAsync(string userId, string titleId)
    {
        var checkResult = await _titlesService.IsTitleDeletedOrInactiveAsync(titleId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Titles oldTitle = await GetTitleCollectionByIdAsync(userId, titleId) ?? new Titles();

        var updateResult = await _titlesGalleryRepository.UpdateCurrentStarTitleGalleryAsync(userId, titleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Titles newTitle = await GetTitleCollectionByIdAsync(userId, titleId) ?? new Titles();
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarTitlesGalleryAsync(string userId)
    {
        Titles oldTitle = await SumPowerTitlesGalleryAsync(userId);

        var updateResult = await _titlesGalleryRepository.UpdateBatchCurrentStarTitlesGalleryAsync(userId);

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

        Titles newTitle = await SumPowerTitlesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchTitlesGalleryAsync(string userId, List<Titles> titles)
    {
        var insertResult = await _titlesGalleryRepository.InsertBatchTitlesGalleryAsync(userId, titles);

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

    public async Task<Titles> GetTitleCollectionByIdAsync(string userId, string titleId)
    {
        var result = await _titlesGalleryRepository.GetTitleCollectionByIdAsync(userId, titleId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTitleGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _titlesService.IsTitleDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        await _titlesGalleryRepository.UpdateTitleGalleryPowerAsync(userId, Id, await _service.GetTitleByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
