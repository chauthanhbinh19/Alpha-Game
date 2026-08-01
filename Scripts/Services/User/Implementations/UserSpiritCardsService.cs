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
        var oldSpiritCardTask = _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        var oldUserSpiritCardTask = _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);

        await Task.WhenAll(oldSpiritCardTask, oldUserSpiritCardTask);

        SpiritCards oldSpiritCard = oldSpiritCardTask.Result;
        SpiritCards oldUserSpiritCard = oldUserSpiritCardTask.Result;

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

        var newSpiritCardTask = _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        var newUserSpiritCardTask = _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);

        await Task.WhenAll(newSpiritCardTask, newUserSpiritCardTask);

        PowerManager deltaPower = (PowerManager)newSpiritCardTask.Result - (PowerManager)oldSpiritCard;
        PowerManager deltaUserPower = (PowerManager)newUserSpiritCardTask.Result - (PowerManager)oldUserSpiritCard;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritCardsBatchAsync(string userId, List<SpiritCards> spiritCards)
    {
        var oldSpiritCardTask = _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        var oldUserSpiritCardTask = _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);

        await Task.WhenAll(oldSpiritCardTask, oldUserSpiritCardTask);

        SpiritCards oldSpiritCard = oldSpiritCardTask.Result;
        SpiritCards oldUserSpiritCard = oldUserSpiritCardTask.Result;

        var insertOrUpdateResult = await _userSpiritCardsRepository.InsertOrUpdateUserSpiritCardsBatchAsync(userId, spiritCards);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _spiritCardsGalleryService.InsertBatchSpiritCardsGalleryAsync(userId, newlyInsertedCards);

            var newSpiritCardTask = _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
            var newUserSpiritCardTask = _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);

            await Task.WhenAll(newSpiritCardTask, newUserSpiritCardTask);

            PowerManager deltaPower = (PowerManager)newSpiritCardTask.Result - (PowerManager)oldSpiritCard;
            PowerManager deltaUserPower = (PowerManager)newUserSpiritCardTask.Result - (PowerManager)oldUserSpiritCard;

            PowerManager totalDelta = new PowerManager();
            if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
            if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

            if (totalDelta.HasAnyPositiveStat())
            {
                PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
                PowerManager updatedPower = currentPower + totalDelta;
                await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
            }
        }

        return insertOrUpdateResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserSpiritCardLevelAsync(string userId, SpiritCards spiritCard)
    {
        SpiritCards oldUserSpiritCard = await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);

        var updateResult = await _userSpiritCardsRepository.UpdateUserSpiritCardLevelAsync(userId, spiritCard);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        SpiritCards newUserSpiritCard = await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserSpiritCard - (PowerManager)oldUserSpiritCard;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserSpiritCardStarAsync(string userId, SpiritCards spiritCard)
    {
        SpiritCards oldUserSpiritCard = await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);

        var updateResult = await _userSpiritCardsRepository.UpdateUserSpiritCardStarAsync(userId, spiritCard);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _spiritCardsGalleryService.UpdateTempStarSpiritCardGalleryAsync(userId, spiritCard.Id, spiritCard.Star);

        SpiritCards newUserSpiritCard = await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserSpiritCard - (PowerManager)oldUserSpiritCard;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

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
