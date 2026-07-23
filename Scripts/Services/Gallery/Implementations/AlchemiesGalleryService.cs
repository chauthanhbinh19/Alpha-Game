using System.Collections.Generic;
using System.Threading.Tasks;

public class AlchemiesGalleryService : IAlchemiesGalleryService
{
    private static AlchemiesGalleryService _instance;
    private readonly IAlchemiesGalleryRepository _alchemiesGalleryRepository;

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

    public async Task<List<Alchemies>> GetAlchemiesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _alchemiesGalleryRepository.GetAlchemiesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemyCountAsync(string search, string type, string rare)
    {
        return await _alchemiesGalleryRepository.GetAlchemyCountAsync(search, type, rare);
    }

    public async Task InsertAlchemyGalleryAsync(string userId, string Id)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemiesGalleryRepository.InsertAlchemyGalleryAsync(userId, Id, await _service.GetAlchemyByIdAsync(Id));
    }

    public async Task UpdateStatusAlchemyGalleryAsync(string userId, string Id)
    {
        await _alchemiesGalleryRepository.UpdateStatusAlchemyGalleryAsync(userId, Id);
    }

    public async Task<Alchemies> SumPowerAlchemyGalleryAsync(string userId)
    {
        return await _alchemiesGalleryRepository.SumPowerAlchemyGalleryAsync(userId);
    }

    public async Task UpdateStarAlchemyGalleryAsync(string userId, string Id, double star)
    {
        await _alchemiesGalleryRepository.UpdateStarAlchemyGalleryAsync(userId, Id, star);
    }

    public async Task UpdateAlchemyGalleryPowerAsync(string userId, string Id)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemiesGalleryRepository.UpdateAlchemyGalleryPowerAsync(userId, Id, await _service.GetAlchemyByIdAsync(Id));
    }
}
