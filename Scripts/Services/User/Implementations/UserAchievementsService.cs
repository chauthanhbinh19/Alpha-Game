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

        foreach (var item in list)
        {
            item.BaseStats = new BaseStats(item);
        }

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
        var oldAchievementTask = _achievementsService.SumPowerAchievementsPercentAsync(userId);
        var oldUserAchievementTask = _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);

        await Task.WhenAll(oldAchievementTask, oldUserAchievementTask);

        Achievements oldAchievement = oldAchievementTask.Result;
        Achievements oldUserAchievement = oldUserAchievementTask.Result;

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

        var newAchievementTask = _achievementsService.SumPowerAchievementsPercentAsync(userId);
        var newUserAchievementTask = _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);

        await Task.WhenAll(newAchievementTask, newUserAchievementTask);

        PowerManager deltaPower = (PowerManager)newAchievementTask.Result - (PowerManager)oldAchievement;
        PowerManager deltaUserPower = (PowerManager)newUserAchievementTask.Result - (PowerManager)oldUserAchievement;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementsBatchAsync(string userId, List<Achievements> achievements)
    {
        var oldAchievementTask = _achievementsService.SumPowerAchievementsPercentAsync(userId);
        var oldUserAchievementTask = _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);

        await Task.WhenAll(oldAchievementTask, oldUserAchievementTask);

        Achievements oldAchievement = oldAchievementTask.Result;
        Achievements oldUserAchievement = oldUserAchievementTask.Result;

        var insertOrUpdateResult = await _userAchievementsRepository.InsertOrUpdateUserAchievementsBatchAsync(userId, achievements);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _achievementsGalleryService.InsertBatchAchievementsGalleryAsync(userId, newlyInsertedCards);

            var newAchievementTask = _achievementsService.SumPowerAchievementsPercentAsync(userId);
            var newUserAchievementTask = _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);

            await Task.WhenAll(newAchievementTask, newUserAchievementTask);

            PowerManager deltaPower = (PowerManager)newAchievementTask.Result - (PowerManager)oldAchievement;
            PowerManager deltaUserPower = (PowerManager)newUserAchievementTask.Result - (PowerManager)oldUserAchievement;

            PowerManager totalDelta = new PowerManager();
            if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
            if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

            if (totalDelta.HasAnyPositiveStat())
            {
                PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
                PowerManager updatedPower = currentPower + totalDelta;
                await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
            }
        }

        return insertOrUpdateResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserAchievementLevelAsync(string userId, Achievements achievement)
    {
        Achievements oldUserAchievement = await _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);

        var updateResult = await _userAchievementsRepository.UpdateUserAchievementLevelAsync(userId, achievement);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Achievements newUserAchievement = await _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserAchievement - (PowerManager)oldUserAchievement;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserAchievementStarAsync(string userId, Achievements achievement)
    {
        Achievements oldUserAchievement = await _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);

        var updateResult = await _userAchievementsRepository.UpdateUserAchievementStarAsync(userId, achievement);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _achievementsGalleryService.UpdateTempStarAchievementGalleryAsync(userId, achievement.Id, achievement.Star);

        Achievements newUserAchievement = await _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserAchievement - (PowerManager)oldUserAchievement;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Achievements> GetUserAchievementByIdAsync(string userId, string Id)
    {
        var result = await _userAchievementsRepository.GetUserAchievementByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

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