using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSymbolsService : IUserSymbolsService
{
    private readonly IUserSymbolsRepository _userSymbolsRepository;
    private readonly ISymbolsGalleryService _symbolsGalleryService;
    private readonly ISymbolsService _symbolsService;

    public UserSymbolsService(
        IUserSymbolsRepository userSymbolsRepository,
        ISymbolsGalleryService symbolsGalleryService,
        ISymbolsService symbolsService)
    {
        _userSymbolsRepository = userSymbolsRepository;
        _symbolsGalleryService = symbolsGalleryService;
        _symbolsService = symbolsService;
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
        var checkSymbolResult = await _symbolsService.IsSymbolDeletedOrInactiveAsync(symbol.Id);
        if (checkSymbolResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userSymbolsRepository.InsertOrUpdateUserSymbolAsync(userId, symbol);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _symbolsGalleryService.InsertSymbolGalleryAsync(userId, symbol.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolsBatchAsync(string userId, List<Symbols> symbols)
    {
        var insertOrUpdateResult = await _userSymbolsRepository.InsertOrUpdateUserSymbolsBatchAsync(userId, symbols);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _symbolsGalleryService.InsertBatchSymbolsGalleryAsync(userId, newlyInsertedCards);
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
                IsChangePower = false,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserSymbolLevelAsync(string userId, Symbols symbol, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (symbol == null)
        {
            return InsertOrUpdateResult<bool>.Failure("Symbol is required.");
        }

        var checkResult = await _symbolsService.IsSymbolDeletedOrInactiveAsync(symbol.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userSymbolsRepository.UpdateUserSymbolLevelAsync(
            userId,
            symbol,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserSymbolStarAsync(string userId, Symbols symbol)
    {
        var checkSymbolResult = await _symbolsService.IsSymbolDeletedOrInactiveAsync(symbol.Id);
        if (checkSymbolResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userSymbolsRepository.UpdateUserSymbolStarAsync(userId, symbol);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        await _symbolsGalleryService.UpdateTempStarSymbolGalleryAsync(userId, symbol.Id, symbol.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserSymbolQuantityAsync(string userId, Symbols symbol, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userSymbolsRepository.UpdateUserSymbolQuantityAsync(userId, symbol, totalDeductions);

        return result;
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
