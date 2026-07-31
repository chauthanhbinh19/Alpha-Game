using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AlchemiesGalleryService : IAlchemiesGalleryService
{
    private readonly IAlchemiesGalleryRepository _alchemiesGalleryRepository;
    private readonly IAlchemiesService _alchemiesService;
    private readonly IPowerManagerService _powerManagerService;

    public AlchemiesGalleryService(
        IAlchemiesGalleryRepository alchemiesGalleryRepository, 
        IAlchemiesService alchemiesService,
        IPowerManagerService powerManagerService)
    {
        _alchemiesGalleryRepository = alchemiesGalleryRepository;
        _alchemiesService = alchemiesService;
        _powerManagerService = powerManagerService;
    }

    public static IAlchemiesGalleryService Create() => ServiceContainer.GetService<IAlchemiesGalleryService>();

    public async Task<List<Alchemies>> GetAlchemiesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _alchemiesGalleryRepository.GetAlchemiesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemyCountAsync(string search, string type, string rare)
    {
        return await _alchemiesGalleryRepository.GetAlchemyCountAsync(search, type, rare);
    }

    public async Task<bool> InsertAlchemyGalleryAsync(string userId, string Id)
    {
        var insertResult = await _alchemiesGalleryRepository.InsertAlchemyGalleryAsync(userId, Id, await _alchemiesService.GetAlchemyByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusAlchemyGalleryAsync(string userId, string alchemyId)
    {
        var updateResult = await _alchemiesGalleryRepository.UpdateStatusAlchemyGalleryAsync(userId, alchemyId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Alchemies alchemyGallery = await GetAlchemyCollectionByIdAsync(userId, alchemyId) ?? new Alchemies();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)alchemyGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusAlchemiesGalleryAsync(string userId)
    {
        Alchemies oldAlchemy = await SumPowerAlchemiesGalleryAsync(userId);

        var updateResult = await _alchemiesGalleryRepository.UpdateBatchStatusAlchemiesGalleryAsync(userId);

        if (updateResult == null || 
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Alchemies newAlchemy = await SumPowerAlchemiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Alchemies> SumPowerAlchemiesGalleryAsync(string userId)
    {
        return await _alchemiesGalleryRepository.SumPowerAlchemyGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarAlchemyGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _alchemiesGalleryRepository.UpdateTempStarAlchemyGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarAlchemyGalleryAsync(string userId, string alchemyId)
    {
        Alchemies oldAlchemy = await GetAlchemyCollectionByIdAsync(userId, alchemyId) ?? new Alchemies();

        var updateResult = await _alchemiesGalleryRepository.UpdateCurrentStarAlchemyGalleryAsync(userId, alchemyId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Alchemies newAlchemy = await GetAlchemyCollectionByIdAsync(userId, alchemyId) ?? new Alchemies();
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarAlchemiesGalleryAsync(string userId)
    {
        Alchemies oldAlchemy = await SumPowerAlchemiesGalleryAsync(userId);

        var updateResult = await _alchemiesGalleryRepository.UpdateBatchCurrentStarAlchemiesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Alchemies newAlchemy = await SumPowerAlchemiesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchAlchemiesGalleryAsync(string userId, List<Alchemies> alchemies)
    {
        var insertResult = await _alchemiesGalleryRepository.InsertBatchAlchemiesGalleryAsync(userId, alchemies);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Alchemies> GetAlchemyCollectionByIdAsync(string userId, string alchemyId)
    {
        var result = await _alchemiesGalleryRepository.GetAlchemyCollectionByIdAsync(userId, alchemyId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateAlchemyGalleryPowerAsync(string userId, string Id)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemiesGalleryRepository.UpdateAlchemyGalleryPowerAsync(userId, Id, await _service.GetAlchemyByIdAsync(Id));
    }
}
