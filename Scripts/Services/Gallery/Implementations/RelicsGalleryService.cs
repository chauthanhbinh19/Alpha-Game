using System.Collections.Generic;
using System.Threading.Tasks;

public class RelicsGalleryService : IRelicsGalleryService
{
    private static RelicsGalleryService _instance;
    private readonly IRelicsGalleryRepository _relicsGalleryRepository;

    public RelicsGalleryService(IRelicsGalleryRepository relicsGalleryRepository)
    {
        _relicsGalleryRepository = relicsGalleryRepository;
    }

    public static RelicsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new RelicsGalleryService(new RelicsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Relics>> GetRelicsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _relicsGalleryRepository.GetRelicsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRelicsCountAsync(string search, string type, string rare)
    {
        return await _relicsGalleryRepository.GetRelicsCountAsync(search, type, rare);
    }

    public async Task InsertRelicGalleryAsync(string userId, string Id)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        await _relicsGalleryRepository.InsertRelicGalleryAsync(userId, Id, await _service.GetRelicByIdAsync(Id));
    }

    public async Task UpdateStatusRelicGalleryAsync(string userId, string Id)
    {
        await _relicsGalleryRepository.UpdateStatusRelicGalleryAsync(userId, Id);
    }

    public async Task<Relics> SumPowerRelicsGalleryAsync(string userId)
    {
        return await _relicsGalleryRepository.SumPowerRelicsGalleryAsync(userId);
    }

    public async Task UpdateStarRelicGalleryAsync(string userId, string Id, double star)
    {
        await _relicsGalleryRepository.UpdateStarRelicGalleryAsync(userId, Id, star);
    }

    public async Task UpdateRelicGalleryPowerAsync(string userId, string Id)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        await _relicsGalleryRepository.UpdateRelicGalleryPowerAsync(userId, Id, await _service.GetRelicByIdAsync(Id));
    }
}
