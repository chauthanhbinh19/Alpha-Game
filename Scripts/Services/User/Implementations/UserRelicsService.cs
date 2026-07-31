using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRelicsService : IUserRelicsService
{
    private readonly IUserRelicsRepository _userRelicsRepository;
    private readonly IRelicsGalleryService _relicsGalleryService;
    private readonly IRelicsService _relicsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserRelicsService(
        IUserRelicsRepository userRelicsRepository,
        IRelicsGalleryService relicsGalleryService,
        IRelicsService relicsService,
        IPowerManagerService powerManagerService)
    {
        _userRelicsRepository = userRelicsRepository;
        _relicsGalleryService = relicsGalleryService;
        _relicsService = relicsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserRelicsService Create() => ServiceContainer.GetService<IUserRelicsService>();

    public async Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _userRelicsRepository.GetUserRelicsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userRelicsRepository.GetUserRelicsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicAsync(string userId, Relics relic)
    {
        Relics oldRelic = await _relicsService.SumPowerRelicsPercentAsync(userId);
        var insertOrUpdateResult = await _userRelicsRepository.InsertOrUpdateUserRelicAsync(userId, relic);

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

        await _relicsGalleryService.InsertRelicGalleryAsync(userId, relic.Id);

        Relics newRelic = await _relicsService.SumPowerRelicsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicsBatchAsync(string userId, List<Relics> relices)
    {
        Relics oldRelic = await _relicsService.SumPowerRelicsPercentAsync(userId);
        var repositoryResult = await _userRelicsRepository.InsertOrUpdateUserRelicsBatchAsync(userId, relices);

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
            await _relicsGalleryService.InsertBatchRelicsGalleryAsync(userId, newlyInsertedCards);
        }

        Relics newRelic = await _relicsService.SumPowerRelicsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newRelic - (PowerManager)oldRelic;

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

    public async Task<bool> UpdateUserRelicLevelAsync(string userId, Relics relic)
    {
        var updateResult = await _userRelicsRepository.UpdateUserRelicLevelAsync(userId, relic);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserRelicStarAsync(string userId, Relics relic)
    {
        var updateResult = await _userRelicsRepository.UpdateUserRelicStarAsync(userId, relic);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _relicsGalleryService.UpdateTempStarRelicGalleryAsync(userId, relic.Id, relic.Star);

        return true;
    }

    public async Task<Relics> GetUserRelicByIdAsync(string userId, string Id)
    {
        var result = await _userRelicsRepository.GetUserRelicByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Relics> SumPowerUserRelicsAsync(string userId)
    {
        return await _userRelicsRepository.SumPowerUserRelicsAsync(userId);
    }
}
