using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBadgesService : IUserBadgesService
{
    private readonly IUserBadgesRepository _userBadgesRepository;
    private readonly IBadgesGalleryService _badgesGalleryService;
    private readonly IBadgesService _badgesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserBadgesService(
        IUserBadgesRepository userBadgesRepository,
        IBadgesGalleryService badgesGalleryService,
        IBadgesService badgesService,
        IPowerManagerService powerManagerService)
    {
        _userBadgesRepository = userBadgesRepository;
        _badgesGalleryService = badgesGalleryService;
        _badgesService = badgesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserBadgesService Create() => ServiceContainer.GetService<IUserBadgesService>();

    public async Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _userBadgesRepository.GetUserBadgesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBadgesCountAsync(string userId, string search, string rare)
    {
        return await _userBadgesRepository.GetUserBadgesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgeAsync(string userId, Badges badge)
    {
        Badges oldBadge = await _badgesService.SumPowerBadgesPercentAsync(userId);
        var insertOrUpdateResult = await _userBadgesRepository.InsertOrUpdateUserBadgeAsync(userId, badge);

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

        await _badgesGalleryService.InsertBadgeGalleryAsync(userId, badge.Id);

        Badges newBadge = await _badgesService.SumPowerBadgesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgesBatchAsync(string userId, List<Badges> badgees)
    {
        Badges oldBadge = await _badgesService.SumPowerBadgesPercentAsync(userId);
        var repositoryResult = await _userBadgesRepository.InsertOrUpdateUserBadgesBatchAsync(userId, badgees);

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
            await _badgesGalleryService.InsertBatchBadgesGalleryAsync(userId, newlyInsertedCards);
        }

        Badges newBadge = await _badgesService.SumPowerBadgesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newBadge - (PowerManager)oldBadge;

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

    public async Task<bool> UpdateUserBadgeLevelAsync(string userId, Badges badge)
    {
        var updateResult = await _userBadgesRepository.UpdateUserBadgeLevelAsync(userId, badge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserBadgeStarAsync(string userId, Badges badge)
    {
        var updateResult = await _userBadgesRepository.UpdateUserBadgeStarAsync(userId, badge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _badgesGalleryService.UpdateTempStarBadgeGalleryAsync(userId, badge.Id, badge.Star);

        return true;
    }

    public async Task<Badges> GetUserBadgeByIdAsync(string userId, string Id)
    {
        var result = await _userBadgesRepository.GetUserBadgeByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Badges> SumPowerUserBadgesAsync(string userId)
    {
        return await _userBadgesRepository.SumPowerUserBadgesAsync(userId);
    }
}
