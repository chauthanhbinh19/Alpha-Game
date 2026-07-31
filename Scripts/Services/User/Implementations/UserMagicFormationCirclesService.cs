using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMagicFormationCirclesService : IUserMagicFormationCirclesService
{
    private readonly IUserMagicFormationCirclesRepository _userMagicFormationCirclesRepository;
    private readonly IMagicFormationCirclesGalleryService _magicFormationCirclesGalleryService;
    private readonly IMagicFormationCirclesService _magicFormationCirclesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserMagicFormationCirclesService(
        IUserMagicFormationCirclesRepository userMagicFormationCirclesRepository,
        IMagicFormationCirclesGalleryService magicFormationCirclesGalleryService,
        IMagicFormationCirclesService magicFormationCirclesService,
        IPowerManagerService powerManagerService)
    {
        _userMagicFormationCirclesRepository = userMagicFormationCirclesRepository;
        _magicFormationCirclesGalleryService = magicFormationCirclesGalleryService;
        _magicFormationCirclesService = magicFormationCirclesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserMagicFormationCirclesService Create() => ServiceContainer.GetService<IUserMagicFormationCirclesService>();

    public async Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMagicFormationCirclesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCircleAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        MagicFormationCircles oldMagicFormationCircle = await _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        var insertOrUpdateResult = await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCircleAsync(userId, magicFormationCircle);

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

        await _magicFormationCirclesGalleryService.InsertMagicFormationCircleGalleryAsync(userId, magicFormationCircle.Id);

        MagicFormationCircles newMagicFormationCircle = await _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCirclesBatchAsync(string userId, List<MagicFormationCircles> magicFormationCirclees)
    {
        MagicFormationCircles oldMagicFormationCircle = await _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        var repositoryResult = await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCirclesBatchAsync(userId, magicFormationCirclees);

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
            await _magicFormationCirclesGalleryService.InsertBatchMagicFormationCirclesGalleryAsync(userId, newlyInsertedCards);
        }

        MagicFormationCircles newMagicFormationCircle = await _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newMagicFormationCircle - (PowerManager)oldMagicFormationCircle;

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

    public async Task<bool> UpdateUserMagicFormationCircleLevelAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        var updateResult = await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleLevelAsync(userId, magicFormationCircle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserMagicFormationCircleStarAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        var updateResult = await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleStarAsync(userId, magicFormationCircle);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _magicFormationCirclesGalleryService.UpdateTempStarMagicFormationCircleGalleryAsync(userId, magicFormationCircle.Id, magicFormationCircle.Star);

        return true;
    }

    public async Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string userId, string Id)
    {
        var result = await _userMagicFormationCirclesRepository.GetUserMagicFormationCircleByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync(string userId)
    {
        return await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);
    }
}
