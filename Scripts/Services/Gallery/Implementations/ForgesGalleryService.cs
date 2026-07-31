using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class ForgesGalleryService : IForgesGalleryService
{
    private readonly IForgesGalleryRepository _forgesGalleryRepository;
    private readonly IForgesService _forgesService;
    private readonly IPowerManagerService _powerManagerService;

    public ForgesGalleryService(
        IForgesGalleryRepository forgesGalleryRepository,
        IForgesService forgesService,
        IPowerManagerService powerManagerService)
    {
        _forgesGalleryRepository = forgesGalleryRepository;
        _forgesService = forgesService;
        _powerManagerService = powerManagerService;
    }

    public static IForgesGalleryService Create() => ServiceContainer.GetService<IForgesGalleryService>();

    public async Task<List<Forges>> GetForgesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _forgesGalleryRepository.GetForgesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesCountAsync(string search, string type, string rare)
    {
        return await _forgesGalleryRepository.GetForgesCountAsync(search, type, rare);
    }

    public async Task<bool> InsertForgeGalleryAsync(string userId, string Id)
    {
        var insertResult = await _forgesGalleryRepository.InsertForgeGalleryAsync(userId, Id, await _forgesService.GetForgeByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusForgeGalleryAsync(string userId, string forgeId)
    {
        var updateResult = await _forgesGalleryRepository.UpdateStatusForgeGalleryAsync(userId, forgeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Forges forgeGallery = await GetForgeCollectionByIdAsync(userId, forgeId) ?? new Forges();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)forgeGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusForgesGalleryAsync(string userId)
    {
        Forges oldForge = await SumPowerForgesGalleryAsync(userId);

        var updateResult = await _forgesGalleryRepository.UpdateBatchStatusForgesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Forges newForge = await SumPowerForgesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newForge - (PowerManager)oldForge;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Forges> SumPowerForgesGalleryAsync(string userId)
    {
        return await _forgesGalleryRepository.SumPowerForgesGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarForgeGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _forgesGalleryRepository.UpdateTempStarForgeGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarForgeGalleryAsync(string userId, string forgeId)
    {
        Forges oldForge = await GetForgeCollectionByIdAsync(userId, forgeId) ?? new Forges();

        var updateResult = await _forgesGalleryRepository.UpdateCurrentStarForgeGalleryAsync(userId, forgeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Forges newForge = await GetForgeCollectionByIdAsync(userId, forgeId) ?? new Forges();
        PowerManager deltaPower = (PowerManager)newForge - (PowerManager)oldForge;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarForgesGalleryAsync(string userId)
    {
        Forges oldForge = await SumPowerForgesGalleryAsync(userId);

        var updateResult = await _forgesGalleryRepository.UpdateBatchCurrentStarForgesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Forges newForge = await SumPowerForgesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newForge - (PowerManager)oldForge;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchForgesGalleryAsync(string userId, List<Forges> forges)
    {
        var insertResult = await _forgesGalleryRepository.InsertBatchForgesGalleryAsync(userId, forges);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Forges> GetForgeCollectionByIdAsync(string userId, string forgeId)
    {
        var result = await _forgesGalleryRepository.GetForgeCollectionByIdAsync(userId, forgeId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateForgeGalleryPowerAsync(string userId, string Id)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        await _forgesGalleryRepository.UpdateForgeGalleryPowerAsync(userId, Id, await _service.GetForgeByIdAsync(Id));
    }
}
