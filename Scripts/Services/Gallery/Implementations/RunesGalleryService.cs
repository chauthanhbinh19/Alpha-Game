using System.Collections.Generic;
using System.Threading.Tasks;

public class RunesGalleryService : IRunesGalleryService
{
    private static RunesGalleryService _instance;
    private readonly IRunesGalleryRepository _runesGalleryRepository;

    public RunesGalleryService(IRunesGalleryRepository runesGalleryRepository)
    {
        _runesGalleryRepository = runesGalleryRepository;
    }

    public static RunesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new RunesGalleryService(new RunesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Runes>> GetRunesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Runes> list = await _runesGalleryRepository.GetRunesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetRunesCountAsync(string search, string rare)
    {
        return await _runesGalleryRepository.GetRunesCountAsync(search, rare);
    }

    public async Task InsertRuneGalleryAsync(string userId, string Id)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        await _runesGalleryRepository.InsertRuneGalleryAsync(userId, Id, await _service.GetRuneByIdAsync(Id));
    }

    public async Task UpdateStatusRuneGalleryAsync(string userId, string Id)
    {
        await _runesGalleryRepository.UpdateStatusRuneGalleryAsync(userId, Id);
    }

    public async Task<Runes> SumPowerRunesGalleryAsync(string userId)
    {
        return await _runesGalleryRepository.SumPowerRunesGalleryAsync(userId);
    }

    public async Task UpdateStarRuneGalleryAsync(string userId, string Id, double star)
    {
        await _runesGalleryRepository.UpdateStarRuneGalleryAsync(userId, Id, star);
    }

    public async Task UpdateRuneGalleryPowerAsync(string userId, string Id)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        await _runesGalleryRepository.UpdateRuneGalleryPowerAsync(userId, Id, await _service.GetRuneByIdAsync(Id));
    }
}
