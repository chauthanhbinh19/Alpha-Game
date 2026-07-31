using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMedalsService : IUserMedalsService
{
    private readonly IUserMedalsRepository _userMedalsRepository;
    private readonly IMedalsGalleryService _medalsGalleryService;
    private readonly IMedalsService _medalsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserMedalsService(
        IUserMedalsRepository userMedalsRepository,
        IMedalsGalleryService medalsGalleryService,
        IMedalsService medalsService,
        IPowerManagerService powerManagerService)
    {
        _userMedalsRepository = userMedalsRepository;
        _medalsGalleryService = medalsGalleryService;
        _medalsService = medalsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserMedalsService Create() => ServiceContainer.GetService<IUserMedalsService>();

    public async Task<List<Medals>> GetUserMedalsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _userMedalsRepository.GetUserMedalsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMedalsCountAsync(string userId, string search, string rare)
    {
        return await _userMedalsRepository.GetUserMedalsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalAsync(string userId, Medals medal)
    {
        Medals oldMedal = await _medalsService.SumPowerMedalsPercentAsync(userId);
        var insertOrUpdateResult = await _userMedalsRepository.InsertOrUpdateUserMedalAsync(userId, medal);

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

        await _medalsGalleryService.InsertMedalGalleryAsync(userId, medal.Id);

        Medals newMedal = await _medalsService.SumPowerMedalsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalsBatchAsync(string userId, List<Medals> medales)
    {
        Medals oldMedal = await _medalsService.SumPowerMedalsPercentAsync(userId);
        var repositoryResult = await _userMedalsRepository.InsertOrUpdateUserMedalsBatchAsync(userId, medales);

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
            await _medalsGalleryService.InsertBatchMedalsGalleryAsync(userId, newlyInsertedCards);
        }

        Medals newMedal = await _medalsService.SumPowerMedalsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newMedal - (PowerManager)oldMedal;

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

    public async Task<bool> UpdateUserMedalLevelAsync(string userId, Medals medal)
    {
        var updateResult = await _userMedalsRepository.UpdateUserMedalLevelAsync(userId, medal);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserMedalStarAsync(string userId, Medals medal)
    {
        var updateResult = await _userMedalsRepository.UpdateUserMedalStarAsync(userId, medal);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _medalsGalleryService.UpdateTempStarMedalGalleryAsync(userId, medal.Id, medal.Star);

        return true;
    }

    public async Task<Medals> GetUserMedalByIdAsync(string userId, string Id)
    {
        var result = await _userMedalsRepository.GetUserMedalByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Medals> SumPowerUserMedalsAsync(string userId)
    {
        return await _userMedalsRepository.SumPowerUserMedalsAsync(userId);
    }
}
