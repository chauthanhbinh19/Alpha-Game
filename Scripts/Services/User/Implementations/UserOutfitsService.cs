using System.Collections.Generic;
using System.Threading.Tasks;

public class UserOutfitsService : IUserOutfitsService
{
    private readonly IUserOutfitsRepository _userOutfitsRepository;
    private readonly IOutfitsGalleryService _outfitsGalleryService;
    private readonly IOutfitsService _outfitsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserOutfitsService(
        IUserOutfitsRepository userOutfitsRepository,
        IOutfitsGalleryService outfitsGalleryService,
        IOutfitsService outfitsService,
        IPowerManagerService powerManagerService)
    {
        _userOutfitsRepository = userOutfitsRepository;
        _outfitsGalleryService = outfitsGalleryService;
        _outfitsService = outfitsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserOutfitsService Create() => ServiceContainer.GetService<IUserOutfitsService>();

    public async Task<List<Outfits>> GetUserOutfitsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> list = await _userOutfitsRepository.GetUserOutfitsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserOutfitsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userOutfitsRepository.GetUserOutfitsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserOutfitAsync(string userId, Outfits outfit)
    {
        Outfits oldOutfit = await _outfitsService.SumPowerOutfitsPercentAsync(userId);
        var insertOrUpdateResult = await _userOutfitsRepository.InsertOrUpdateUserOutfitAsync(userId, outfit);

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

        await _outfitsGalleryService.InsertOutfitGalleryAsync(userId, outfit.Id);

        Outfits newOutfit = await _outfitsService.SumPowerOutfitsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newOutfit - (PowerManager)oldOutfit;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserOutfitsBatchAsync(string userId, List<Outfits> outfites)
    {
        Outfits oldOutfit = await _outfitsService.SumPowerOutfitsPercentAsync(userId);
        var repositoryResult = await _userOutfitsRepository.InsertOrUpdateUserOutfitsBatchAsync(userId, outfites);

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
            await _outfitsGalleryService.InsertBatchOutfitsGalleryAsync(userId, newlyInsertedCards);
        }

        Outfits newOutfit = await _outfitsService.SumPowerOutfitsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newOutfit - (PowerManager)oldOutfit;

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

    public async Task<bool> UpdateUserOutfitLevelAsync(string userId, Outfits outfit)
    {
        var updateResult = await _userOutfitsRepository.UpdateUserOutfitLevelAsync(userId, outfit);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserOutfitStarAsync(string userId, Outfits outfit)
    {
        var updateResult = await _userOutfitsRepository.UpdateUserOutfitStarAsync(userId, outfit);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _outfitsGalleryService.UpdateTempStarOutfitGalleryAsync(userId, outfit.Id, outfit.Star);

        return true;
    }

    public async Task<Outfits> GetUserOutfitByIdAsync(string userId, string Id)
    {
        var result = await _userOutfitsRepository.GetUserOutfitByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Outfits> SumPowerUserOutfitsAsync(string userId)
    {
        return await _userOutfitsRepository.SumPowerUserOutfitsAsync(userId);
    }
}
