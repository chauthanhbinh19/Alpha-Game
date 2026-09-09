using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AchievementsGalleryService : IAchievementsGalleryService
{
    private readonly IAchievementsGalleryRepository _achievementsGalleryRepository;
    private readonly IAchievementsService _achievementsService;
    private readonly IPowerManagerService _powerManagerService;

    public AchievementsGalleryService(
        IAchievementsGalleryRepository achievementsGalleryRepository, 
        IAchievementsService achievementsService,
        IPowerManagerService powerManagerService)
    {
        _achievementsGalleryRepository = achievementsGalleryRepository;
        _achievementsService = achievementsService;
        _powerManagerService = powerManagerService;
    }

    public static IAchievementsGalleryService Create() => ServiceContainer.GetService<IAchievementsGalleryService>();

    public async Task<List<Achievements>> GetAchievementsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Achievements> list = await _achievementsGalleryRepository.GetAchievementsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAchievementsCountAsync(string search, string rare)
    {
        return await _achievementsGalleryRepository.GetAchievementsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertAchievementGalleryAsync(string userId, string Id)
    {
        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _achievementsGalleryRepository.InsertAchievementGalleryAsync(userId, Id, await _achievementsService.GetAchievementByIdAsync(Id));

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

    public async Task<Achievements> SumPowerAchievementsGalleryAsync(string userId)
    {
        return await _achievementsGalleryRepository.SumPowerAchievementsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateAchievementGalleryPowerAsync(string userId, string Id, Achievements AchievementFromDB)
    {
        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }
        
        await _achievementsGalleryRepository.UpdateAchievementGalleryPowerAsync(userId, Id, AchievementFromDB);
        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarAchievementGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _achievementsGalleryRepository.UpdateTempStarAchievementGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusAchievementGalleryAsync(string userId, string achievementId)
    {
        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(achievementId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _achievementsGalleryRepository.UpdateStatusAchievementGalleryAsync(userId, achievementId);

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
        Achievements achievementGallery = await GetAchievementCollectionByIdAsync(userId, achievementId) ?? new Achievements();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)achievementGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAchievementsGalleryAsync(string userId)
    {
        Achievements oldAchievement = await SumPowerAchievementsGalleryAsync(userId);

        var updateResult = await _achievementsGalleryRepository.UpdateBatchStatusAchievementsGalleryAsync(userId);

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

        Achievements newAchievement = await SumPowerAchievementsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAchievement - (PowerManager)oldAchievement;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarAchievementGalleryAsync(string userId, string achievementId)
    {
        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(achievementId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Achievements oldAchievement = await GetAchievementCollectionByIdAsync(userId, achievementId) ?? new Achievements();

        var updateResult = await _achievementsGalleryRepository.UpdateCurrentStarAchievementGalleryAsync(userId, achievementId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Achievements newAchievement = await GetAchievementCollectionByIdAsync(userId, achievementId) ?? new Achievements();
        PowerManager deltaPower = (PowerManager)newAchievement - (PowerManager)oldAchievement;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarAchievementsGalleryAsync(string userId)
    {
        Achievements oldAchievement = await SumPowerAchievementsGalleryAsync(userId);

        var updateResult = await _achievementsGalleryRepository.UpdateBatchCurrentStarAchievementsGalleryAsync(userId);

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

        Achievements newAchievement = await SumPowerAchievementsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAchievement - (PowerManager)oldAchievement;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchAchievementsGalleryAsync(string userId, List<Achievements> achievements)
    {
        var insertResult = await _achievementsGalleryRepository.InsertBatchAchievementsGalleryAsync(userId, achievements);

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

    public async Task<Achievements> GetAchievementCollectionByIdAsync(string userId, string achievementId)
    {
        var result = await _achievementsGalleryRepository.GetAchievementCollectionByIdAsync(userId, achievementId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }
}