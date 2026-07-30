using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BeveragesGalleryService : IBeveragesGalleryService
{
    private readonly IBeveragesGalleryRepository _beveragesGalleryRepository;
    private readonly IBeveragesService _beveragesService;
    private readonly IPowerManagerService _powerManagerService;

    public BeveragesGalleryService(
        IBeveragesGalleryRepository beveragesGalleryRepository,
        IBeveragesService beveragesService,
        IPowerManagerService powerManagerService)
    {
        _beveragesGalleryRepository = beveragesGalleryRepository;
        _beveragesService = beveragesService;
        _powerManagerService = powerManagerService;
    }

    public static IBeveragesGalleryService Create() => ServiceContainer.GetService<IBeveragesGalleryService>();

    public async Task<List<Beverages>> GetBeveragesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _beveragesGalleryRepository.GetBeveragesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesCountAsync(string search, string rare)
    {
        return await _beveragesGalleryRepository.GetBeveragesCountAsync(search, rare);
    }

    public async Task<bool> InsertBeverageGalleryAsync(string userId, string Id)
    {
        var insertResult = await _beveragesGalleryRepository.InsertBeverageGalleryAsync(userId, Id, await _beveragesService.GetBeverageByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusBeverageGalleryAsync(string userId, string beverageId)
    {
        var updateResult = await _beveragesGalleryRepository.UpdateStatusBeverageGalleryAsync(userId, beverageId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Beverages beverageGallery = await GetBeverageCollectionByIdAsync(userId, beverageId) ?? new Beverages();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)beverageGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusBeveragesGalleryAsync(string userId)
    {
        Beverages oldBeverage = await SumPowerBeveragesGalleryAsync(userId);

        var updateResult = await _beveragesGalleryRepository.UpdateBatchStatusBeveragesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Beverages newBeverage = await SumPowerBeveragesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Beverages> SumPowerBeveragesGalleryAsync(string userId)
    {
        return await _beveragesGalleryRepository.SumPowerBeveragesGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarBeverageGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _beveragesGalleryRepository.UpdateStarBeverageGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarBeverageGalleryAsync(string userId, string beverageId)
    {
        Beverages oldBeverage = await GetBeverageCollectionByIdAsync(userId, beverageId) ?? new Beverages();

        var updateResult = await _beveragesGalleryRepository.UpdateCurrentStarBeverageGalleryAsync(userId, beverageId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Beverages newBeverage = await GetBeverageCollectionByIdAsync(userId, beverageId) ?? new Beverages();
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarBeveragesGalleryAsync(string userId)
    {
        Beverages oldBeverage = await SumPowerBeveragesGalleryAsync(userId);

        var updateResult = await _beveragesGalleryRepository.UpdateBatchCurrentStarBeveragesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Beverages newBeverage = await SumPowerBeveragesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBeverage - (PowerManager)oldBeverage;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchBeveragesGalleryAsync(string userId, List<Beverages> beverages)
    {
        var insertResult = await _beveragesGalleryRepository.InsertBatchBeveragesGalleryAsync(userId, beverages);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Beverages> GetBeverageCollectionByIdAsync(string userId, string beverageId)
    {
        var result = await _beveragesGalleryRepository.GetBeverageCollectionByIdAsync(userId, beverageId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateBeverageGalleryPowerAsync(string userId, string Id)
    {
        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        await _beveragesGalleryRepository.UpdateBeverageGalleryPowerAsync(userId, Id, await _service.GetBeverageByIdAsync(Id));
    }
}
