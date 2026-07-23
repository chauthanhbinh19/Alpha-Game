using System.Collections.Generic;
using System.Threading.Tasks;

public class MedalsGalleryService : IMedalsGalleryService
{
    private static MedalsGalleryService _instance;
    private readonly IMedalsGalleryRepository _medalsGalleryRepository;

    public MedalsGalleryService(IMedalsGalleryRepository medalsGalleryRepository)
    {
        _medalsGalleryRepository = medalsGalleryRepository;
    }

    public static MedalsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new MedalsGalleryService(new MedalsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Medals>> GetMedalsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _medalsGalleryRepository.GetMedalsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsCountAsync(string search, string rare)
    {
        return await _medalsGalleryRepository.GetMedalsCountAsync(search, rare);
    }

    public async Task InsertMedalGalleryAsync(string userId, string Id)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        await _medalsGalleryRepository.InsertMedalGalleryAsync(userId, Id, await _service.GetMedalByIdAsync(Id));
    }

    public async Task UpdateStatusMedalGalleryAsync(string userId, string Id)
    {
        await _medalsGalleryRepository.UpdateStatusMedalGalleryAsync(userId, Id);
    }

    public async Task<Medals> SumPowerMedalsGalleryAsync(string userId)
    {
        return await _medalsGalleryRepository.SumPowerMedalsGalleryAsync(userId);
    }

    public async Task UpdateStarMedalGalleryAsync(string userId, string Id, double star)
    {
        await _medalsGalleryRepository.UpdateStarMedalGalleryAsync(userId, Id, star);
    }

    public async Task UpdateMedalGalleryPowerAsync(string userId, string Id)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        await _medalsGalleryRepository.UpdateMedalGalleryPowerAsync(userId, Id, await _service.GetMedalByIdAsync(Id));
    }
}
