using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTitlesService : IUserTitlesService
{
    private readonly IUserTitlesRepository _userTitlesRepository;
    private readonly ITitlesGalleryService _titlesGalleryService;
    private readonly ITitlesService _titlesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserTitlesService(
        IUserTitlesRepository userTitlesRepository,
        ITitlesGalleryService titlesGalleryService,
        ITitlesService titlesService,
        IPowerManagerService powerManagerService)
    {
        _userTitlesRepository = userTitlesRepository;
        _titlesGalleryService = titlesGalleryService;
        _titlesService = titlesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserTitlesService Create() => ServiceContainer.GetService<IUserTitlesService>();

    public async Task<List<Titles>> GetUserTitlesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _userTitlesRepository.GetUserTitlesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTitlesCountAsync(string userId, string search, string rare)
    {
        return await _userTitlesRepository.GetUserTitlesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTitleAsync(string userId, Titles title)
    {
        Titles oldTitle = await _titlesService.SumPowerTitlesPercentAsync(userId);
        var insertOrUpdateResult = await _userTitlesRepository.InsertOrUpdateUserTitleAsync(userId, title);

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

        await _titlesGalleryService.InsertTitleGalleryAsync(userId, title.Id);

        Titles newTitle = await _titlesService.SumPowerTitlesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTitlesBatchAsync(string userId, List<Titles> titlees)
    {
        Titles oldTitle = await _titlesService.SumPowerTitlesPercentAsync(userId);
        var repositoryResult = await _userTitlesRepository.InsertOrUpdateUserTitlesBatchAsync(userId, titlees);

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
            await _titlesGalleryService.InsertBatchTitlesGalleryAsync(userId, newlyInsertedCards);
        }

        Titles newTitle = await _titlesService.SumPowerTitlesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newTitle - (PowerManager)oldTitle;

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

    public async Task<bool> UpdateUserTitleLevelAsync(string userId, Titles title)
    {
        var updateResult = await _userTitlesRepository.UpdateUserTitleLevelAsync(userId, title);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserTitleStarAsync(string userId, Titles title)
    {
        var updateResult = await _userTitlesRepository.UpdateUserTitleStarAsync(userId, title);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _titlesGalleryService.UpdateTempStarTitleGalleryAsync(userId, title.Id, title.Star);

        return true;
    }

    public async Task<Titles> GetUserTitleByIdAsync(string userId, string Id)
    {
        var result = await _userTitlesRepository.GetUserTitleByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Titles> SumPowerUserTitlesAsync(string userId)
    {
        return await _userTitlesRepository.SumPowerUserTitlesAsync(userId);
    }
}
