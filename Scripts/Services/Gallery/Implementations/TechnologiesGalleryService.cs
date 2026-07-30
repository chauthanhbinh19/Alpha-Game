using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TechnologiesGalleryService : ITechnologiesGalleryService
{
    private readonly ITechnologiesGalleryRepository _technologiesGalleryRepository;
    private readonly ITechnologiesService _technologiesService;
    private readonly IPowerManagerService _powerManagerService;

    public TechnologiesGalleryService(
        ITechnologiesGalleryRepository technologiesGalleryRepository,
        ITechnologiesService technologiesService,
        IPowerManagerService powerManagerService)
    {
        _technologiesGalleryRepository = technologiesGalleryRepository;
        _technologiesService = technologiesService;
        _powerManagerService = powerManagerService;
    }

    public static ITechnologiesGalleryService Create() => ServiceContainer.GetService<ITechnologiesGalleryService>();

    public async Task<List<Technologies>> GetTechnologiesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _technologiesGalleryRepository.GetTechnologiesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string search, string rare)
    {
        return await _technologiesGalleryRepository.GetTechnologiesCountAsync(search, rare);
    }

    public async Task<bool> InsertTechnologyGalleryAsync(string userId, string Id)
    {
        var insertResult = await _technologiesGalleryRepository.InsertTechnologyGalleryAsync(userId, Id, await _technologiesService.GetTechnologyByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusTechnologyGalleryAsync(string userId, string technologyId)
    {
        var updateResult = await _technologiesGalleryRepository.UpdateStatusTechnologyGalleryAsync(userId, technologyId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Technologies technologyGallery = await GetTechnologyCollectionByIdAsync(userId, technologyId) ?? new Technologies();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)technologyGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusTechnologiesGalleryAsync(string userId)
    {
        Technologies oldTechnology = await SumPowerTechnologiesGalleryAsync(userId);

        var updateResult = await _technologiesGalleryRepository.UpdateBatchStatusTechnologiesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Technologies newTechnology = await SumPowerTechnologiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Technologies> SumPowerTechnologiesGalleryAsync(string userId)
    {
        return await _technologiesGalleryRepository.SumPowerTechnologiesGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarTechnologyGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _technologiesGalleryRepository.UpdateStarTechnologyGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarTechnologyGalleryAsync(string userId, string technologyId)
    {
        Technologies oldTechnology = await GetTechnologyCollectionByIdAsync(userId, technologyId) ?? new Technologies();

        var updateResult = await _technologiesGalleryRepository.UpdateCurrentStarTechnologyGalleryAsync(userId, technologyId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Technologies newTechnology = await GetTechnologyCollectionByIdAsync(userId, technologyId) ?? new Technologies();
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarTechnologiesGalleryAsync(string userId)
    {
        Technologies oldTechnology = await SumPowerTechnologiesGalleryAsync(userId);

        var updateResult = await _technologiesGalleryRepository.UpdateBatchCurrentStarTechnologiesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Technologies newTechnology = await SumPowerTechnologiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchTechnologiesGalleryAsync(string userId, List<Technologies> technologies)
    {
        var insertResult = await _technologiesGalleryRepository.InsertBatchTechnologiesGalleryAsync(userId, technologies);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Technologies> GetTechnologyCollectionByIdAsync(string userId, string technologyId)
    {
        var result = await _technologiesGalleryRepository.GetTechnologyCollectionByIdAsync(userId, technologyId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateTechnologyGalleryPowerAsync(string userId, string Id)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        await _technologiesGalleryRepository.UpdateTechnologyGalleryPowerAsync(userId, Id, await _service.GetTechnologyByIdAsync(Id));
    }
}
