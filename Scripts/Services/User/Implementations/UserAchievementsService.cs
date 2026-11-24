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

    public UserAchievementsService(
        IUserAchievementsRepository userAchievementsRepository,
        IAchievementsGalleryService achievementsGalleryService,
        IAchievementsService achievementsService)
    {
        _userAchievementsRepository = userAchievementsRepository;
        _achievementsGalleryService = achievementsGalleryService;
        _achievementsService = achievementsService;
    }

    public static IUserAchievementsService Create() => ServiceContainer.GetService<IUserAchievementsService>();

    public async Task<List<Achievements>> GetUserAchievementsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Achievements> result = await _userAchievementsRepository.GetUserAchievementsAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserAchievementsCountAsync(string userId, string search, string rare)
    {
        return await _userAchievementsRepository.GetUserArchievementsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementAsync(string userId, Achievements achievement)
    {
        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(achievement.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userAchievementsRepository.InsertOrUpdateUserAchievementAsync(userId, achievement);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _achievementsGalleryService.InsertAchievementGalleryAsync(userId, achievement.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementsBatchAsync(string userId, List<Achievements> achievements)
    {
        var insertOrUpdateResult = await _userAchievementsRepository.InsertOrUpdateUserAchievementsBatchAsync(userId, achievements);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _achievementsGalleryService.InsertBatchAchievementsGalleryAsync(userId, newlyInsertedCards);
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
                IsChangePower = false,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserAchievementLevelAsync(string userId, Achievements achievement, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (achievement == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Achievement is required.");
        }

        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(achievement.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userAchievementsRepository.UpdateUserAchievementLevelAsync(
            userId,
            achievement,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserAchievementStarAsync(string userId, Achievements achievement)
    {
        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(achievement.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userAchievementsRepository.UpdateUserAchievementStarAsync(userId, achievement);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        await _achievementsGalleryService.UpdateTempStarAchievementGalleryAsync(userId, achievement.Id, achievement.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserAchievementQuantityAsync(string userId, Achievements achievement, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userAchievementsRepository.UpdateUserAchievementQuantityAsync(userId, achievement, totalDeductions);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserAchievementAffinityAsync(string userId, Achievements achievement, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (achievement == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Achievement is required.");
        }

        var checkResult = await _achievementsService.IsAchievementDeletedOrInactiveAsync(achievement.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userAchievementsRepository.UpdateUserAchievementAffinityAsync(
            userId,
            achievement,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }
    
    public async Task<Achievements> GetUserAchievementByIdAsync(string userId, string Id)
    {
        var result = await _userAchievementsRepository.GetUserAchievementByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Achievements> SumPowerUserAchievementsAsync(string userId)
    {
        return await _userAchievementsRepository.SumPowerUserAchievementsAsync(userId);
    }
}