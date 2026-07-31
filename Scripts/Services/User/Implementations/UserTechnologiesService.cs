using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTechnologiesService : IUserTechnologiesService
{
    private readonly IUserTechnologiesRepository _userTechnologiesRepository;
    private readonly ITechnologiesGalleryService _technologiesGalleryService;
    private readonly ITechnologiesService _technologiesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserTechnologiesService(
        IUserTechnologiesRepository userTechnologiesRepository,
        ITechnologiesGalleryService technologiesGalleryService,
        ITechnologiesService technologiesService,
        IPowerManagerService powerManagerService)
    {
        _userTechnologiesRepository = userTechnologiesRepository;
        _technologiesGalleryService = technologiesGalleryService;
        _technologiesService = technologiesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserTechnologiesService Create() => ServiceContainer.GetService<IUserTechnologiesService>();

    public async Task<List<Technologies>> GetUserTechnologiesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _userTechnologiesRepository.GetUserTechnologiesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTechnologiesCountAsync(string userId, string search, string rare)
    {
        return await _userTechnologiesRepository.GetUserTechnologiesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologyAsync(string userId, Technologies technology)
    {
        Technologies oldTechnology = await _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        var insertOrUpdateResult = await _userTechnologiesRepository.InsertOrUpdateUserTechnologyAsync(userId, technology);

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

        await _technologiesGalleryService.InsertTechnologyGalleryAsync(userId, technology.Id);

        Technologies newTechnology = await _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologiesBatchAsync(string userId, List<Technologies> technologyes)
    {
        Technologies oldTechnology = await _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        var repositoryResult = await _userTechnologiesRepository.InsertOrUpdateUserTechnologiesBatchAsync(userId, technologyes);

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
            await _technologiesGalleryService.InsertBatchTechnologiesGalleryAsync(userId, newlyInsertedCards);
        }

        Technologies newTechnology = await _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newTechnology - (PowerManager)oldTechnology;

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

    public async Task<bool> UpdateUserTechnologyLevelAsync(string userId, Technologies technology)
    {
        var updateResult = await _userTechnologiesRepository.UpdateUserTechnologyLevelAsync(userId, technology);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserTechnologyStarAsync(string userId, Technologies technology)
    {
        var updateResult = await _userTechnologiesRepository.UpdateUserTechnologyStarAsync(userId, technology);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _technologiesGalleryService.UpdateTempStarTechnologyGalleryAsync(userId, technology.Id, technology.Star);

        return true;
    }

    public async Task<Technologies> GetUserTechnologyByIdAsync(string userId, string Id)
    {
        var result = await _userTechnologiesRepository.GetUserTechnologyByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Technologies> SumPowerUserTechnologiesAsync(string userId)
    {
        return await _userTechnologiesRepository.SumPowerUserTechnologiesAsync(userId);
    }
}
