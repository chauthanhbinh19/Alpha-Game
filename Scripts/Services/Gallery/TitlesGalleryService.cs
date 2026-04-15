using System.Collections.Generic;
using System.Threading.Tasks;

public class TitlesGalleryService : ITitlesGalleryService
{
    private static TitlesGalleryService _instance;
    private readonly ITitlesGalleryRepository _titlesGalleryRepository;

    public TitlesGalleryService(ITitlesGalleryRepository titlesGalleryRepository)
    {
        _titlesGalleryRepository = titlesGalleryRepository;
    }

    public static TitlesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new TitlesGalleryService(new TitlesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Titles>> GetTitlesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _titlesGalleryRepository.GetTitlesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetTitlesCountAsync(string search, string rare)
    {
        return await _titlesGalleryRepository.GetTitlesCountAsync(search, rare);
    }

    public async Task InsertTitleGalleryAsync(string Id)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        await _titlesGalleryRepository.InsertTitleGalleryAsync(Id, await _service.GetTitleByIdAsync(Id));
    }

    public async Task UpdateStatusTitleGalleryAsync(string Id)
    {
        await _titlesGalleryRepository.UpdateStatusTitleGalleryAsync(Id);
    }

    public async Task<Titles> SumPowerTitlesGalleryAsync()
    {
        return await _titlesGalleryRepository.SumPowerTitlesGalleryAsync();
    }

    public async Task UpdateStarTitleGalleryAsync(string Id, double star)
    {
        await _titlesGalleryRepository.UpdateStarTitleGalleryAsync(Id, star);
    }

    public async Task UpdateTitleGalleryPowerAsync(string Id)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        await _titlesGalleryRepository.UpdateTitleGalleryPowerAsync(Id, await _service.GetTitleByIdAsync(Id));
    }
}
