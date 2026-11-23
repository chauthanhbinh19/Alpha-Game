using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBadgesService : IUserBadgesService
{
    private readonly IUserBadgesRepository _userBadgesRepository;
    private readonly IBadgesGalleryService _badgesGalleryService;
    private readonly IBadgesService _badgesService;

    public UserBadgesService(
        IUserBadgesRepository userBadgesRepository,
        IBadgesGalleryService badgesGalleryService,
        IBadgesService badgesService)
    {
        _userBadgesRepository = userBadgesRepository;
        _badgesGalleryService = badgesGalleryService;
        _badgesService = badgesService;
    }

    public static IUserBadgesService Create() => ServiceContainer.GetService<IUserBadgesService>();

    public async Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Badges> result = await _userBadgesRepository.GetUserBadgesAsync(userId, search, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserBadgesCountAsync(string userId, string search, string rare)
    {
        return await _userBadgesRepository.GetUserBadgesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgeAsync(string userId, Badges badge)
    {
        var checkBadgeResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(badge.Id);
        if (checkBadgeResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userBadgesRepository.InsertOrUpdateUserBadgeAsync(userId, badge);

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

        await _badgesGalleryService.InsertBadgeGalleryAsync(userId, badge.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgesBatchAsync(string userId, List<Badges> badges)
    {
        var insertOrUpdateResult = await _userBadgesRepository.InsertOrUpdateUserBadgesBatchAsync(userId, badges);

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
            await _badgesGalleryService.InsertBatchBadgesGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBadgeLevelAsync(string userId, Badges badge, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (badge == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Badge is required.");
        }

        var checkResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(badge.Id);
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

        var updateResult = await _userBadgesRepository.UpdateUserBadgeLevelAsync(
            userId,
            badge,
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBadgeStarAsync(string userId, Badges badge)
    {
        var checkBadgeResult = await _badgesService.IsBadgeDeletedOrInactiveAsync(badge.Id);
        if (checkBadgeResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userBadgesRepository.UpdateUserBadgeStarAsync(userId, badge);

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

        await _badgesGalleryService.UpdateTempStarBadgeGalleryAsync(userId, badge.Id, badge.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserBadgeQuantityAsync(string userId, Badges badge, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userBadgesRepository.UpdateUserBadgeQuantityAsync(userId, badge, totalDeductions);

        return result;
    }
    
    public async Task<Badges> GetUserBadgeByIdAsync(string userId, string Id)
    {
        var result = await _userBadgesRepository.GetUserBadgeByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<Badges> SumPowerUserBadgesAsync(string userId)
    {
        return await _userBadgesRepository.SumPowerUserBadgesAsync(userId);
    }
}
