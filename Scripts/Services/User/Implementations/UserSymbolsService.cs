using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSymbolsService : IUserSymbolsService
{
    private readonly IUserSymbolsRepository _userSymbolsRepository;
    private readonly ISymbolsGalleryService _symbolsGalleryService;
    private readonly ISymbolsService _symbolsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserSymbolsService(
        IUserSymbolsRepository userSymbolsRepository,
        ISymbolsGalleryService symbolsGalleryService,
        ISymbolsService symbolsService,
        IPowerManagerService powerManagerService)
    {
        _userSymbolsRepository = userSymbolsRepository;
        _symbolsGalleryService = symbolsGalleryService;
        _symbolsService = symbolsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserSymbolsService Create() => ServiceContainer.GetService<IUserSymbolsService>();

    public async Task<List<Symbols>> GetUserSymbolsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> result = await _userSymbolsRepository.GetUserSymbolsAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserSymbolsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userSymbolsRepository.GetUserSymbolsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolAsync(string userId, Symbols symbol)
    {
        var oldSymbolTask = _symbolsService.SumPowerSymbolsPercentAsync(userId);
        var oldUserSymbolTask = _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);

        await Task.WhenAll(oldSymbolTask, oldUserSymbolTask);

        Symbols oldSymbol = oldSymbolTask.Result;
        Symbols oldUserSymbol = oldUserSymbolTask.Result;

        var insertOrUpdateResult = await _userSymbolsRepository.InsertOrUpdateUserSymbolAsync(userId, symbol);

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

        await _symbolsGalleryService.InsertSymbolGalleryAsync(userId, symbol.Id);

        var newSymbolTask = _symbolsService.SumPowerSymbolsPercentAsync(userId);
        var newUserSymbolTask = _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);

        await Task.WhenAll(newSymbolTask, newUserSymbolTask);

        PowerManager deltaPower = (PowerManager)newSymbolTask.Result - (PowerManager)oldSymbol;
        PowerManager deltaUserPower = (PowerManager)newUserSymbolTask.Result - (PowerManager)oldUserSymbol;

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

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolsBatchAsync(string userId, List<Symbols> symbols)
    {
        var oldSymbolTask = _symbolsService.SumPowerSymbolsPercentAsync(userId);
        var oldUserSymbolTask = _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);

        await Task.WhenAll(oldSymbolTask, oldUserSymbolTask);

        Symbols oldSymbol = oldSymbolTask.Result;
        Symbols oldUserSymbol = oldUserSymbolTask.Result;

        var insertOrUpdateResult = await _userSymbolsRepository.InsertOrUpdateUserSymbolsBatchAsync(userId, symbols);

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
            await _symbolsGalleryService.InsertBatchSymbolsGalleryAsync(userId, newlyInsertedCards);

            var newSymbolTask = _symbolsService.SumPowerSymbolsPercentAsync(userId);
            var newUserSymbolTask = _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);

            await Task.WhenAll(newSymbolTask, newUserSymbolTask);

            PowerManager deltaPower = (PowerManager)newSymbolTask.Result - (PowerManager)oldSymbol;
            PowerManager deltaUserPower = (PowerManager)newUserSymbolTask.Result - (PowerManager)oldUserSymbol;

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

    public async Task<bool> UpdateUserSymbolLevelAsync(string userId, Symbols symbol)
    {
        Symbols oldUserSymbol = await _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);

        var updateResult = await _userSymbolsRepository.UpdateUserSymbolLevelAsync(userId, symbol);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        Symbols newUserSymbol = await _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserSymbol - (PowerManager)oldUserSymbol;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<bool> UpdateUserSymbolStarAsync(string userId, Symbols symbol)
    {
        Symbols oldUserSymbol = await _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);

        var updateResult = await _userSymbolsRepository.UpdateUserSymbolStarAsync(userId, symbol);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _symbolsGalleryService.UpdateTempStarSymbolGalleryAsync(userId, symbol.Id, symbol.Star);

        Symbols newUserSymbol = await _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);
        PowerManager deltaUserPower = (PowerManager)newUserSymbol - (PowerManager)oldUserSymbol;

        if (deltaUserPower.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            PowerManager updatedPower = currentPower + deltaUserPower;
            await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);
        }

        return true;
    }

    public async Task<Symbols> GetUserSymbolByIdAsync(string userId, string Id)
    {
        var result = await _userSymbolsRepository.GetUserSymbolByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<Symbols> SumPowerUserSymbolsAsync(string userId)
    {
        return await _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);
    }
}
