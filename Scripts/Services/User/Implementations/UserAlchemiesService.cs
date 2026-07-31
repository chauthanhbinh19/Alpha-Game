
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAlchemiesService : IUserAlchemiesService
{
    private readonly IUserAlchemiesRepository _userAlchemiesRepository;
    private readonly IAlchemiesGalleryService _alchemiesGalleryService;
    private readonly IAlchemiesService _alchemiesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserAlchemiesService(
        IUserAlchemiesRepository userAlchemiesRepository,
        IAlchemiesGalleryService alchemiesGalleryService,
        IAlchemiesService alchemiesService,
        IPowerManagerService powerManagerService)
    {
        _userAlchemiesRepository = userAlchemiesRepository;
        _alchemiesGalleryService = alchemiesGalleryService;
        _alchemiesService = alchemiesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserAlchemiesService Create() => ServiceContainer.GetService<IUserAlchemiesService>();

    public async Task<List<Alchemies>> GetUserAlchemiesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _userAlchemiesRepository.GetUserAlchemiesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAlchemiesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userAlchemiesRepository.GetUserAlchemiesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAlchemyAsync(string userId, Alchemies alchemy)
    {
        Alchemies oldAlchemy = await _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        var insertOrUpdateResult = await _userAlchemiesRepository.InsertOrUpdateUserAlchemyAsync(userId, alchemy);

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

        await _alchemiesGalleryService.InsertAlchemyGalleryAsync(userId, alchemy.Id);

        Alchemies newAlchemy = await _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAlchemiesBatchAsync(string userId, List<Alchemies> alchemyes)
    {
        Alchemies oldAlchemy = await _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        var repositoryResult = await _userAlchemiesRepository.InsertOrUpdateUserAlchemiesBatchAsync(userId, alchemyes);

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
            await _alchemiesGalleryService.InsertBatchAlchemiesGalleryAsync(userId, newlyInsertedCards);
        }

        Alchemies newAlchemy = await _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newAlchemy - (PowerManager)oldAlchemy;

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

    public async Task<bool> UpdateUserAlchemyLevelAsync(string userId, Alchemies alchemy)
    {
        var updateResult = await _userAlchemiesRepository.UpdateUserAlchemyLevelAsync(userId, alchemy);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserAlchemyStarAsync(string userId, Alchemies alchemy)
    {
        var updateResult = await _userAlchemiesRepository.UpdateUserAlchemyStarAsync(userId, alchemy);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _alchemiesGalleryService.UpdateTempStarAlchemyGalleryAsync(userId, alchemy.Id, alchemy.Star);

        return true;
    }

    public async Task<Alchemies> GetUserAlchemyByIdAsync(string userId, string Id)
    {
        var result = await _userAlchemiesRepository.GetUserAlchemyByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Alchemies> SumPowerUserAlchemiesAsync(string userId)
    {
        return await _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);
    }
}
