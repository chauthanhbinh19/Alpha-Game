using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritCardsService : IUserSpiritCardsService
{
    private readonly IUserSpiritCardsRepository _userSpiritCardsRepository;
    private readonly ISpiritCardsGalleryService _spiritCardsGalleryService;
    private readonly ISpiritCardsService _spiritCardsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserSpiritCardsService(
        IUserSpiritCardsRepository userSpiritCardsRepository,
        ISpiritCardsGalleryService spiritCardsGalleryService,
        ISpiritCardsService spiritCardsService,
        IPowerManagerService powerManagerService)
    {
        _userSpiritCardsRepository = userSpiritCardsRepository;
        _spiritCardsGalleryService = spiritCardsGalleryService;
        _spiritCardsService = spiritCardsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserSpiritCardsService Create() => ServiceContainer.GetService<IUserSpiritCardsService>();

    public async Task<List<SpiritCards>> GetUserSpiritCardAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _userSpiritCardsRepository.GetUserSpiritCardsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritCardCountAsync(string userId, string search, string type, string rare)
    {
        return await _userSpiritCardsRepository.GetUserSpiritCardsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritCardAsync(string userId, SpiritCards spiritCard)
    {
        SpiritCards oldSpiritCard = await _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        var insertOrUpdateResult = await _userSpiritCardsRepository.InsertOrUpdateUserSpiritCardAsync(userId, spiritCard);

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

        await _spiritCardsGalleryService.InsertSpiritCardGalleryAsync(userId, spiritCard.Id);

        SpiritCards newSpiritCard = await _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritCard - (PowerManager)oldSpiritCard;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritCardsBatchAsync(string userId, List<SpiritCards> spiritCardes)
    {
        SpiritCards oldSpiritCard = await _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        var repositoryResult = await _userSpiritCardsRepository.InsertOrUpdateUserSpiritCardsBatchAsync(userId, spiritCardes);

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
            await _spiritCardsGalleryService.InsertBatchSpiritCardsGalleryAsync(userId, newlyInsertedCards);
        }

        SpiritCards newSpiritCard = await _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newSpiritCard - (PowerManager)oldSpiritCard;

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

    public async Task<bool> UpdateUserSpiritCardLevelAsync(string userId, SpiritCards spiritCard)
    {
        var updateResult = await _userSpiritCardsRepository.UpdateUserSpiritCardLevelAsync(userId, spiritCard);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserSpiritCardStarAsync(string userId, SpiritCards spiritCard)
    {
        var updateResult = await _userSpiritCardsRepository.UpdateUserSpiritCardStarAsync(userId, spiritCard);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _spiritCardsGalleryService.UpdateTempStarSpiritCardGalleryAsync(userId, spiritCard.Id, spiritCard.Star);

        return true;
    }

    public async Task<SpiritCards> GetUserSpiritCardByIdAsync(string userId, string Id)
    {
        var result = await _userSpiritCardsRepository.GetUserSpiritCardByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<SpiritCards> SumPowerUserSpiritCardsAsync(string userId)
    {
        return await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);
    }
}
