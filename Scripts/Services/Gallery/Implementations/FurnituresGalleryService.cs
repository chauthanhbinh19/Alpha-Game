using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FurnituresGalleryService : IFurnituresGalleryService
{
    private readonly IFurnituresGalleryRepository _furnituresGalleryRepository;
    private readonly IFurnituresService _furnituresService;
    private readonly IPowerManagerService _powerManagerService;

    public FurnituresGalleryService(
        IFurnituresGalleryRepository furnituresGalleryRepository,
        IFurnituresService furnituresService,
        IPowerManagerService powerManagerService)
    {
        _furnituresGalleryRepository = furnituresGalleryRepository;
        _furnituresService = furnituresService;
        _powerManagerService = powerManagerService;
    }

    public static IFurnituresGalleryService Create() => ServiceContainer.GetService<IFurnituresGalleryService>();

    public async Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _furnituresGalleryRepository.GetFurnituresCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string search, string type, string rare)
    {
        return await _furnituresGalleryRepository.GetFurnituresCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertFurnitureGalleryAsync(string userId, string Id)
    {
        var checkResult = await _furnituresService.IsFurnitureDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _furnituresGalleryRepository.InsertFurnitureGalleryAsync(userId, Id, await _furnituresService.GetFurnitureByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusFurnitureGalleryAsync(string userId, string furnitureId)
    {
        var checkResult = await _furnituresService.IsFurnitureDeletedOrInactiveAsync(furnitureId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _furnituresGalleryRepository.UpdateStatusFurnitureGalleryAsync(userId, furnitureId);

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
        Furnitures furnitureGallery = await GetFurnitureCollectionByIdAsync(userId, furnitureId) ?? new Furnitures();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)furnitureGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFurnituresGalleryAsync(string userId)
    {
        Furnitures oldFurniture = await SumPowerFurnituresGalleryAsync(userId);

        var updateResult = await _furnituresGalleryRepository.UpdateBatchStatusFurnituresGalleryAsync(userId);

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

        Furnitures newFurniture = await SumPowerFurnituresGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

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

    public async Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId)
    {
        return await _furnituresGalleryRepository.SumPowerFurnituresGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarFurnitureGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _furnituresService.IsFurnitureDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _furnituresGalleryRepository.UpdateTempStarFurnitureGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarFurnitureGalleryAsync(string userId, string furnitureId)
    {
        var checkResult = await _furnituresService.IsFurnitureDeletedOrInactiveAsync(furnitureId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Furnitures oldFurniture = await GetFurnitureCollectionByIdAsync(userId, furnitureId) ?? new Furnitures();

        var updateResult = await _furnituresGalleryRepository.UpdateCurrentStarFurnitureGalleryAsync(userId, furnitureId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Furnitures newFurniture = await GetFurnitureCollectionByIdAsync(userId, furnitureId) ?? new Furnitures();
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarFurnituresGalleryAsync(string userId)
    {
        Furnitures oldFurniture = await SumPowerFurnituresGalleryAsync(userId);

        var updateResult = await _furnituresGalleryRepository.UpdateBatchCurrentStarFurnituresGalleryAsync(userId);

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

        Furnitures newFurniture = await SumPowerFurnituresGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchFurnituresGalleryAsync(string userId, List<Furnitures> furnitures)
    {
        var insertResult = await _furnituresGalleryRepository.InsertBatchFurnituresGalleryAsync(userId, furnitures);

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

    public async Task<Furnitures> GetFurnitureCollectionByIdAsync(string userId, string furnitureId)
    {
        var result = await _furnituresGalleryRepository.GetFurnitureCollectionByIdAsync(userId, furnitureId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateFurnitureGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _furnituresService.IsFurnitureDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _furnituresGalleryRepository.UpdateFurnitureGalleryPowerAsync(userId, Id, await _service.GetFurnitureByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
