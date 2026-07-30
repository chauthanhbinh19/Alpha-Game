using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PlantsGalleryService : IPlantsGalleryService
{
    private readonly IPlantsGalleryRepository _plantsGalleryRepository;
    private readonly IPlantsService _plantsService;
    private readonly IPowerManagerService _powerManagerService;

    public PlantsGalleryService(
        IPlantsGalleryRepository plantsGalleryRepository,
        IPlantsService plantsService,
        IPowerManagerService powerManagerService)
    {
        _plantsGalleryRepository = plantsGalleryRepository;
        _plantsService = plantsService;
        _powerManagerService = powerManagerService;
    }

    public static IPlantsGalleryService Create() => ServiceContainer.GetService<IPlantsGalleryService>();

    public async Task<List<Plants>> GetPlantsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Plants> list = await _plantsGalleryRepository.GetPlantsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPlantsCountAsync(string search, string rare)
    {
        return await _plantsGalleryRepository.GetPlantsCountAsync(search, rare);
    }

    public async Task<bool> InsertPlantGalleryAsync(string userId, string Id)
    {
        var insertResult = await _plantsGalleryRepository.InsertPlantGalleryAsync(userId, Id, await _plantsService.GetPlantByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusPlantGalleryAsync(string userId, string plantId)
    {
        var updateResult = await _plantsGalleryRepository.UpdateStatusPlantGalleryAsync(userId, plantId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Plants plantGallery = await GetPlantCollectionByIdAsync(userId, plantId) ?? new Plants();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)plantGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusPlantsGalleryAsync(string userId)
    {
        Plants oldPlant = await SumPowerPlantsGalleryAsync(userId);

        var updateResult = await _plantsGalleryRepository.UpdateBatchStatusPlantsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Plants newPlant = await SumPowerPlantsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newPlant - (PowerManager)oldPlant;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Plants> SumPowerPlantsGalleryAsync(string userId)
    {
        return await _plantsGalleryRepository.SumPowerPlantsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarPlantGalleryAsync(string userId, string Id, double star)
    {
        var insertResult = await _plantsGalleryRepository.UpdateStarPlantGalleryAsync(userId, Id, star);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarPlantGalleryAsync(string userId, string plantId)
    {
        Plants oldPlant = await GetPlantCollectionByIdAsync(userId, plantId) ?? new Plants();

        var updateResult = await _plantsGalleryRepository.UpdateCurrentStarPlantGalleryAsync(userId, plantId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Plants newPlant = await GetPlantCollectionByIdAsync(userId, plantId) ?? new Plants();
        PowerManager deltaPower = (PowerManager)newPlant - (PowerManager)oldPlant;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarPlantsGalleryAsync(string userId)
    {
        Plants oldPlant = await SumPowerPlantsGalleryAsync(userId);

        var updateResult = await _plantsGalleryRepository.UpdateBatchCurrentStarPlantsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Plants newPlant = await SumPowerPlantsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newPlant - (PowerManager)oldPlant;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchPlantsGalleryAsync(string userId, List<Plants> plants)
    {
        var insertResult = await _plantsGalleryRepository.InsertBatchPlantsGalleryAsync(userId, plants);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Plants> GetPlantCollectionByIdAsync(string userId, string plantId)
    {
        var result = await _plantsGalleryRepository.GetPlantCollectionByIdAsync(userId, plantId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdatePlantGalleryPowerAsync(string userId, string Id)
    {
        IPlantsRepository _repository = new PlantsRepository();
        PlantsService _service = new PlantsService(_repository);
        await _plantsGalleryRepository.UpdatePlantGalleryPowerAsync(userId, Id, await _service.GetPlantByIdAsync(Id));
    }
}
