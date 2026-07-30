using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SymbolsGalleryService : ISymbolsGalleryService
{
    private readonly ISymbolsGalleryRepository _symbolsGalleryRepository;
    private readonly ISymbolsService _symbolsService;
    private readonly IPowerManagerService _powerManagerService;

    public SymbolsGalleryService(
        ISymbolsGalleryRepository symbolsGalleryRepository,
        ISymbolsService symbolsService,
        IPowerManagerService powerManagerService)
    {
        _symbolsGalleryRepository = symbolsGalleryRepository;
        _symbolsService = symbolsService;
        _powerManagerService = powerManagerService;
    }

    public static ISymbolsGalleryService Create() => ServiceContainer.GetService<ISymbolsGalleryService>();

    public async Task<List<Symbols>> GetSymbolsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = await _symbolsGalleryRepository.GetSymbolsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSymbolsCountAsync(string search, string type, string rare)
    {
        return await _symbolsGalleryRepository.GetSymbolsCountAsync(search, type, rare);
    }

    public async Task<bool> InsertSymbolGalleryAsync(string userId, string Id)
    {
        var insertResult = await _symbolsGalleryRepository.InsertSymbolGalleryAsync(userId, Id, await _symbolsService.GetSymbolByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusSymbolGalleryAsync(string userId, string symbolId)
    {
        var updateResult = await _symbolsGalleryRepository.UpdateStatusSymbolGalleryAsync(userId, symbolId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Symbols symbolGallery = await GetSymbolCollectionByIdAsync(userId, symbolId) ?? new Symbols();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)symbolGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusSymbolsGalleryAsync(string userId)
    {
        Symbols oldSymbol = await SumPowerSymbolsGalleryAsync(userId);

        var updateResult = await _symbolsGalleryRepository.UpdateBatchStatusSymbolsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Symbols newSymbol = await SumPowerSymbolsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSymbol - (PowerManager)oldSymbol;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Symbols> SumPowerSymbolsGalleryAsync(string userId)
    {
        return await _symbolsGalleryRepository.SumPowerSymbolsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarSymbolGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _symbolsGalleryRepository.UpdateStarSymbolGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarSymbolGalleryAsync(string userId, string symbolId)
    {
        Symbols oldSymbol = await GetSymbolCollectionByIdAsync(userId, symbolId) ?? new Symbols();

        var updateResult = await _symbolsGalleryRepository.UpdateCurrentStarSymbolGalleryAsync(userId, symbolId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Symbols newSymbol = await GetSymbolCollectionByIdAsync(userId, symbolId) ?? new Symbols();
        PowerManager deltaPower = (PowerManager)newSymbol - (PowerManager)oldSymbol;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarSymbolsGalleryAsync(string userId)
    {
        Symbols oldSymbol = await SumPowerSymbolsGalleryAsync(userId);

        var updateResult = await _symbolsGalleryRepository.UpdateBatchCurrentStarSymbolsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Symbols newSymbol = await SumPowerSymbolsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSymbol - (PowerManager)oldSymbol;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchSymbolsGalleryAsync(string userId, List<Symbols> symbols)
    {
        var insertResult = await _symbolsGalleryRepository.InsertBatchSymbolsGalleryAsync(userId, symbols);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Symbols> GetSymbolCollectionByIdAsync(string userId, string symbolId)
    {
        var result = await _symbolsGalleryRepository.GetSymbolCollectionByIdAsync(userId, symbolId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateSymbolGalleryPowerAsync(string userId, string Id)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        await _symbolsGalleryRepository.UpdateSymbolGalleryPowerAsync(userId, Id, await _service.GetSymbolByIdAsync(Id));
    }
}
