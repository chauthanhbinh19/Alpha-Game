using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MedalsGalleryService : IMedalsGalleryService
{
    private readonly IMedalsGalleryRepository _medalsGalleryRepository;
    private readonly IMedalsService _medalsService;
    private readonly IPowerManagerService _powerManagerService;

    public MedalsGalleryService(
        IMedalsGalleryRepository medalsGalleryRepository,
        IMedalsService medalsService,
        IPowerManagerService powerManagerService)
    {
        _medalsGalleryRepository = medalsGalleryRepository;
        _medalsService = medalsService;
        _powerManagerService = powerManagerService;
    }

    public static IMedalsGalleryService Create() => ServiceContainer.GetService<IMedalsGalleryService>();

    public async Task<List<Medals>> GetMedalsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _medalsGalleryRepository.GetMedalsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsCountAsync(string search, string rare)
    {
        return await _medalsGalleryRepository.GetMedalsCountAsync(search, rare);
    }

    public async Task<bool> InsertMedalGalleryAsync(string userId, string Id)
    {
        var insertResult = await _medalsGalleryRepository.InsertMedalGalleryAsync(userId, Id, await _medalsService.GetMedalByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusMedalGalleryAsync(string userId, string medalId)
    {
        var updateResult = await _medalsGalleryRepository.UpdateStatusMedalGalleryAsync(userId, medalId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Medals medalGallery = await GetMedalCollectionByIdAsync(userId, medalId) ?? new Medals();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)medalGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusMedalsGalleryAsync(string userId)
    {
        Medals oldMedal = await SumPowerMedalsGalleryAsync(userId);

        var updateResult = await _medalsGalleryRepository.UpdateBatchStatusMedalsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Medals newMedal = await SumPowerMedalsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Medals> SumPowerMedalsGalleryAsync(string userId)
    {
        return await _medalsGalleryRepository.SumPowerMedalsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarMedalGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _medalsGalleryRepository.UpdateTempStarMedalGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarMedalGalleryAsync(string userId, string medalId)
    {
        Medals oldMedal = await GetMedalCollectionByIdAsync(userId, medalId) ?? new Medals();

        var updateResult = await _medalsGalleryRepository.UpdateCurrentStarMedalGalleryAsync(userId, medalId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Medals newMedal = await GetMedalCollectionByIdAsync(userId, medalId) ?? new Medals();
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarMedalsGalleryAsync(string userId)
    {
        Medals oldMedal = await SumPowerMedalsGalleryAsync(userId);

        var updateResult = await _medalsGalleryRepository.UpdateBatchCurrentStarMedalsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Medals newMedal = await SumPowerMedalsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchMedalsGalleryAsync(string userId, List<Medals> medals)
    {
        var insertResult = await _medalsGalleryRepository.InsertBatchMedalsGalleryAsync(userId, medals);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Medals> GetMedalCollectionByIdAsync(string userId, string medalId)
    {
        var result = await _medalsGalleryRepository.GetMedalCollectionByIdAsync(userId, medalId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateMedalGalleryPowerAsync(string userId, string Id)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        await _medalsGalleryRepository.UpdateMedalGalleryPowerAsync(userId, Id, await _service.GetMedalByIdAsync(Id));
    }
}
