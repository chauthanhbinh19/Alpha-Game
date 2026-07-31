using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFashionsService : IUserFashionsService
{
    private readonly IUserFashionsRepository _userFashionsRepository;
    private readonly IFashionsGalleryService _fashionsGalleryService;
    private readonly IFashionsService _fashionsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserFashionsService(
        IUserFashionsRepository userFashionsRepository,
        IFashionsGalleryService fashionsGalleryService,
        IFashionsService fashionsService,
        IPowerManagerService powerManagerService)
    {
        _userFashionsRepository = userFashionsRepository;
        _fashionsGalleryService = fashionsGalleryService;
        _fashionsService = fashionsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserFashionsService Create() => ServiceContainer.GetService<IUserFashionsService>();

    public async Task<List<Fashions>> GetUserFashionsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _userFashionsRepository.GetUserFashionsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFashionsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userFashionsRepository.GetUserFashionsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionAsync(string userId, Fashions fashion)
    {
        Fashions oldFashion = await _fashionsService.SumPowerFashionsPercentAsync(userId);
        var insertOrUpdateResult = await _userFashionsRepository.InsertOrUpdateUserFashionAsync(userId, fashion);

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

        await _fashionsGalleryService.InsertFashionGalleryAsync(userId, fashion.Id);

        Fashions newFashion = await _fashionsService.SumPowerFashionsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionsBatchAsync(string userId, List<Fashions> fashiones)
    {
        Fashions oldFashion = await _fashionsService.SumPowerFashionsPercentAsync(userId);
        var repositoryResult = await _userFashionsRepository.InsertOrUpdateUserFashionsBatchAsync(userId, fashiones);

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
            await _fashionsGalleryService.InsertBatchFashionsGalleryAsync(userId, newlyInsertedCards);
        }

        Fashions newFashion = await _fashionsService.SumPowerFashionsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newFashion - (PowerManager)oldFashion;

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

    public async Task<bool> UpdateUserFashionLevelAsync(string userId, Fashions fashion)
    {
        var updateResult = await _userFashionsRepository.UpdateUserFashionLevelAsync(userId, fashion);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserFashionStarAsync(string userId, Fashions fashion)
    {
        var updateResult = await _userFashionsRepository.UpdateUserFashionStarAsync(userId, fashion);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _fashionsGalleryService.UpdateTempStarFashionGalleryAsync(userId, fashion.Id, fashion.Star);

        return true;
    }

    public async Task<Fashions> GetUserFashionByIdAsync(string userId, string Id)
    {
        var result = await _userFashionsRepository.GetUserFashionByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Fashions> SumPowerUserFashionsAsync(string userId)
    {
        return await _userFashionsRepository.SumPowerUserFashionsAsync(userId);
    }
}
