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

    public async Task<List<Puppets>> GetPuppetsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _puppetsGalleryRepository.GetPuppetsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsCountAsync(string search, string type, string rare)
    {
        return await _puppetsGalleryRepository.GetPuppetsCountAsync(search, type, rare);
    }

    public async Task InsertPuppetGalleryAsync(string Id)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        await _puppetsGalleryRepository.InsertPuppetGalleryAsync(Id, await _service.GetPuppetByIdAsync(Id));
    }

    public async Task UpdateStatusPuppetGalleryAsync(string Id)
    {
        await _puppetsGalleryRepository.UpdateStatusPuppetGalleryAsync(Id);
    }

    public async Task<Puppets> SumPowerPuppetsGalleryAsync()
    {
        return await _puppetsGalleryRepository.SumPowerPuppetsGalleryAsync();
    }

    public async Task UpdateStarPuppetGalleryAsync(string Id, double star)
    {
        await _puppetsGalleryRepository.UpdateStarPuppetGalleryAsync(Id, star);
    }

    public async Task UpdatePuppetGalleryPowerAsync(string Id)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        await _puppetsGalleryRepository.UpdatePuppetGalleryPowerAsync(Id, await _service.GetPuppetByIdAsync(Id));
    }
}
