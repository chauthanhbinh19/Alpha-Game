using System.Collections.Generic;
using System.Threading.Tasks;

public class RunesGalleryService : IRunesGalleryService
{
    private readonly IRunesGalleryRepository _RunesGalleryRepository;

    public RunesGalleryService(IRunesGalleryRepository RunesGalleryRepository)
    {
        _RunesGalleryRepository = RunesGalleryRepository;
    }

    public static RunesGalleryService Create()
    {
        return new RunesGalleryService(new RunesGalleryRepository());
    }

    public async Task<List<Runes>> GetRunesCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Runes> list = await _RunesGalleryRepository.GetRunesCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesCountAsync(string rare)
    {
        return await _RunesGalleryRepository.GetRunesCountAsync(rare);
    }

    public async Task InsertRuneGalleryAsync(string Id)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        await _RunesGalleryRepository.InsertRuneGalleryAsync(Id, await _service.GetRuneByIdAsync(Id));
    }

    public async Task UpdateStatusRuneGalleryAsync(string Id)
    {
        await _RunesGalleryRepository.UpdateStatusRuneGalleryAsync(Id);
    }

    public async Task<Runes> SumPowerRunesGalleryAsync()
    {
        return await _RunesGalleryRepository.SumPowerRunesGalleryAsync();
    }

    public async Task UpdateStarRuneGalleryAsync(string Id, double star)
    {
        await _RunesGalleryRepository.UpdateStarRuneGalleryAsync(Id, star);
    }

    public async Task UpdateRuneGalleryPowerAsync(string Id)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        await _RunesGalleryRepository.UpdateRuneGalleryPowerAsync(Id, await _service.GetRuneByIdAsync(Id));
    }
}
