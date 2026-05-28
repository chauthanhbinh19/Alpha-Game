using System.Collections.Generic;
using System.Threading.Tasks;

public class TalismansGalleryService : ITalismansGalleryService
{
    private static TalismansGalleryService _instance;
    private readonly ITalismansGalleryRepository _talismansGalleryRepository;

    public TalismansGalleryService(ITalismansGalleryRepository talismansGalleryRepository)
    {
        _talismansGalleryRepository = talismansGalleryRepository;
    }

    public static TalismansGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new TalismansGalleryService(new TalismansGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Talismans>> GetTalismansCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _talismansGalleryRepository.GetTalismansCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansCountAsync(string search, string type, string rare)
    {
        return await _talismansGalleryRepository.GetTalismansCountAsync(search, type, rare);
    }

    public async Task InsertTalismanGalleryAsync(string Id)
    {
        ITalismansRepository _repository = new TalismansRepository();
        TalismansService _service = new TalismansService(_repository);
        await _talismansGalleryRepository.InsertTalismanGalleryAsync(Id, await _service.GetTalismanByIdAsync(Id));
    }

    public async Task UpdateStatusTalismanGalleryAsync(string Id)
    {
        await _talismansGalleryRepository.UpdateStatusTalismanGalleryAsync(Id);
    }

    public async Task<Talismans> SumPowerTalismansGalleryAsync()
    {
        return await _talismansGalleryRepository.SumPowerTalismansGalleryAsync();
    }

    public async Task UpdateStarTalismanGalleryAsync(string Id, double star)
    {
        await _talismansGalleryRepository.UpdateStarTalismanGalleryAsync(Id, star);
    }

    public async Task UpdateTalismanGalleryPowerAsync(string Id)
    {
        ITalismansRepository _repository = new TalismansRepository();
        TalismansService _service = new TalismansService(_repository);
        await _talismansGalleryRepository.UpdateTalismanGalleryPowerAsync(Id, await _service.GetTalismanByIdAsync(Id));
    }
}
