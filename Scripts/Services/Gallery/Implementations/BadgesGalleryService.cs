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

    public async Task<InsertOrUpdateResult<bool>> InsertBadgeGalleryAsync(string userId, string Id)
    {
        var checkResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _badgesGalleryRepository.InsertBadgeGalleryAsync(userId, Id, await _badgesService.GetBadgeByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusBadgeGalleryAsync(string userId, string badgeId)
    {
        var checkResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(badgeId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _badgesGalleryRepository.UpdateStatusBadgeGalleryAsync(userId, badgeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Badges badgeGallery = await GetBadgeCollectionByIdAsync(userId, badgeId) ?? new Badges();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)badgeGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBadgesGalleryAsync(string userId)
    {
        Badges oldBadge = await SumPowerBadgesGalleryAsync(userId);

        var updateResult = await _badgesGalleryRepository.UpdateBatchStatusBadgesGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Badges newBadge = await SumPowerBadgesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Badges> SumPowerBadgesGalleryAsync(string userId)
    {
        return await _badgesGalleryRepository.SumPowerBadgesGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarBadgeGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _badgesGalleryRepository.UpdateTempStarBadgeGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBadgeGalleryAsync(string userId, string badgeId)
    {
        var checkResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(badgeId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Badges oldBadge = await GetBadgeCollectionByIdAsync(userId, badgeId) ?? new Badges();

        var updateResult = await _badgesGalleryRepository.UpdateCurrentStarBadgeGalleryAsync(userId, badgeId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Badges newBadge = await GetBadgeCollectionByIdAsync(userId, badgeId) ?? new Badges();
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBadgesGalleryAsync(string userId)
    {
        Badges oldBadge = await SumPowerBadgesGalleryAsync(userId);

        var updateResult = await _badgesGalleryRepository.UpdateBatchCurrentStarBadgesGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Badges newBadge = await SumPowerBadgesGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchBadgesGalleryAsync(string userId, List<Badges> badges)
    {
        var insertResult = await _badgesGalleryRepository.InsertBatchBadgesGalleryAsync(userId, badges);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Badges> GetBadgeCollectionByIdAsync(string userId, string badgeId)
    {
        var result = await _badgesGalleryRepository.GetBadgeCollectionByIdAsync(userId, badgeId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBadgeGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _badgesGalleryRepository.UpdateBadgeGalleryPowerAsync(userId, Id, await _service.GetBadgeByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
