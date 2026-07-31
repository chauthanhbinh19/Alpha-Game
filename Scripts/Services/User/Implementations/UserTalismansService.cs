using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTalismansService : IUserTalismansService
{
    private readonly IUserTalismansRepository _userTalismansRepository;
    private readonly ITalismansGalleryService _talismansGalleryService;
    private readonly ITalismansService _talismansService;
    private readonly IPowerManagerService _powerManagerService;

    public UserTalismansService(
        IUserTalismansRepository userTalismansRepository,
        ITalismansGalleryService talismansGalleryService,
        ITalismansService talismansService,
        IPowerManagerService powerManagerService)
    {
        _userTalismansRepository = userTalismansRepository;
        _talismansGalleryService = talismansGalleryService;
        _talismansService = talismansService;
        _powerManagerService = powerManagerService;
    }

    public static IUserTalismansService Create() => ServiceContainer.GetService<IUserTalismansService>();

    public async Task<List<Talismans>> GetUserTalismansAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _userTalismansRepository.GetUserTalismansAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTalismansCountAsync(string userId, string search, string type, string rare)
    {
        return await _userTalismansRepository.GetUserTalismansCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismanAsync(string userId, Talismans talisman)
    {
        Talismans oldTalisman = await _talismansService.SumPowerTalismansPercentAsync(userId);
        var insertOrUpdateResult = await _userTalismansRepository.InsertOrUpdateUserTalismanAsync(userId, talisman);

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

        await _talismansGalleryService.InsertTalismanGalleryAsync(userId, talisman.Id);

        Talismans newTalisman = await _talismansService.SumPowerTalismansPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newTalisman - (PowerManager)oldTalisman;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismansBatchAsync(string userId, List<Talismans> talismanes)
    {
        Talismans oldTalisman = await _talismansService.SumPowerTalismansPercentAsync(userId);
        var repositoryResult = await _userTalismansRepository.InsertOrUpdateUserTalismansBatchAsync(userId, talismanes);

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
            await _talismansGalleryService.InsertBatchTalismansGalleryAsync(userId, newlyInsertedCards);
        }

        Talismans newTalisman = await _talismansService.SumPowerTalismansPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newTalisman - (PowerManager)oldTalisman;

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

    public async Task<bool> UpdateUserTalismanLevelAsync(string userId, Talismans talisman)
    {
        var updateResult = await _userTalismansRepository.UpdateUserTalismanLevelAsync(userId, talisman);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserTalismanStarAsync(string userId, Talismans talisman)
    {
        var updateResult = await _userTalismansRepository.UpdateUserTalismanStarAsync(userId, talisman);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _talismansGalleryService.UpdateTempStarTalismanGalleryAsync(userId, talisman.Id, talisman.Star);

        return true;
    }

    public async Task<Talismans> GetUserTalismanByIdAsync(string userId, string Id)
    {
        var result = await _userTalismansRepository.GetUserTalismanByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Talismans> SumPowerUserTalismansAsync(string userId)
    {
        return await _userTalismansRepository.SumPowerUserTalismansAsync(userId);
    }
}
