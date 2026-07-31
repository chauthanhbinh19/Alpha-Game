using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArchitecturesService : IUserArchitecturesService
{
    private readonly IUserArchitecturesRepository _userArchitecturesRepository;
    private readonly IArchitecturesGalleryService _architecturesGalleryService;
    private readonly IArchitecturesService _architecturesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserArchitecturesService(
        IUserArchitecturesRepository userArchitecturesRepository,
        IArchitecturesGalleryService architecturesGalleryService,
        IArchitecturesService architecturesService,
        IPowerManagerService powerManagerService)
    {
        _userArchitecturesRepository = userArchitecturesRepository;
        _architecturesGalleryService = architecturesGalleryService;
        _architecturesService = architecturesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserArchitecturesService Create() => ServiceContainer.GetService<IUserArchitecturesService>();

    public async Task<List<Architectures>> GetUserArchitecturesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Architectures> list = await _userArchitecturesRepository.GetUserArchitecturesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArchitecturesCountAsync(string userId, string search, string rare)
    {
        return await _userArchitecturesRepository.GetUserArchitecturesCountAsync(userId, search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchitectureAsync(string userId, Architectures architecture)
    {
        Architectures oldArchitecture = await _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        var insertOrUpdateResult = await _userArchitecturesRepository.InsertOrUpdateUserArchitectureAsync(userId, architecture);

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

        await _architecturesGalleryService.InsertArchitectureGalleryAsync(userId, architecture.Id);

        Architectures newArchitecture = await _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newArchitecture - (PowerManager)oldArchitecture;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchitecturesBatchAsync(string userId, List<Architectures> architecturees)
    {
        Architectures oldArchitecture = await _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        var repositoryResult = await _userArchitecturesRepository.InsertOrUpdateUserArchitecturesBatchAsync(userId, architecturees);

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
            await _architecturesGalleryService.InsertBatchArchitecturesGalleryAsync(userId, newlyInsertedCards);
        }

        Architectures newArchitecture = await _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newArchitecture - (PowerManager)oldArchitecture;

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

    public async Task<bool> UpdateUserArchitectureLevelAsync(string userId, Architectures architecture)
    {
        var updateResult = await _userArchitecturesRepository.UpdateUserArchitectureLevelAsync(userId, architecture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserArchitectureStarAsync(string userId, Architectures architecture)
    {
        var updateResult = await _userArchitecturesRepository.UpdateUserArchitectureStarAsync(userId, architecture);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _architecturesGalleryService.UpdateTempStarArchitectureGalleryAsync(userId, architecture.Id, architecture.Star);

        return true;
    }

    public async Task<Architectures> GetUserArchitectureByIdAsync(string userId, string Id)
    {
        var result = await _userArchitecturesRepository.GetUserArchitectureByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Architectures> SumPowerUserArchitecturesAsync(string userId)
    {
        return await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);
    }
}
