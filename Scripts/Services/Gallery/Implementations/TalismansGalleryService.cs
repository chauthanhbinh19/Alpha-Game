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

    public async Task<List<Talismans>> GetTalismansCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = await _talismansGalleryRepository.GetTalismansCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTalismansCountAsync(string search, string type, string rare)
    {
        return await _talismansGalleryRepository.GetTalismansCountAsync(search, type, rare);
    }

    public async Task InsertTalismanGalleryAsync(string userId, string Id)
    {
        ITalismansRepository _repository = new TalismansRepository();
        TalismansService _service = new TalismansService(_repository);
        await _talismansGalleryRepository.InsertTalismanGalleryAsync(userId, Id, await _service.GetTalismanByIdAsync(Id));
    }

    public async Task UpdateStatusTalismanGalleryAsync(string userId, string Id)
    {
        await _talismansGalleryRepository.UpdateStatusTalismanGalleryAsync(userId, Id);
    }

    public async Task<Talismans> SumPowerTalismansGalleryAsync(string userId)
    {
        return await _talismansGalleryRepository.SumPowerTalismansGalleryAsync(userId);
    }

    public async Task UpdateStarTalismanGalleryAsync(string userId, string Id, double star)
    {
        await _talismansGalleryRepository.UpdateStarTalismanGalleryAsync(userId, Id, star);
    }

    public async Task UpdateTalismanGalleryPowerAsync(string userId, string Id)
    {
        ITalismansRepository _repository = new TalismansRepository();
        TalismansService _service = new TalismansService(_repository);
        await _talismansGalleryRepository.UpdateTalismanGalleryPowerAsync(userId, Id, await _service.GetTalismanByIdAsync(Id));
    }
}
