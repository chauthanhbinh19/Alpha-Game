using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BuildingsGalleryService : IBuildingsGalleryService
{
    private readonly IBuildingsGalleryRepository _buildingsGalleryRepository;
    private readonly IBuildingsService _buildingsService;
    private readonly IPowerManagerService _powerManagerService;

    public BuildingsGalleryService(
        IBuildingsGalleryRepository buildingsGalleryRepository,
        IBuildingsService buildingsService,
        IPowerManagerService powerManagerService)
    {
        _buildingsGalleryRepository = buildingsGalleryRepository;
        _buildingsService = buildingsService;
        _powerManagerService = powerManagerService;
    }

    public static IBuildingsGalleryService Create() => ServiceContainer.GetService<IBuildingsGalleryService>();

    public async Task<List<Buildings>> GetBuildingsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> list = await _buildingsGalleryRepository.GetBuildingsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBuildingsCountAsync(string search, string type, string rare)
    {
        return await _buildingsGalleryRepository.GetBuildingsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBuildingGalleryAsync(string userId, string Id)
    {
        var checkResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _buildingsGalleryRepository.InsertBuildingGalleryAsync(userId, Id, await _buildingsService.GetBuildingByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusBuildingGalleryAsync(string userId, string buildingId)
    {
        var checkResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(buildingId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _buildingsGalleryRepository.UpdateStatusBuildingGalleryAsync(userId, buildingId);

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
        Buildings buildingGallery = await GetBuildingCollectionByIdAsync(userId, buildingId) ?? new Buildings();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)buildingGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBuildingsGalleryAsync(string userId)
    {
        Buildings oldBuilding = await SumPowerBuildingsGalleryAsync(userId);

        var updateResult = await _buildingsGalleryRepository.UpdateBatchStatusBuildingsGalleryAsync(userId);

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

        Buildings newBuilding = await SumPowerBuildingsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

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

    public async Task<Buildings> SumPowerBuildingsGalleryAsync(string userId)
    {
        return await _buildingsGalleryRepository.SumPowerBuildingsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarBuildingGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _buildingsGalleryRepository.UpdateTempStarBuildingGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBuildingGalleryAsync(string userId, string buildingId)
    {
        var checkResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(buildingId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Buildings oldBuilding = await GetBuildingCollectionByIdAsync(userId, buildingId) ?? new Buildings();

        var updateResult = await _buildingsGalleryRepository.UpdateCurrentStarBuildingGalleryAsync(userId, buildingId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Buildings newBuilding = await GetBuildingCollectionByIdAsync(userId, buildingId) ?? new Buildings();
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBuildingsGalleryAsync(string userId)
    {
        Buildings oldBuilding = await SumPowerBuildingsGalleryAsync(userId);

        var updateResult = await _buildingsGalleryRepository.UpdateBatchCurrentStarBuildingsGalleryAsync(userId);

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

        Buildings newBuilding = await SumPowerBuildingsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchBuildingsGalleryAsync(string userId, List<Buildings> buildings)
    {
        var insertResult = await _buildingsGalleryRepository.InsertBatchBuildingsGalleryAsync(userId, buildings);

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

    public async Task<Buildings> GetBuildingCollectionByIdAsync(string userId, string buildingId)
    {
        var result = await _buildingsGalleryRepository.GetBuildingCollectionByIdAsync(userId, buildingId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBuildingGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _buildingsService.IsBuildingDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        await _buildingsGalleryRepository.UpdateBuildingGalleryPowerAsync(userId, Id, await _service.GetBuildingByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
