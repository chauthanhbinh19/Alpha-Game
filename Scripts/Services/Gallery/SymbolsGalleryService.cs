using System.Collections.Generic;
using System.Threading.Tasks;

public class SymbolsGalleryService : ISymbolsGalleryService
{
    private readonly ISymbolsGalleryRepository _symbolsGalleryRepository;

    public SymbolsGalleryService(ISymbolsGalleryRepository symbolsGalleryRepository)
    {
        _symbolsGalleryRepository = symbolsGalleryRepository;
    }

    public static SymbolsGalleryService Create()
    {
        return new SymbolsGalleryService(new SymbolsGalleryRepository());
    }

    public async Task<List<Symbols>> GetSymbolsCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = await _symbolsGalleryRepository.GetSymbolsCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSymbolsCountAsync(string type, string rare)
    {
        return await _symbolsGalleryRepository.GetSymbolsCountAsync(type, rare);
    }

    public async Task InsertSymbolGalleryAsync(string Id)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        await _symbolsGalleryRepository.InsertSymbolGalleryAsync(Id, await _service.GetSymbolByIdAsync(Id));
    }

    public async Task UpdateStatusSymbolGalleryAsync(string Id)
    {
        await _symbolsGalleryRepository.UpdateStatusSymbolGalleryAsync(Id);
    }

    public async Task<Symbols> SumPowerSymbolsGalleryAsync()
    {
        return await _symbolsGalleryRepository.SumPowerSymbolsGalleryAsync();
    }

    public async Task UpdateStarSymbolGalleryAsync(string Id, double star)
    {
        await _symbolsGalleryRepository.UpdateStarSymbolGalleryAsync(Id, star);
    }

    public async Task UpdateSymbolGalleryPowerAsync(string Id)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        await _symbolsGalleryRepository.UpdateSymbolGalleryPowerAsync(Id, await _service.GetSymbolByIdAsync(Id));
    }
}
