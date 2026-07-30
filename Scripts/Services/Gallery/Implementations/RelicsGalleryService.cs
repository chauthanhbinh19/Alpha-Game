using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RelicsGalleryService : IRelicsGalleryService
{
    private readonly IRelicsGalleryRepository _relicsGalleryRepository;
    private readonly IRelicsService _relicsService;
    private readonly IPowerManagerService _powerManagerService;

    public RelicsGalleryService(
        IRelicsGalleryRepository relicsGalleryRepository,
        IRelicsService relicsService,
        IPowerManagerService powerManagerService)
    {
        _relicsGalleryRepository = relicsGalleryRepository;
        _relicsService = relicsService;
        _powerManagerService = powerManagerService;
    }

    public static IRelicsGalleryService Create() => ServiceContainer.GetService<IRelicsGalleryService>();

    public async Task<List<Relics>> GetRelicsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _relicsGalleryRepository.GetRelicsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRelicsCountAsync(string search, string type, string rare)
    {
        return await _relicsGalleryRepository.GetRelicsCountAsync(search, type, rare);
    }

    public async Task<bool> InsertRelicGalleryAsync(string userId, string Id)
    {
        var insertResult = await _relicsGalleryRepository.InsertRelicGalleryAsync(userId, Id, await _relicsService.GetRelicByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusRelicGalleryAsync(string userId, string relicId)
    {
        var updateResult = await _relicsGalleryRepository.UpdateStatusRelicGalleryAsync(userId, relicId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Relics relicGallery = await GetRelicCollectionByIdAsync(userId, relicId) ?? new Relics();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)relicGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusRelicsGalleryAsync(string userId)
    {
        Relics oldRelic = await SumPowerRelicsGalleryAsync(userId);

        var updateResult = await _relicsGalleryRepository.UpdateBatchStatusRelicsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Relics newRelic = await SumPowerRelicsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Relics> SumPowerRelicsGalleryAsync(string userId)
    {
        return await _relicsGalleryRepository.SumPowerRelicsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarRelicGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _relicsGalleryRepository.UpdateStarRelicGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarRelicGalleryAsync(string userId, string relicId)
    {
        Relics oldRelic = await GetRelicCollectionByIdAsync(userId, relicId) ?? new Relics();

        var updateResult = await _relicsGalleryRepository.UpdateCurrentStarRelicGalleryAsync(userId, relicId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Relics newRelic = await GetRelicCollectionByIdAsync(userId, relicId) ?? new Relics();
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarRelicsGalleryAsync(string userId)
    {
        Relics oldRelic = await SumPowerRelicsGalleryAsync(userId);

        var updateResult = await _relicsGalleryRepository.UpdateBatchCurrentStarRelicsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Relics newRelic = await SumPowerRelicsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchRelicsGalleryAsync(string userId, List<Relics> relics)
    {
        var insertResult = await _relicsGalleryRepository.InsertBatchRelicsGalleryAsync(userId, relics);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Relics> GetRelicCollectionByIdAsync(string userId, string relicId)
    {
        var result = await _relicsGalleryRepository.GetRelicCollectionByIdAsync(userId, relicId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateRelicGalleryPowerAsync(string userId, string Id)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        await _relicsGalleryRepository.UpdateRelicGalleryPowerAsync(userId, Id, await _service.GetRelicByIdAsync(Id));
    }
}
