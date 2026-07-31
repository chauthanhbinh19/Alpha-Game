using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRunesService : IUserRunesService
{
    private readonly IUserRunesRepository _userRunesRepository;
    private readonly IRunesGalleryService _runesGalleryService;
    private readonly IRunesService _runesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserRunesService(
        IUserRunesRepository userRunesRepository,
        IRunesGalleryService runesGalleryService,
        IRunesService runesService,
        IPowerManagerService powerManagerService)
    {
        _userRunesRepository = userRunesRepository;
        _runesGalleryService = runesGalleryService;
        _runesService = runesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserRunesService Create() => ServiceContainer.GetService<IUserRunesService>();

    public async Task<List<Runes>> GetUserRunesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Runes> list = await _userRunesRepository.GetUserRunesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRunesCountAsync(string userId, string search, string rare)
    {
        return await _userRunesRepository.GetUserRunesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRuneAsync(string userId, Runes rune)
    {
        Runes oldRune = await _runesService.SumPowerRunesPercentAsync(userId);
        var insertOrUpdateResult = await _userRunesRepository.InsertOrUpdateUserRuneAsync(userId, rune);

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

        await _runesGalleryService.InsertRuneGalleryAsync(userId, rune.Id);

        Runes newRune = await _runesService.SumPowerRunesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newRune - (PowerManager)oldRune;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRunesBatchAsync(string userId, List<Runes> runees)
    {
        Runes oldRune = await _runesService.SumPowerRunesPercentAsync(userId);
        var repositoryResult = await _userRunesRepository.InsertOrUpdateUserRunesBatchAsync(userId, runees);

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
            await _runesGalleryService.InsertBatchRunesGalleryAsync(userId, newlyInsertedCards);
        }

        Runes newRune = await _runesService.SumPowerRunesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newRune - (PowerManager)oldRune;

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

    public async Task<bool> UpdateUserRuneLevelAsync(string userId, Runes rune)
    {
        var updateResult = await _userRunesRepository.UpdateUserRuneLevelAsync(userId, rune);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserRuneStarAsync(string userId, Runes rune)
    {
        var updateResult = await _userRunesRepository.UpdateUserRuneStarAsync(userId, rune);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _runesGalleryService.UpdateTempStarRuneGalleryAsync(userId, rune.Id, rune.Star);

        return true;
    }

    public async Task<Runes> GetUserRuneByIdAsync(string userId, string Id)
    {
        var result = await _userRunesRepository.GetUserRuneByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Runes> SumPowerUserRunesAsync(string userId)
    {
        return await _userRunesRepository.SumPowerUserRunesAsync(userId);
    }
}
