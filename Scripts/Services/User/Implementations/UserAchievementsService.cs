using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class UserAchievementsService : IUserAchievementsService
{
    private readonly IUserAchievementsRepository _userAchievementsRepository;
    private readonly IAchievementsGalleryService _achievementsGalleryService;
    private readonly IAchievementsService _achievementsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserAchievementsService(
        IUserAchievementsRepository userAchievementsRepository,
        IAchievementsGalleryService achievementsGalleryService,
        IAchievementsService achievementsService,
        IPowerManagerService powerManagerService)
    {
        _userAchievementsRepository = userAchievementsRepository;
        _achievementsGalleryService = achievementsGalleryService;
        _achievementsService = achievementsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserAchievementsService Create() => ServiceContainer.GetService<IUserAchievementsService>();

    public async Task<List<Achievements>> GetUserAchievementsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Achievements> list = await _userAchievementsRepository.GetUserAchievementsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAchievementsCountAsync(string userId, string search, string rare)
    {
        return await _userAchievementsRepository.GetUserArchievementsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementAsync(string userId, Achievements achievement)
    {
        Achievements oldAchievement = await _achievementsService.SumPowerAchievementsPercentAsync(userId);
        var insertOrUpdateResult = await _userAchievementsRepository.InsertOrUpdateUserAchievementAsync(userId, achievement);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _achievementsGalleryService.InsertAchievementGalleryAsync(userId, achievement.Id);

        Achievements newAchievement = await _achievementsService.SumPowerAchievementsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newAchievement - (PowerManager)oldAchievement;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementsBatchAsync(string userId, List<Achievements> achievementes)
    {
        Achievements oldAchievement = await _achievementsService.SumPowerAchievementsPercentAsync(userId);
        var repositoryResult = await _userAchievementsRepository.InsertOrUpdateUserAchievementsBatchAsync(userId, achievementes);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _achievementsGalleryService.InsertBatchAchievementsGalleryAsync(userId, newlyInsertedCards);
        }

        Achievements newAchievement = await _achievementsService.SumPowerAchievementsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newAchievement - (PowerManager)oldAchievement;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserAchievementLevelAsync(string userId, Achievements achievement)
    {
        var updateResult = await _userAchievementsRepository.UpdateUserAchievementLevelAsync(userId, achievement);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserAchievementStarAsync(string userId, Achievements achievement)
    {
        var updateResult = await _userAchievementsRepository.UpdateUserAchievementStarAsync(userId, achievement);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _achievementsGalleryService.UpdateTempStarAchievementGalleryAsync(userId, achievement.Id, achievement.Star);

        return true;
    }

    public async Task<Achievements> GetUserAchievementByIdAsync(string userId, string Id)
    {
        var result = await _userAchievementsRepository.GetUserAchievementByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Achievements> SumPowerUserAchievementsAsync(string userId)
    {
        return await _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);
    }
}