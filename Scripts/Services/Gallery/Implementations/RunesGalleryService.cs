using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RunesGalleryService : IRunesGalleryService
{
    private readonly IRunesGalleryRepository _runesGalleryRepository;
    private readonly IRunesService _runesService;
    private readonly IPowerManagerService _powerManagerService;

    public RunesGalleryService(
        IRunesGalleryRepository runesGalleryRepository,
        IRunesService runesService,
        IPowerManagerService powerManagerService)
    {
        _runesGalleryRepository = runesGalleryRepository;
        _runesService = runesService;
        _powerManagerService = powerManagerService;
    }

    public static IRunesGalleryService Create() => ServiceContainer.GetService<IRunesGalleryService>();

    public async Task<List<Runes>> GetRunesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Runes> list = await _runesGalleryRepository.GetRunesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesCountAsync(string search, string rare)
    {
        return await _runesGalleryRepository.GetRunesCountAsync(search, rare);
    }

    public async Task<bool> InsertRuneGalleryAsync(string userId, string Id)
    {
        var insertResult = await _runesGalleryRepository.InsertRuneGalleryAsync(userId, Id, await _runesService.GetRuneByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusRuneGalleryAsync(string userId, string runeId)
    {
        var updateResult = await _runesGalleryRepository.UpdateStatusRuneGalleryAsync(userId, runeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Runes runeGallery = await GetRuneCollectionByIdAsync(userId, runeId) ?? new Runes();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)runeGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusRunesGalleryAsync(string userId)
    {
        Runes oldRune = await SumPowerRunesGalleryAsync(userId);

        var updateResult = await _runesGalleryRepository.UpdateBatchStatusRunesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Runes newRune = await SumPowerRunesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRune - (PowerManager)oldRune;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Runes> SumPowerRunesGalleryAsync(string userId)
    {
        return await _runesGalleryRepository.SumPowerRunesGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarRuneGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _runesGalleryRepository.UpdateStarRuneGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarRuneGalleryAsync(string userId, string runeId)
    {
        Runes oldRune = await GetRuneCollectionByIdAsync(userId, runeId) ?? new Runes();

        var updateResult = await _runesGalleryRepository.UpdateCurrentStarRuneGalleryAsync(userId, runeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Runes newRune = await GetRuneCollectionByIdAsync(userId, runeId) ?? new Runes();
        PowerManager deltaPower = (PowerManager)newRune - (PowerManager)oldRune;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarRunesGalleryAsync(string userId)
    {
        Runes oldRune = await SumPowerRunesGalleryAsync(userId);

        var updateResult = await _runesGalleryRepository.UpdateBatchCurrentStarRunesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Runes newRune = await SumPowerRunesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newRune - (PowerManager)oldRune;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchRunesGalleryAsync(string userId, List<Runes> runes)
    {
        var insertResult = await _runesGalleryRepository.InsertBatchRunesGalleryAsync(userId, runes);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Runes> GetRuneCollectionByIdAsync(string userId, string runeId)
    {
        var result = await _runesGalleryRepository.GetRuneCollectionByIdAsync(userId, runeId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateRuneGalleryPowerAsync(string userId, string Id)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        await _runesGalleryRepository.UpdateRuneGalleryPowerAsync(userId, Id, await _service.GetRuneByIdAsync(Id));
    }
}
