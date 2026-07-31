using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPuppetsService : IUserPuppetsService
{
    private readonly IUserPuppetsRepository _userPuppetsRepository;
    private readonly IPuppetsGalleryService _puppetsGalleryService;
    private readonly IPuppetsService _puppetsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserPuppetsService(
        IUserPuppetsRepository userPuppetsRepository,
        IPuppetsGalleryService puppetsGalleryService,
        IPuppetsService puppetsService,
        IPowerManagerService powerManagerService)
    {
        _userPuppetsRepository = userPuppetsRepository;
        _puppetsGalleryService = puppetsGalleryService;
        _puppetsService = puppetsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserPuppetsService Create() => ServiceContainer.GetService<IUserPuppetsService>();

    public async Task<List<Puppets>> GetUserPuppetsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _userPuppetsRepository.GetUserPuppetsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPuppetsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userPuppetsRepository.GetUserPuppetsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetAsync(string userId, Puppets cardLife)
    {
        Puppets oldPuppet = await _puppetsService.SumPowerPuppetsPercentAsync(userId);
        var insertOrUpdateResult = await _userPuppetsRepository.InsertOrUpdateUserPuppetAsync(userId, cardLife);

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

        await _puppetsGalleryService.InsertPuppetGalleryAsync(userId, cardLife.Id);

        Puppets newPuppet = await _puppetsService.SumPowerPuppetsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newPuppet - (PowerManager)oldPuppet;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetsBatchAsync(string userId, List<Puppets> cardLifees)
    {
        Puppets oldPuppet = await _puppetsService.SumPowerPuppetsPercentAsync(userId);
        var repositoryResult = await _userPuppetsRepository.InsertOrUpdateUserPuppetsBatchAsync(userId, cardLifees);

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
            await _puppetsGalleryService.InsertBatchPuppetsGalleryAsync(userId, newlyInsertedCards);
        }

        Puppets newPuppet = await _puppetsService.SumPowerPuppetsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newPuppet - (PowerManager)oldPuppet;

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

    public async Task<bool> UpdateUserPuppetLevelAsync(string userId, Puppets cardLife)
    {
        var updateResult = await _userPuppetsRepository.UpdateUserPuppetLevelAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserPuppetStarAsync(string userId, Puppets cardLife)
    {
        var updateResult = await _userPuppetsRepository.UpdateUserPuppetStarAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _puppetsGalleryService.UpdateTempStarPuppetGalleryAsync(userId, cardLife.Id, cardLife.Star);

        return true;
    }

    public async Task<Puppets> GetUserPuppetByIdAsync(string userId, string Id)
    {
        var result = await _userPuppetsRepository.GetUserPuppetByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Puppets> SumPowerUserPuppetsAsync(string userId)
    {
        return await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);
    }
}
