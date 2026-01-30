using System.Collections.Generic;
using System.Threading.Tasks;

public class RelicsGalleryService : IRelicsGalleryService
{
    private readonly IRelicsGalleryRepository _relicsGalleryRepository;

    public RelicsGalleryService(IRelicsGalleryRepository relicsGalleryRepository)
    {
        _relicsGalleryRepository = relicsGalleryRepository;
    }

    public static RelicsGalleryService Create()
    {
        return new RelicsGalleryService(new RelicsGalleryRepository());
    }

    public async Task<List<Relics>> GetRelicsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _relicsGalleryRepository.GetRelicsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRelicsCountAsync(string search, string type, string rare)
    {
        return await _relicsGalleryRepository.GetRelicsCountAsync(search, type, rare);
    }

    public async Task InsertRelicGalleryAsync(string Id)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        await _relicsGalleryRepository.InsertRelicGalleryAsync(Id, await _service.GetRelicByIdAsync(Id));
    }

    public async Task UpdateStatusRelicGalleryAsync(string Id)
    {
        await _relicsGalleryRepository.UpdateStatusRelicGalleryAsync(Id);
    }

    public async Task<Relics> SumPowerRelicsGalleryAsync()
    {
        return await _relicsGalleryRepository.SumPowerRelicsGalleryAsync();
    }

    public async Task UpdateStarRelicGalleryAsync(string Id, double star)
    {
        await _relicsGalleryRepository.UpdateStarRelicGalleryAsync(Id, star);
    }

    public async Task UpdateRelicGalleryPowerAsync(string Id)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        await _relicsGalleryRepository.UpdateRelicGalleryPowerAsync(Id, await _service.GetRelicByIdAsync(Id));
    }
}
