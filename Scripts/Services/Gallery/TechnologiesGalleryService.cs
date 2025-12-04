using System.Collections.Generic;
using System.Threading.Tasks;

public class TechnologiesGalleryService : ITechnologiesGalleryService
{
    private readonly ITechnologiesGalleryRepository _TechnologiesGalleryRepository;

    public TechnologiesGalleryService(ITechnologiesGalleryRepository TechnologiesGalleryRepository)
    {
        _TechnologiesGalleryRepository = TechnologiesGalleryRepository;
    }

    public static TechnologiesGalleryService Create()
    {
        return new TechnologiesGalleryService(new TechnologiesGalleryRepository());
    }

    public async Task<List<Technologies>> GetTechnologiesCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _TechnologiesGalleryRepository.GetTechnologiesCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string rare)
    {
        return await _TechnologiesGalleryRepository.GetTechnologiesCountAsync(rare);
    }

    public async Task InsertTechnologyGalleryAsync(string Id)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        await _TechnologiesGalleryRepository.InsertTechnologyGalleryAsync(Id, await _service.GetTechnologyByIdAsync(Id));
    }

    public async Task UpdateStatusTechnologyGalleryAsync(string Id)
    {
        await _TechnologiesGalleryRepository.UpdateStatusTechnologyGalleryAsync(Id);
    }

    public async Task<Technologies> SumPowerTechnologiesGalleryAsync()
    {
        return await _TechnologiesGalleryRepository.SumPowerTechnologiesGalleryAsync();
    }

    public async Task UpdateStarTechnologyGalleryAsync(string Id, double star)
    {
        await _TechnologiesGalleryRepository.UpdateStarTechnologyGalleryAsync(Id, star);
    }

    public async Task UpdateTechnologyGalleryPowerAsync(string Id)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        await _TechnologiesGalleryRepository.UpdateTechnologyGalleryPowerAsync(Id, await _service.GetTechnologyByIdAsync(Id));
    }
}
