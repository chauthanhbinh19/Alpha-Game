using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMechaBeastsService : IUserMechaBeastsService
{
    private readonly IUserMechaBeastsRepository _userMechaBeastsRepository;
    private readonly IMechaBeastsGalleryService _mechaBeastsGalleryService;
    private readonly IMechaBeastsService _mechaBeastsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserMechaBeastsService(
        IUserMechaBeastsRepository userMechaBeastsRepository,
        IMechaBeastsGalleryService mechaBeastsGalleryService,
        IMechaBeastsService mechaBeastsService,
        IPowerManagerService powerManagerService)
    {
        _userMechaBeastsRepository = userMechaBeastsRepository;
        _mechaBeastsGalleryService = mechaBeastsGalleryService;
        _mechaBeastsService = mechaBeastsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserMechaBeastsService Create() => ServiceContainer.GetService<IUserMechaBeastsService>();

    public async Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _userMechaBeastsRepository.GetUserMechaBeastsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMechaBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastsCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastAsync(string userId, MechaBeasts mechaBeast)
    {
        MechaBeasts oldMechaBeast = await _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        var insertOrUpdateResult = await _userMechaBeastsRepository.InsertOrUpdateUserMechaBeastAsync(userId, mechaBeast);

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

        await _mechaBeastsGalleryService.InsertMechaBeastGalleryAsync(userId, mechaBeast.Id);

        MechaBeasts newMechaBeast = await _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastsBatchAsync(string userId, List<MechaBeasts> mechaBeastes)
    {
        MechaBeasts oldMechaBeast = await _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        var repositoryResult = await _userMechaBeastsRepository.InsertOrUpdateUserMechaBeastsBatchAsync(userId, mechaBeastes);

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
            await _mechaBeastsGalleryService.InsertBatchMechaBeastsGalleryAsync(userId, newlyInsertedCards);
        }

        MechaBeasts newMechaBeast = await _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newMechaBeast - (PowerManager)oldMechaBeast;

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

    public async Task<bool> UpdateUserMechaBeastLevelAsync(string userId, MechaBeasts mechaBeast)
    {
        var updateResult = await _userMechaBeastsRepository.UpdateUserMechaBeastLevelAsync(userId, mechaBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserMechaBeastStarAsync(string userId, MechaBeasts mechaBeast)
    {
        var updateResult = await _userMechaBeastsRepository.UpdateUserMechaBeastStarAsync(userId, mechaBeast);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _mechaBeastsGalleryService.UpdateTempStarMechaBeastGalleryAsync(userId, mechaBeast.Id, mechaBeast.Star);

        return true;
    }

    public async Task<MechaBeasts> GetUserMechaBeastByIdAsync(string userId, string Id)
    {
        var result = await _userMechaBeastsRepository.GetUserMechaBeastByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<MechaBeasts> SumPowerUserMechaBeastsAsync(string userId)
    {
        return await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);
    }
}
