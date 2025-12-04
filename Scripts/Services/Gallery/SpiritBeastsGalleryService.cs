using System.Collections.Generic;
using System.Threading.Tasks;

public class SpiritBeastsGalleryService : ISpiritBeastsGalleryService
{
    private readonly ISpiritBeastsGalleryRepository _SpiritBeastGalleryRepository;

    public SpiritBeastsGalleryService(ISpiritBeastsGalleryRepository SpiritBeastGalleryRepository)
    {
        _SpiritBeastGalleryRepository = SpiritBeastGalleryRepository;
    }

    public static SpiritBeastsGalleryService Create()
    {
        return new SpiritBeastsGalleryService(new SpiritBeastsGalleryRepository());
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsCollectionAsync(int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _SpiritBeastGalleryRepository.GetSpiritBeastsCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSpiritBeastsCountAsync(string rare)
    {
        return await _SpiritBeastGalleryRepository.GetSpiritBeastsCountAsync(rare);
    }

    public async Task InsertSpiritBeastGalleryAsync(string Id)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _SpiritBeastGalleryRepository.InsertSpiritBeastGalleryAsync(Id, await _service.GetSpiritBeastByIdAsync(Id));
    }

    public async Task UpdateStatusSpiritBeastGalleryAsync(string Id)
    {
        await _SpiritBeastGalleryRepository.UpdateStatusSpiritBeastGalleryAsync(Id);
    }

    public async Task<SpiritBeasts> SumPowerSpiritBeastsGalleryAsync()
    {
        return await _SpiritBeastGalleryRepository.SumPowerSpiritBeastsGalleryAsync();
    }

    public async Task UpdateStarSpiritBeastGalleryAsync(string Id, double star)
    {
        await _SpiritBeastGalleryRepository.UpdateStarSpiritBeastGalleryAsync(Id, star);
    }

    public async Task UpdateSpiritBeastGalleryPowerAsync(string Id)
    {
        ISpiritBeastsRepository _repository = new SpiritBeastsRepository();
        SpiritBeastsService _service = new SpiritBeastsService(_repository);
        await _SpiritBeastGalleryRepository.UpdateSpiritBeastGalleryPowerAsync(Id, await _service.GetSpiritBeastByIdAsync(Id));
    }
}
