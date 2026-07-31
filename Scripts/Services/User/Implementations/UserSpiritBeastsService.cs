using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritBeastsService : IUserSpiritBeastsService
{
    private readonly IUserSpiritBeastsRepository _userSpiritBeastsRepository;
    private readonly ISpiritBeastsGalleryService _spiritBeastsGalleryService;
    private readonly ISpiritBeastsService _spiritBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserSpiritBeastsService(
        IUserSpiritBeastsRepository userSpiritBeastsRepository,
        ISpiritBeastsGalleryService spiritBeastsGalleryService,
        ISpiritBeastsService spiritBeastsService,
        IPowerManagerService powerManagerService)
    {
        _userSpiritBeastsRepository = userSpiritBeastsRepository;
        _spiritBeastsGalleryService = spiritBeastsGalleryService;
        _spiritBeastsService = spiritBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserSpiritBeastsService Create() => ServiceContainer.GetService<IUserSpiritBeastsService>();

    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string userId, int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetAllUserSpiritBeastsAsync(userId, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsByCardIdsAsync(string userId, List<string> cardIds)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsByCardIdsAsync(userId, cardIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastAsync(string userId, SpiritBeasts spiritBeast)
    {
        SpiritBeasts oldSpiritBeast = await _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        var insertOrUpdateResult = await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastAsync(userId, spiritBeast);

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

        await _spiritBeastsGalleryService.InsertSpiritBeastGalleryAsync(userId, spiritBeast.Id);

        SpiritBeasts newSpiritBeast = await _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastsBatchAsync(string userId, List<SpiritBeasts> spiritBeastes)
    {
        SpiritBeasts oldSpiritBeast = await _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        var repositoryResult = await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastsBatchAsync(userId, spiritBeastes);

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
            await _spiritBeastsGalleryService.InsertBatchSpiritBeastsGalleryAsync(userId, newlyInsertedCards);
        }

        SpiritBeasts newSpiritBeast = await _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritBeast - (PowerManager)oldSpiritBeast;

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

    public async Task<bool> UpdateUserSpiritBeastLevelAsync(string userId, SpiritBeasts spiritBeast)
    {
        var updateResult = await _userSpiritBeastsRepository.UpdateUserSpiritBeastLevelAsync(userId, spiritBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserSpiritBeastStarAsync(string userId, SpiritBeasts spiritBeast)
    {
        var updateResult = await _userSpiritBeastsRepository.UpdateUserSpiritBeastStarAsync(userId, spiritBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _spiritBeastsGalleryService.UpdateTempStarSpiritBeastGalleryAsync(userId, spiritBeast.Id, spiritBeast.Star);

        return true;
    }

    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id)
    {
        var result = await _userSpiritBeastsRepository.GetUserSpiritBeastByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync(string userId)
    {
        return await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);
    }
}
