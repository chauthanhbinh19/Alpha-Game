using System.Collections.Generic;
using System.Threading.Tasks;

public class PuppetsGalleryService : IPuppetsGalleryService
{
    private static PuppetsGalleryService _instance;
    private readonly IPuppetsGalleryRepository _puppetsGalleryRepository;

    public PuppetsGalleryService(IPuppetsGalleryRepository puppetsGalleryRepository)
    {
        _puppetsGalleryRepository = puppetsGalleryRepository;
    }

    public static PuppetsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new PuppetsGalleryService(new PuppetsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Puppets>> GetPuppetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _puppetsGalleryRepository.GetPuppetsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsCountAsync(string search, string type, string rare)
    {
        return await _puppetsGalleryRepository.GetPuppetsCountAsync(search, type, rare);
    }

    public async Task InsertPuppetGalleryAsync(string userId, string Id)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        await _puppetsGalleryRepository.InsertPuppetGalleryAsync(userId, Id, await _service.GetPuppetByIdAsync(Id));
    }

    public async Task UpdateStatusPuppetGalleryAsync(string userId, string Id)
    {
        await _puppetsGalleryRepository.UpdateStatusPuppetGalleryAsync(userId, Id);
    }

    public async Task<Puppets> SumPowerPuppetsGalleryAsync(string userId)
    {
        return await _puppetsGalleryRepository.SumPowerPuppetsGalleryAsync(userId);
    }

    public async Task UpdateStarPuppetGalleryAsync(string userId, string Id, double star)
    {
        await _puppetsGalleryRepository.UpdateStarPuppetGalleryAsync(userId, Id, star);
    }

    public async Task UpdatePuppetGalleryPowerAsync(string userId, string Id)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        await _puppetsGalleryRepository.UpdatePuppetGalleryPowerAsync(userId, Id, await _service.GetPuppetByIdAsync(Id));
    }
}
