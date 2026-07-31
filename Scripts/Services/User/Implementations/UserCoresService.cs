using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCoresService : IUserCoresService
{
    private readonly IUserCoresRepository _userCoresRepository;
    private readonly ICoresGalleryService _coresGalleryService;
    private readonly ICoresService _coresService;
    private readonly IPowerManagerService _powerManagerService;

    public UserCoresService(
        IUserCoresRepository userCoresRepository,
        ICoresGalleryService coresGalleryService,
        ICoresService coresService,
        IPowerManagerService powerManagerService)
    {
        _userCoresRepository = userCoresRepository;
        _coresGalleryService = coresGalleryService;
        _coresService = coresService;
        _powerManagerService = powerManagerService;
    }

    public static IUserCoresService Create() => ServiceContainer.GetService<IUserCoresService>();

    public async Task<List<Cores>> GetUserCoresAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _userCoresRepository.GetUserCoresAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCoresCountAsync(string userId, string search, string rare)
    {
        return await _userCoresRepository.GetUserCoresCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoreAsync(string userId, Cores core)
    {
        Cores oldCore = await _coresService.SumPowerCoresPercentAsync(userId);
        var insertOrUpdateResult = await _userCoresRepository.InsertOrUpdateUserCoreAsync(userId, core);

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

        await _coresGalleryService.InsertCoreGalleryAsync(userId, core.Id);

        Cores newCore = await _coresService.SumPowerCoresPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newCore - (PowerManager)oldCore;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoresBatchAsync(string userId, List<Cores> corees)
    {
        Cores oldCore = await _coresService.SumPowerCoresPercentAsync(userId);
        var repositoryResult = await _userCoresRepository.InsertOrUpdateUserCoresBatchAsync(userId, corees);

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
            await _coresGalleryService.InsertBatchCoresGalleryAsync(userId, newlyInsertedCards);
        }

        Cores newCore = await _coresService.SumPowerCoresPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newCore - (PowerManager)oldCore;

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

    public async Task<bool> UpdateUserCoreLevelAsync(string userId, Cores core)
    {
        var updateResult = await _userCoresRepository.UpdateUserCoreLevelAsync(userId, core);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserCoreStarAsync(string userId, Cores core)
    {
        var updateResult = await _userCoresRepository.UpdateUserCoreStarAsync(userId, core);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _coresGalleryService.UpdateTempStarCoreGalleryAsync(userId, core.Id, core.Star);

        return true;
    }

    public async Task<Cores> GetUserCoreByIdAsync(string userId, string Id)
    {
        var result = await _userCoresRepository.GetUserCoreByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Cores> SumPowerUserCoresAsync(string userId)
    {
        return await _userCoresRepository.SumPowerUserCoresAsync(userId);
    }
}
