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
        List<Symbols> list = await _userSymbolsRepository.GetUserSymbolsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSymbolsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userSymbolsRepository.GetUserSymbolsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolAsync(string userId, Symbols symbol)
    {
        Symbols oldSymbol = await _symbolsService.SumPowerSymbolsPercentAsync(userId);
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

        Symbols newSymbol = await _symbolsService.SumPowerSymbolsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newSymbol - (PowerManager)oldSymbol;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolsBatchAsync(string userId, List<Symbols> symboles)
    {
        Symbols oldSymbol = await _symbolsService.SumPowerSymbolsPercentAsync(userId);
        var repositoryResult = await _userSymbolsRepository.InsertOrUpdateUserSymbolsBatchAsync(userId, symboles);

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
            await _symbolsGalleryService.InsertBatchSymbolsGalleryAsync(userId, newlyInsertedCards);
        }

        Symbols newSymbol = await _symbolsService.SumPowerSymbolsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newSymbol - (PowerManager)oldSymbol;

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

    public async Task<bool> UpdateUserSymbolLevelAsync(string userId, Symbols symbol)
    {
        var updateResult = await _userSymbolsRepository.UpdateUserSymbolLevelAsync(userId, symbol);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserSymbolStarAsync(string userId, Symbols symbol)
    {
        var updateResult = await _userSymbolsRepository.UpdateUserSymbolStarAsync(userId, symbol);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _symbolsGalleryService.UpdateTempStarSymbolGalleryAsync(userId, symbol.Id, symbol.Star);

        return true;
    }

    public async Task<Symbols> GetUserSymbolByIdAsync(string userId, string Id)
    {
        var result = await _userSymbolsRepository.GetUserSymbolByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<Symbols> SumPowerUserSymbolsAsync(string userId)
    {
        return await _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);
    }
}
