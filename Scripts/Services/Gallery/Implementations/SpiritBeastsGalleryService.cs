using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpiritBeastsGalleryService : ISpiritBeastsGalleryService
{
    private readonly ISpiritBeastsGalleryRepository _spiritBeastsGalleryRepository;
    private readonly ISpiritBeastsService _spiritBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public SpiritBeastsGalleryService(
        ISpiritBeastsGalleryRepository spiritBeastsGalleryRepository,
        ISpiritBeastsService spiritBeastsService,
        IPowerManagerService powerManagerService)
    {
        _spiritBeastsGalleryRepository = spiritBeastsGalleryRepository;
        _spiritBeastsService = spiritBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static ISpiritBeastsGalleryService Create() => ServiceContainer.GetService<ISpiritBeastsGalleryService>();

    public async Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _spiritBeastsGalleryRepository.GetSpiritBeastsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string search, string rare)
    {
        return await _spiritBeastsGalleryRepository.GetSpiritBeastsCountAsync(search, rare);
    }

    public async Task<bool> InsertSpiritBeastGalleryAsync(string userId, string Id)
    {
        var insertResult = await _spiritBeastsGalleryRepository.InsertSpiritBeastGalleryAsync(userId, Id, await _spiritBeastsService.GetSpiritBeastByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusSpiritBeastGalleryAsync(string userId, string spiritBeastId)
    {
        var updateResult = await _spiritBeastsGalleryRepository.UpdateStatusSpiritBeastGalleryAsync(userId, spiritBeastId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        SpiritBeasts spiritBeastGallery = await GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId) ?? new SpiritBeasts();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)spiritBeastGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusSpiritBeastsGalleryAsync(string userId)
    {
        SpiritBeasts oldSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);

        var updateResult = await _spiritBeastsGalleryRepository.UpdateBatchStatusSpiritBeastsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        SpiritBeasts newSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync(string userId)
    {
        return await _spiritBeastsGalleryRepository.SumPowerSpiritBeastsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarSpiritBeastGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _spiritBeastsGalleryRepository.UpdateStarSpiritBeastGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarSpiritBeastGalleryAsync(string userId, string spiritBeastId)
    {
        SpiritBeasts oldSpiritBeast = await GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId) ?? new SpiritBeasts();

        var updateResult = await _spiritBeastsGalleryRepository.UpdateCurrentStarSpiritBeastGalleryAsync(userId, spiritBeastId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        SpiritBeasts newSpiritBeast = await GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId) ?? new SpiritBeasts();
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarSpiritBeastsGalleryAsync(string userId)
    {
        SpiritBeasts oldSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);

        var updateResult = await _spiritBeastsGalleryRepository.UpdateBatchCurrentStarSpiritBeastsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        SpiritBeasts newSpiritBeast = await SumPowerSpiritBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchSpiritBeastsGalleryAsync(string userId, List<SpiritBeasts> spiritBeasts)
    {
        var insertResult = await _spiritBeastsGalleryRepository.InsertBatchSpiritBeastsGalleryAsync(userId, spiritBeasts);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<SpiritBeasts> GetSpiritBeastCollectionByIdAsync(string userId, string spiritBeastId)
    {
        var result = await _spiritBeastsGalleryRepository.GetSpiritBeastCollectionByIdAsync(userId, spiritBeastId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateSpiritBeastGalleryPowerAsync(string userId, string Id)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _spiritBeastsGalleryRepository.UpdateSpiritBeastGalleryPowerAsync(userId, Id, await _service.GetSpiritBeastByIdAsync(Id));
    }
}
