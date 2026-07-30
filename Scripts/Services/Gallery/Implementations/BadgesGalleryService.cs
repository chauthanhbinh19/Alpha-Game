using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BadgesGalleryService : IBadgesGalleryService
{
    private readonly IBadgesGalleryRepository _badgesGalleryRepository;
    private readonly IBadgesService _badgesService;
    private readonly IPowerManagerService _powerManagerService;

    public BadgesGalleryService(
        IBadgesGalleryRepository badgesGalleryRepository,
        IBadgesService badgesService,
        IPowerManagerService powerManagerService)
    {
        _badgesGalleryRepository = badgesGalleryRepository;
        _badgesService = badgesService;
        _powerManagerService = powerManagerService;
    }

    public static IBadgesGalleryService Create() => ServiceContainer.GetService<IBadgesGalleryService>();

    public async Task<List<Badges>> GetBadgesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _badgesGalleryRepository.GetBadgesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesCountAsync(string search, string rare)
    {
        return await _badgesGalleryRepository.GetBadgesCountAsync(search, rare);
    }

    public async Task<bool> InsertBadgeGalleryAsync(string userId, string Id)
    {
        var insertResult = await _badgesGalleryRepository.InsertBadgeGalleryAsync(userId, Id, await _badgesService.GetBadgeByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusBadgeGalleryAsync(string userId, string badgeId)
    {
        var updateResult = await _badgesGalleryRepository.UpdateStatusBadgeGalleryAsync(userId, badgeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Badges badgeGallery = await GetBadgeCollectionByIdAsync(userId, badgeId) ?? new Badges();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)badgeGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusBadgesGalleryAsync(string userId)
    {
        Badges oldBadge = await SumPowerBadgesGalleryAsync(userId);

        var updateResult = await _badgesGalleryRepository.UpdateBatchStatusBadgesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Badges newBadge = await SumPowerBadgesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Badges> SumPowerBadgesGalleryAsync(string userId)
    {
        return await _badgesGalleryRepository.SumPowerBadgesGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarBadgeGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _badgesGalleryRepository.UpdateStarBadgeGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarBadgeGalleryAsync(string userId, string badgeId)
    {
        Badges oldBadge = await GetBadgeCollectionByIdAsync(userId, badgeId) ?? new Badges();

        var updateResult = await _badgesGalleryRepository.UpdateCurrentStarBadgeGalleryAsync(userId, badgeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Badges newBadge = await GetBadgeCollectionByIdAsync(userId, badgeId) ?? new Badges();
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarBadgesGalleryAsync(string userId)
    {
        Badges oldBadge = await SumPowerBadgesGalleryAsync(userId);

        var updateResult = await _badgesGalleryRepository.UpdateBatchCurrentStarBadgesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Badges newBadge = await SumPowerBadgesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchBadgesGalleryAsync(string userId, List<Badges> badges)
    {
        var insertResult = await _badgesGalleryRepository.InsertBatchBadgesGalleryAsync(userId, badges);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Badges> GetBadgeCollectionByIdAsync(string userId, string badgeId)
    {
        var result = await _badgesGalleryRepository.GetBadgeCollectionByIdAsync(userId, badgeId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateBadgeGalleryPowerAsync(string userId, string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _badgesGalleryRepository.UpdateBadgeGalleryPowerAsync(userId, Id, await _service.GetBadgeByIdAsync(Id));
    }
}
