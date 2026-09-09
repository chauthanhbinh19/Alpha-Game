using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OutfitsGalleryService : IOutfitsGalleryService
{
    private readonly IOutfitsGalleryRepository _outfitsGalleryRepository;
    private readonly IOutfitsService _outfitsService;
    private readonly IPowerManagerService _powerManagerService;

    public OutfitsGalleryService(
        IOutfitsGalleryRepository outfitsGalleryRepository,
        IOutfitsService outfitsService,
        IPowerManagerService powerManagerService)
    {
        _outfitsGalleryRepository = outfitsGalleryRepository;
        _outfitsService = outfitsService;
        _powerManagerService = powerManagerService;
    }

    public static IOutfitsGalleryService Create() => ServiceContainer.GetService<IOutfitsGalleryService>();

    public async Task<List<Outfits>> GetOutfitsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> list = await _outfitsGalleryRepository.GetOutfitsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetOutfitsCountAsync(string search, string type, string rare)
    {
        return await _outfitsGalleryRepository.GetOutfitsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOutfitGalleryAsync(string userId, string Id)
    {
        var checkResult = await _outfitsService.IsOutfitDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _outfitsGalleryRepository.InsertOutfitGalleryAsync(userId, Id, await _outfitsService.GetOutfitByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusOutfitGalleryAsync(string userId, string outfitId)
    {
        var checkResult = await _outfitsService.IsOutfitDeletedOrInactiveAsync(outfitId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _outfitsGalleryRepository.UpdateStatusOutfitGalleryAsync(userId, outfitId);

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
        Outfits outfitGallery = await GetOutfitCollectionByIdAsync(userId, outfitId) ?? new Outfits();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)outfitGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusOutfitsGalleryAsync(string userId)
    {
        Outfits oldOutfit = await SumPowerOutfitsGalleryAsync(userId);

        var updateResult = await _outfitsGalleryRepository.UpdateBatchStatusOutfitsGalleryAsync(userId);

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

        Outfits newOutfit = await SumPowerOutfitsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newOutfit - (PowerManager)oldOutfit;

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

    public async Task<Outfits> SumPowerOutfitsGalleryAsync(string userId)
    {
        return await _outfitsGalleryRepository.SumPowerOutfitsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarOutfitGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _outfitsService.IsOutfitDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _outfitsGalleryRepository.UpdateTempStarOutfitGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarOutfitGalleryAsync(string userId, string outfitId)
    {
        var checkResult = await _outfitsService.IsOutfitDeletedOrInactiveAsync(outfitId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Outfits oldOutfit = await GetOutfitCollectionByIdAsync(userId, outfitId) ?? new Outfits();

        var updateResult = await _outfitsGalleryRepository.UpdateCurrentStarOutfitGalleryAsync(userId, outfitId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Outfits newOutfit = await GetOutfitCollectionByIdAsync(userId, outfitId) ?? new Outfits();
        PowerManager deltaPower = (PowerManager)newOutfit - (PowerManager)oldOutfit;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarOutfitsGalleryAsync(string userId)
    {
        Outfits oldOutfit = await SumPowerOutfitsGalleryAsync(userId);

        var updateResult = await _outfitsGalleryRepository.UpdateBatchCurrentStarOutfitsGalleryAsync(userId);

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

        Outfits newOutfit = await SumPowerOutfitsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newOutfit - (PowerManager)oldOutfit;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchOutfitsGalleryAsync(string userId, List<Outfits> outfits)
    {
        var insertResult = await _outfitsGalleryRepository.InsertBatchOutfitsGalleryAsync(userId, outfits);

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

    public async Task<Outfits> GetOutfitCollectionByIdAsync(string userId, string outfitId)
    {
        var result = await _outfitsGalleryRepository.GetOutfitCollectionByIdAsync(userId, outfitId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateOutfitGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _outfitsService.IsOutfitDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IOutfitsRepository _repository = new OutfitsRepository();
        OutfitsService _service = new OutfitsService(_repository);
        await _outfitsGalleryRepository.UpdateOutfitGalleryPowerAsync(userId, Id, await _service.GetOutfitByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
