using System.Collections.Generic;
using System.Threading.Tasks;

public class UserForgesService : IUserForgesService
{
    private readonly IUserForgesRepository _userForgesRepository;
    private readonly IForgesGalleryService _forgesGalleryService;
    private readonly IForgesService _forgesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserForgesService(
        IUserForgesRepository userForgesRepository,
        IForgesGalleryService forgesGalleryService,
        IForgesService forgesService,
        IPowerManagerService powerManagerService)
    {
        _userForgesRepository = userForgesRepository;
        _forgesGalleryService = forgesGalleryService;
        _forgesService = forgesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserForgesService Create() => ServiceContainer.GetService<IUserForgesService>();

    public async Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _userForgesRepository.GetUserForgesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userForgesRepository.GetUserForgesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgeAsync(string userId, Forges forge)
    {
        Forges oldForge = await _forgesService.SumPowerForgesPercentAsync(userId);
        var insertOrUpdateResult = await _userForgesRepository.InsertOrUpdateUserForgeAsync(userId, forge);

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

        await _forgesGalleryService.InsertForgeGalleryAsync(userId, forge.Id);

        Forges newForge = await _forgesService.SumPowerForgesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newForge - (PowerManager)oldForge;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgesBatchAsync(string userId, List<Forges> forgees)
    {
        Forges oldForge = await _forgesService.SumPowerForgesPercentAsync(userId);
        var repositoryResult = await _userForgesRepository.InsertOrUpdateUserForgesBatchAsync(userId, forgees);

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
            await _forgesGalleryService.InsertBatchForgesGalleryAsync(userId, newlyInsertedCards);
        }

        Forges newForge = await _forgesService.SumPowerForgesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newForge - (PowerManager)oldForge;

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

    public async Task<bool> UpdateUserForgeLevelAsync(string userId, Forges forge)
    {
        var updateResult = await _userForgesRepository.UpdateUserForgeLevelAsync(userId, forge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserForgeStarAsync(string userId, Forges forge)
    {
        var updateResult = await _userForgesRepository.UpdateUserForgeStarAsync(userId, forge);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _forgesGalleryService.UpdateTempStarForgeGalleryAsync(userId, forge.Id, forge.Star);

        return true;
    }

    public async Task<Forges> GetUserForgeByIdAsync(string userId, string Id)
    {
        var result = await _userForgesRepository.GetUserForgeByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Forges> SumPowerUserForgesAsync(string userId)
    {
        return await _userForgesRepository.SumPowerUserForgesAsync(userId);
    }
}
