using System.Collections.Generic;
using System.Threading.Tasks;

public class TechnologiesGalleryService : ITechnologiesGalleryService
{
    private static TechnologiesGalleryService _instance;
    private readonly ITechnologiesGalleryRepository _technologiesGalleryRepository;

    public TechnologiesGalleryService(ITechnologiesGalleryRepository technologiesGalleryRepository)
    {
        _technologiesGalleryRepository = technologiesGalleryRepository;
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
        List<Technologies> list = await _technologiesGalleryRepository.GetTechnologiesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTechnologiesCountAsync(string search, string rare)
    {
        return await _technologiesGalleryRepository.GetTechnologiesCountAsync(search, rare);
    }

    public async Task InsertTechnologyGalleryAsync(string Id)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        await _technologiesGalleryRepository.InsertTechnologyGalleryAsync(Id, await _service.GetTechnologyByIdAsync(Id));
    }

    public async Task UpdateStatusTechnologyGalleryAsync(string Id)
    {
        await _technologiesGalleryRepository.UpdateStatusTechnologyGalleryAsync(Id);
    }

    public async Task<Technologies> SumPowerTechnologiesGalleryAsync()
    {
        return await _technologiesGalleryRepository.SumPowerTechnologiesGalleryAsync();
    }

    public async Task UpdateStarTechnologyGalleryAsync(string Id, double star)
    {
        await _technologiesGalleryRepository.UpdateStarTechnologyGalleryAsync(Id, star);
    }

    public async Task UpdateTechnologyGalleryPowerAsync(string Id)
    {
        ITechnologiesRepository _repository = new TechnologiesRepository();
        TechnologiesService _service = new TechnologiesService(_repository);
        await _technologiesGalleryRepository.UpdateTechnologyGalleryPowerAsync(Id, await _service.GetTechnologyByIdAsync(Id));
    }
}
