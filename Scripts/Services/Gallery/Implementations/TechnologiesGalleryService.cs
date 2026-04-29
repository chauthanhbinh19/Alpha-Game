using System.Collections.Generic;
using System.Threading.Tasks;

public class TechnologiesGalleryService : ITechnologiesGalleryService
{
    private static TechnologiesGalleryService _instance;
    private readonly ITechnologiesGalleryRepository _TechnologiesGalleryRepository;

    public TechnologiesGalleryService(ITechnologiesGalleryRepository TechnologiesGalleryRepository)
    {
        _TechnologiesGalleryRepository = TechnologiesGalleryRepository;
    }

    public static TechnologiesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new TechnologiesGalleryService(new TechnologiesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Technologies>> GetTechnologiesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Technologies> list = await _TechnologiesGalleryRepository.GetTechnologiesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string search, string rare)
    {
        return await _TechnologiesGalleryRepository.GetTechnologiesCountAsync(search, rare);
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
