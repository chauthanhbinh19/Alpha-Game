using System.Collections.Generic;
using System.Threading.Tasks;

public class PuppetsGalleryService : IPuppetsGalleryService
{
    private readonly IPuppetsGalleryRepository _puppetGalleryRepository;

    public PuppetsGalleryService(IPuppetsGalleryRepository puppetGalleryRepository)
    {
        _puppetGalleryRepository = puppetGalleryRepository;
    }

    public static PuppetsGalleryService Create()
    {
        return new PuppetsGalleryService(new PuppetsGalleryRepository());
    }

    public async Task<List<Puppets>> GetPuppetsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _puppetGalleryRepository.GetPuppetsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPuppetsCountAsync(string search, string type, string rare)
    {
        return await _puppetGalleryRepository.GetPuppetsCountAsync(search, type, rare);
    }

    public async Task InsertPuppetGalleryAsync(string Id)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        await _puppetGalleryRepository.InsertPuppetGalleryAsync(Id, await _service.GetPuppetByIdAsync(Id));
    }

    public async Task UpdateStatusPuppetGalleryAsync(string Id)
    {
        await _puppetGalleryRepository.UpdateStatusPuppetGalleryAsync(Id);
    }

    public async Task<Puppets> SumPowerPuppetsGalleryAsync()
    {
        return await _puppetGalleryRepository.SumPowerPuppetsGalleryAsync();
    }

    public async Task UpdateStarPuppetGalleryAsync(string Id, double star)
    {
        await _puppetGalleryRepository.UpdateStarPuppetGalleryAsync(Id, star);
    }

    public async Task UpdatePuppetGalleryPowerAsync(string Id)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        await _puppetGalleryRepository.UpdatePuppetGalleryPowerAsync(Id, await _service.GetPuppetByIdAsync(Id));
    }
}
