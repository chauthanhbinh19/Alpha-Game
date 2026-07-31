using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TitlesGalleryService : ITitlesGalleryService
{
    private readonly ITitlesGalleryRepository _titlesGalleryRepository;
    private readonly ITitlesService _titlesService;
    private readonly IPowerManagerService _powerManagerService;

    public TitlesGalleryService(
        ITitlesGalleryRepository titlesGalleryRepository,
        ITitlesService titlesService,
        IPowerManagerService powerManagerService)
    {
        _titlesGalleryRepository = titlesGalleryRepository;
        _titlesService = titlesService;
        _powerManagerService = powerManagerService;
    }

    public static ITitlesGalleryService Create() => ServiceContainer.GetService<ITitlesGalleryService>();

    public async Task<List<Titles>> GetTitlesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _titlesGalleryRepository.GetTitlesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTitlesCountAsync(string search, string rare)
    {
        return await _titlesGalleryRepository.GetTitlesCountAsync(search, rare);
    }

    public async Task<bool> InsertTitleGalleryAsync(string userId, string Id)
    {
        var insertResult = await _titlesGalleryRepository.InsertTitleGalleryAsync(userId, Id, await _titlesService.GetTitleByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusTitleGalleryAsync(string userId, string titleId)
    {
        var updateResult = await _titlesGalleryRepository.UpdateStatusTitleGalleryAsync(userId, titleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Titles titleGallery = await GetTitleCollectionByIdAsync(userId, titleId) ?? new Titles();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)titleGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusTitlesGalleryAsync(string userId)
    {
        Titles oldTitle = await SumPowerTitlesGalleryAsync(userId);

        var updateResult = await _titlesGalleryRepository.UpdateBatchStatusTitlesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Titles newTitle = await SumPowerTitlesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Titles> SumPowerTitlesGalleryAsync(string userId)
    {
        return await _titlesGalleryRepository.SumPowerTitlesGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarTitleGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _titlesGalleryRepository.UpdateTempStarTitleGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarTitleGalleryAsync(string userId, string titleId)
    {
        Titles oldTitle = await GetTitleCollectionByIdAsync(userId, titleId) ?? new Titles();

        var updateResult = await _titlesGalleryRepository.UpdateCurrentStarTitleGalleryAsync(userId, titleId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Titles newTitle = await GetTitleCollectionByIdAsync(userId, titleId) ?? new Titles();
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarTitlesGalleryAsync(string userId)
    {
        Titles oldTitle = await SumPowerTitlesGalleryAsync(userId);

        var updateResult = await _titlesGalleryRepository.UpdateBatchCurrentStarTitlesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Titles newTitle = await SumPowerTitlesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchTitlesGalleryAsync(string userId, List<Titles> titles)
    {
        var insertResult = await _titlesGalleryRepository.InsertBatchTitlesGalleryAsync(userId, titles);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Titles> GetTitleCollectionByIdAsync(string userId, string titleId)
    {
        var result = await _titlesGalleryRepository.GetTitleCollectionByIdAsync(userId, titleId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateTitleGalleryPowerAsync(string userId, string Id)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        await _titlesGalleryRepository.UpdateTitleGalleryPowerAsync(userId, Id, await _service.GetTitleByIdAsync(Id));
    }
}
