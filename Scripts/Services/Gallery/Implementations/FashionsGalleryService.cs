using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FashionsGalleryService : IFashionsGalleryService
{
    private readonly IFashionsGalleryRepository _fashionsGalleryRepository;
    private readonly IFashionsService _fashionsService;
    private readonly IPowerManagerService _powerManagerService;

    public FashionsGalleryService(
        IFashionsGalleryRepository fashionsGalleryRepository,
        IFashionsService fashionsService,
        IPowerManagerService powerManagerService)
    {
        _fashionsGalleryRepository = fashionsGalleryRepository;
        _fashionsService = fashionsService;
        _powerManagerService = powerManagerService;
    }

    public static IFashionsGalleryService Create() => ServiceContainer.GetService<IFashionsGalleryService>();

    public async Task<List<Fashions>> GetFashionsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _fashionsGalleryRepository.GetFashionsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsCountAsync(string search, string type, string rare)
    {
        return await _fashionsGalleryRepository.GetFashionsCountAsync(search, type, rare);
    }

    public async Task<bool> InsertFashionGalleryAsync(string userId, string Id)
    {
        var insertResult = await _fashionsGalleryRepository.InsertFashionGalleryAsync(userId, Id, await _fashionsService.GetFashionByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusFashionGalleryAsync(string userId, string fashionId)
    {
        var updateResult = await _fashionsGalleryRepository.UpdateStatusFashionGalleryAsync(userId, fashionId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Fashions fashionGallery = await GetFashionCollectionByIdAsync(userId, fashionId) ?? new Fashions();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)fashionGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusFashionsGalleryAsync(string userId)
    {
        Fashions oldFashion = await SumPowerFashionsGalleryAsync(userId);

        var updateResult = await _fashionsGalleryRepository.UpdateBatchStatusFashionsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Fashions newFashion = await SumPowerFashionsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Fashions> SumPowerFashionsGalleryAsync(string userId)
    {
        return await _fashionsGalleryRepository.SumPowerFashionsGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarFashionGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _fashionsGalleryRepository.UpdateTempStarFashionGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarFashionGalleryAsync(string userId, string fashionId)
    {
        Fashions oldFashion = await GetFashionCollectionByIdAsync(userId, fashionId) ?? new Fashions();

        var updateResult = await _fashionsGalleryRepository.UpdateCurrentStarFashionGalleryAsync(userId, fashionId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Fashions newFashion = await GetFashionCollectionByIdAsync(userId, fashionId) ?? new Fashions();
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarFashionsGalleryAsync(string userId)
    {
        Fashions oldFashion = await SumPowerFashionsGalleryAsync(userId);

        var updateResult = await _fashionsGalleryRepository.UpdateBatchCurrentStarFashionsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Fashions newFashion = await SumPowerFashionsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchFashionsGalleryAsync(string userId, List<Fashions> fashions)
    {
        var insertResult = await _fashionsGalleryRepository.InsertBatchFashionsGalleryAsync(userId, fashions);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Fashions> GetFashionCollectionByIdAsync(string userId, string fashionId)
    {
        var result = await _fashionsGalleryRepository.GetFashionCollectionByIdAsync(userId, fashionId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateFashionGalleryPowerAsync(string userId, string Id)
    {
        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        await _fashionsGalleryRepository.UpdateFashionGalleryPowerAsync(userId, Id, await _service.GetFashionByIdAsync(Id));
    }
}
