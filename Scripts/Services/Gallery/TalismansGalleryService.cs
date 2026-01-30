using System.Collections.Generic;
using System.Threading.Tasks;

public class TalismansGalleryService : ITalismansGalleryService
{
    private readonly ITalismansGalleryRepository _talismanGalleryRepository;

    public TalismansGalleryService(ITalismansGalleryRepository talismanGalleryRepository)
    {
        _talismanGalleryRepository = talismanGalleryRepository;
    }

    public static TalismansGalleryService Create()
    {
        return new TalismansGalleryService(new TalismansGalleryRepository());
    }

    public async Task<List<Talismans>> GetTalismansCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _talismanGalleryRepository.GetTalismansCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansCountAsync(string search, string type, string rare)
    {
        return await _talismanGalleryRepository.GetTalismansCountAsync(search, type, rare);
    }

    public async Task InsertTalismanGalleryAsync(string Id)
    {
        ITalismansRepository _repository = new TalismansRepository();
        TalismansService _service = new TalismansService(_repository);
        await _talismanGalleryRepository.InsertTalismanGalleryAsync(Id, await _service.GetTalismanByIdAsync(Id));
    }

    public async Task UpdateStatusTalismanGalleryAsync(string Id)
    {
        await _talismanGalleryRepository.UpdateStatusTalismanGalleryAsync(Id);
    }

    public async Task<Talismans> SumPowerTalismansGalleryAsync()
    {
        return await _talismanGalleryRepository.SumPowerTalismansGalleryAsync();
    }

    public async Task UpdateStarTalismanGalleryAsync(string Id, double star)
    {
        await _talismanGalleryRepository.UpdateStarTalismanGalleryAsync(Id, star);
    }

    public async Task UpdateTalismanGalleryPowerAsync(string Id)
    {
        ITalismansRepository _repository = new TalismansRepository();
        TalismansService _service = new TalismansService(_repository);
        await _talismanGalleryRepository.UpdateTalismanGalleryPowerAsync(Id, await _service.GetTalismanByIdAsync(Id));
    }
}
