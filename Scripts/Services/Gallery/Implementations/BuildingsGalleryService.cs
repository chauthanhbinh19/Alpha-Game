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

    public async Task<bool> InsertBuildingGalleryAsync(string userId, string Id)
    {
        var insertResult = await _buildingsGalleryRepository.InsertBuildingGalleryAsync(userId, Id, await _buildingsService.GetBuildingByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusBuildingGalleryAsync(string userId, string buildingId)
    {
        var updateResult = await _buildingsGalleryRepository.UpdateStatusBuildingGalleryAsync(userId, buildingId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Buildings buildingGallery = await GetBuildingCollectionByIdAsync(userId, buildingId) ?? new Buildings();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)buildingGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusBuildingsGalleryAsync(string userId)
    {
        Buildings oldBuilding = await SumPowerBuildingsGalleryAsync(userId);

        var updateResult = await _buildingsGalleryRepository.UpdateBatchStatusBuildingsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Buildings newBuilding = await SumPowerBuildingsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Buildings> SumPowerBuildingsGalleryAsync(string userId)
    {
        return await _buildingsGalleryRepository.SumPowerBuildingsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarBuildingGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _buildingsGalleryRepository.UpdateTempStarBuildingGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarBuildingGalleryAsync(string userId, string buildingId)
    {
        Buildings oldBuilding = await GetBuildingCollectionByIdAsync(userId, buildingId) ?? new Buildings();

        var updateResult = await _buildingsGalleryRepository.UpdateCurrentStarBuildingGalleryAsync(userId, buildingId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Buildings newBuilding = await GetBuildingCollectionByIdAsync(userId, buildingId) ?? new Buildings();
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarBuildingsGalleryAsync(string userId)
    {
        Buildings oldBuilding = await SumPowerBuildingsGalleryAsync(userId);

        var updateResult = await _buildingsGalleryRepository.UpdateBatchCurrentStarBuildingsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Buildings newBuilding = await SumPowerBuildingsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBuilding - (PowerManager)oldBuilding;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchBuildingsGalleryAsync(string userId, List<Buildings> buildings)
    {
        var insertResult = await _buildingsGalleryRepository.InsertBatchBuildingsGalleryAsync(userId, buildings);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Buildings> GetBuildingCollectionByIdAsync(string userId, string buildingId)
    {
        var result = await _buildingsGalleryRepository.GetBuildingCollectionByIdAsync(userId, buildingId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateBuildingGalleryPowerAsync(string userId, string Id)
    {
        IBuildingsRepository _repository = new BuildingsRepository();
        BuildingsService _service = new BuildingsService(_repository);
        await _buildingsGalleryRepository.UpdateBuildingGalleryPowerAsync(userId, Id, await _service.GetBuildingByIdAsync(Id));
    }
}
