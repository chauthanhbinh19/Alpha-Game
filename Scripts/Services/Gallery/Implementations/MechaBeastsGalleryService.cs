using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MechaBeastsGalleryService : IMechaBeastsGalleryService
{
    private readonly IMechaBeastsGalleryRepository _mechaBeastsGalleryRepository;
    private readonly IMechaBeastsService _mechaBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public MechaBeastsGalleryService(
        IMechaBeastsGalleryRepository mechaBeastsGalleryRepository,
        IMechaBeastsService mechaBeastsService,
        IPowerManagerService powerManagerService)
    {
        _mechaBeastsGalleryRepository = mechaBeastsGalleryRepository;
        _mechaBeastsService = mechaBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static IMechaBeastsGalleryService Create() => ServiceContainer.GetService<IMechaBeastsGalleryService>();

    public async Task<List<MechaBeasts>> GetMechaBeastsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _mechaBeastsGalleryRepository.GetMechaBeastsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMechaBeastsCountAsync(string search, string rare)
    {
        return await _mechaBeastsGalleryRepository.GetMechaBeastsCountAsync(search, rare);
    }

    public async Task<bool> InsertMechaBeastGalleryAsync(string userId, string Id)
    {
        var insertResult = await _mechaBeastsGalleryRepository.InsertMechaBeastGalleryAsync(userId, Id, await _mechaBeastsService.GetMechaBeastByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusMechaBeastGalleryAsync(string userId, string mechaBeastId)
    {
        var updateResult = await _mechaBeastsGalleryRepository.UpdateStatusMechaBeastGalleryAsync(userId, mechaBeastId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        MechaBeasts mechaBeastGallery = await GetMechaBeastCollectionByIdAsync(userId, mechaBeastId) ?? new MechaBeasts();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)mechaBeastGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusMechaBeastsGalleryAsync(string userId)
    {
        MechaBeasts oldMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);

        var updateResult = await _mechaBeastsGalleryRepository.UpdateBatchStatusMechaBeastsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        MechaBeasts newMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<MechaBeasts> SumPowerMechaBeastsGalleryAsync(string userId)
    {
        return await _mechaBeastsGalleryRepository.SumPowerMechaBeastsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarMechaBeastGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _mechaBeastsGalleryRepository.UpdateStarMechaBeastGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarMechaBeastGalleryAsync(string userId, string mechaBeastId)
    {
        MechaBeasts oldMechaBeast = await GetMechaBeastCollectionByIdAsync(userId, mechaBeastId) ?? new MechaBeasts();

        var updateResult = await _mechaBeastsGalleryRepository.UpdateCurrentStarMechaBeastGalleryAsync(userId, mechaBeastId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        MechaBeasts newMechaBeast = await GetMechaBeastCollectionByIdAsync(userId, mechaBeastId) ?? new MechaBeasts();
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarMechaBeastsGalleryAsync(string userId)
    {
        MechaBeasts oldMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);

        var updateResult = await _mechaBeastsGalleryRepository.UpdateBatchCurrentStarMechaBeastsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        MechaBeasts newMechaBeast = await SumPowerMechaBeastsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchMechaBeastsGalleryAsync(string userId, List<MechaBeasts> mechaBeasts)
    {
        var insertResult = await _mechaBeastsGalleryRepository.InsertBatchMechaBeastsGalleryAsync(userId, mechaBeasts);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<MechaBeasts> GetMechaBeastCollectionByIdAsync(string userId, string mechaBeastId)
    {
        var result = await _mechaBeastsGalleryRepository.GetMechaBeastCollectionByIdAsync(userId, mechaBeastId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateMechaBeastGalleryPowerAsync(string userId, string Id)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        await _mechaBeastsGalleryRepository.UpdateMechaBeastGalleryPowerAsync(userId, Id, await _service.GetMechaBeastByIdAsync(Id));
    }
}
