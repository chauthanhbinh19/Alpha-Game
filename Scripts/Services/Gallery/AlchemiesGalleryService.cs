using System.Collections.Generic;
using System.Threading.Tasks;

public class AlchemiesGalleryService : IAlchemiesGalleryService
{
    private static AlchemiesGalleryService _instance;
    private IAlchemiesGalleryRepository _alchemiesGalleryRepository;

    public AlchemiesGalleryService(IAlchemiesGalleryRepository alchemiesGalleryRepository)
    {
        _alchemiesGalleryRepository = alchemiesGalleryRepository;
    }

    public static AlchemiesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new AlchemiesGalleryService(new AlchemiesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Alchemies>> GetAlchemyCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _alchemiesGalleryRepository.GetAlchemyCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemyCountAsync(string search, string type, string rare)
    {
        return await _alchemiesGalleryRepository.GetAlchemyCountAsync(search, type, rare);
    }

    public async Task InsertAlchemyGalleryAsync(string Id)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemiesGalleryRepository.InsertAlchemyGalleryAsync(Id, await _service.GetAlchemyByIdAsync(Id));
    }

    public async Task UpdateStatusAlchemyGalleryAsync(string Id)
    {
        await _alchemiesGalleryRepository.UpdateStatusAlchemyGalleryAsync(Id);
    }

    public async Task<Alchemies> SumPowerAlchemyGalleryAsync()
    {
        return await _alchemiesGalleryRepository.SumPowerAlchemyGalleryAsync();
    }

    public async Task UpdateStarAlchemyGalleryAsync(string Id, double star)
    {
        await _alchemiesGalleryRepository.UpdateStarAlchemyGalleryAsync(Id, star);
    }

    public async Task UpdateAlchemyGalleryPowerAsync(string Id)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemiesGalleryRepository.UpdateAlchemyGalleryPowerAsync(Id, await _service.GetAlchemyByIdAsync(Id));
    }
}
