using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCardLivesService : IUserCardLivesService
{
    private readonly IUserCardLivesRepository _userCardLivesRepository;
    private readonly ICardLivesGalleryService _cardLivesGalleryService;
    private readonly ICardLivesService _cardLivesService;
    private readonly IPowerManagerService _powerManagerService;

    public UserCardLivesService(
        IUserCardLivesRepository userCardLivesRepository,
        ICardLivesGalleryService cardLivesGalleryService,
        ICardLivesService cardLivesService,
        IPowerManagerService powerManagerService)
    {
        _userCardLivesRepository = userCardLivesRepository;
        _cardLivesGalleryService = cardLivesGalleryService;
        _cardLivesService = cardLivesService;
        _powerManagerService = powerManagerService;
    }

    public static IUserCardLivesService Create() => ServiceContainer.GetService<IUserCardLivesService>();

    public async Task<List<CardLives>> GetUserCardLivesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = await _userCardLivesRepository.GetUserCardLivesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCardLivesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCardLivesRepository.GetUserCardLivesCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLifeAsync(string userId, CardLives cardLife)
    {
        var oldCardLifeTask = _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        var oldUserCardLifeTask = _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);

        await Task.WhenAll(oldCardLifeTask, oldUserCardLifeTask);

        CardLives oldCardLife = oldCardLifeTask.Result;
        CardLives oldUserCardLife = oldUserCardLifeTask.Result;

        var insertOrUpdateResult = await _userCardLivesRepository.InsertOrUpdateUserCardLifeAsync(userId, cardLife);

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

        await _cardLivesGalleryService.InsertCardLifeGalleryAsync(userId, cardLife.Id);

        var newCardLifeTask = _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        var newUserCardLifeTask = _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);

        await Task.WhenAll(newCardLifeTask, newUserCardLifeTask);

        PowerManager deltaPower = (PowerManager)newCardLifeTask.Result - (PowerManager)oldCardLife;
        PowerManager deltaUserPower = (PowerManager)newUserCardLifeTask.Result - (PowerManager)oldUserCardLife;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLivesBatchAsync(string userId, List<CardLives> cardLives)
    {
        var oldCardLifeTask = _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        var oldUserCardLifeTask = _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);

        await Task.WhenAll(oldCardLifeTask, oldUserCardLifeTask);

        CardLives oldCardLife = oldCardLifeTask.Result;
        CardLives oldUserCardLife = oldUserCardLifeTask.Result;

        var insertOrUpdateResult = await _userCardLivesRepository.InsertOrUpdateUserCardLivesBatchAsync(userId, cardLives);

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
            await _cardLivesGalleryService.InsertBatchCardLivesGalleryAsync(userId, newlyInsertedCards);

            var newCardLifeTask = _cardLivesService.SumPowerCardLivesPercentAsync(userId);
            var newUserCardLifeTask = _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);

            await Task.WhenAll(newCardLifeTask, newUserCardLifeTask);

            PowerManager deltaPower = (PowerManager)newCardLifeTask.Result - (PowerManager)oldCardLife;
            PowerManager deltaUserPower = (PowerManager)newUserCardLifeTask.Result - (PowerManager)oldUserCardLife;

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

    public async Task<bool> UpdateUserCardLifeLevelAsync(string userId, CardLives cardLife)
    {
        CardLives oldUserCardLife = await _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);

        var updateResult = await _userCardLivesRepository.UpdateUserCardLifeLevelAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        CardLives newUserCardLife = await _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCardLife - (PowerManager)oldUserCardLife;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserCardLifeStarAsync(string userId, CardLives cardLife)
    {
        CardLives oldUserCardLife = await _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);

        var updateResult = await _userCardLivesRepository.UpdateUserCardLifeStarAsync(userId, cardLife);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _cardLivesGalleryService.UpdateTempStarCardLifeGalleryAsync(userId, cardLife.Id, cardLife.Star);

        CardLives newUserCardLife = await _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserCardLife - (PowerManager)oldUserCardLife;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<CardLives> GetUserCardLifeByIdAsync(string userId, string Id)
    {
        var result = await _userCardLivesRepository.GetUserCardLifeByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<CardLives> SumPowerUserCardLivesAsync(string userId)
    {
        return await _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);
    }
}
