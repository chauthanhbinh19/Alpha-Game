using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MagicFormationCirclesGalleryService : IMagicFormationCirclesGalleryService
{
    private readonly IMagicFormationCirclesGalleryRepository _magicFormationCirclesGalleryRepository;
    private readonly IMagicFormationCirclesService _magicFormationCirclesService;
    private readonly IPowerManagerService _powerManagerService;

    public MagicFormationCirclesGalleryService(
        IMagicFormationCirclesGalleryRepository magicFormationCirclesGalleryRepository,
        IMagicFormationCirclesService magicFormationCirclesService,
        IPowerManagerService powerManagerService)
    {
        _magicFormationCirclesGalleryRepository = magicFormationCirclesGalleryRepository;
        _magicFormationCirclesService = magicFormationCirclesService;
        _powerManagerService = powerManagerService;
    }

    public static IMagicFormationCirclesGalleryService Create() => ServiceContainer.GetService<IMagicFormationCirclesGalleryService>();

    public async Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _magicFormationCirclesGalleryRepository.GetMagicFormationCirclesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare)
    {
        return await _magicFormationCirclesGalleryRepository.GetMagicFormationCirclesCountAsync(search, type, rare);
    }

    public async Task<bool> InsertMagicFormationCircleGalleryAsync(string userId, string Id)
    {
        var insertResult = await _magicFormationCirclesGalleryRepository.InsertMagicFormationCircleGalleryAsync(userId, Id, await _magicFormationCirclesService.GetMagicFormationCircleByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusMagicFormationCircleGalleryAsync(string userId, string magicFormationCircleId)
    {
        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateStatusMagicFormationCircleGalleryAsync(userId, magicFormationCircleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await PowerManagerService.Create().GetUserStatsAsync(userId);
        MagicFormationCircles magicFormationCircleGallery = await GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId) ?? new MagicFormationCircles();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)magicFormationCircleGallery;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusMagicFormationCirclesGalleryAsync(string userId)
    {
        MagicFormationCircles oldMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateBatchStatusMagicFormationCirclesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        MagicFormationCircles newMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await PowerManagerService.Create().GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync(string userId)
    {
        return await _magicFormationCirclesGalleryRepository.SumPowerMagicFormationCirclesGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarMagicFormationCircleGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateTempStarMagicFormationCircleGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarMagicFormationCircleGalleryAsync(string userId, string magicFormationCircleId)
    {
        MagicFormationCircles oldMagicFormationCircle = await GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId) ?? new MagicFormationCircles();

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateCurrentStarMagicFormationCircleGalleryAsync(userId, magicFormationCircleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        MagicFormationCircles newMagicFormationCircle = await GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId) ?? new MagicFormationCircles();
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await PowerManagerService.Create().GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarMagicFormationCirclesGalleryAsync(string userId)
    {
        MagicFormationCircles oldMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);

        var updateResult = await _magicFormationCirclesGalleryRepository.UpdateBatchCurrentStarMagicFormationCirclesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        MagicFormationCircles newMagicFormationCircle = await SumPowerMagicFormationCirclesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await PowerManagerService.Create().GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await PowerManagerService.Create().UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchMagicFormationCirclesGalleryAsync(string userId, List<MagicFormationCircles> magicFormationCircles)
    {
        var insertResult = await _magicFormationCirclesGalleryRepository.InsertBatchMagicFormationCirclesGalleryAsync(userId, magicFormationCircles);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<MagicFormationCircles> GetMagicFormationCircleCollectionByIdAsync(string userId, string magicFormationCircleId)
    {
        var result = await _magicFormationCirclesGalleryRepository.GetMagicFormationCircleCollectionByIdAsync(userId, magicFormationCircleId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateMagicFormationCircleGalleryPowerAsync(string userId, string Id)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        await _magicFormationCirclesGalleryRepository.UpdateMagicFormationCircleGalleryPowerAsync(userId, Id, await _service.GetMagicFormationCircleByIdAsync(Id));
    }
}
