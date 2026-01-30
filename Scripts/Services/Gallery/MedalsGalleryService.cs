using System.Collections.Generic;
using System.Threading.Tasks;

public class MedalsGalleryService : IMedalsGalleryService
{
    private IMedalsGalleryRepository _medalsGalleryRepository;

    public MedalsGalleryService(IMedalsGalleryRepository medalsGalleryRepository)
    {
        _medalsGalleryRepository = medalsGalleryRepository;
    }

    public static MedalsGalleryService Create()
    {
        return new MedalsGalleryService(new MedalsGalleryRepository());
    }

    public async Task<List<Medals>> GetMedalsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _medalsGalleryRepository.GetMedalsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetMedalsCountAsync(string search, string rare)
    {
        return await _medalsGalleryRepository.GetMedalsCountAsync(search, rare);
    }

    public async Task InsertMedalGalleryAsync(string Id)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        await _medalsGalleryRepository.InsertMedalGalleryAsync(Id, await _service.GetMedalByIdAsync(Id));
    }

    public async Task UpdateStatusMedalGalleryAsync(string Id)
    {
        await _medalsGalleryRepository.UpdateStatusMedalGalleryAsync(Id);
    }

    public async Task<Medals> SumPowerMedalsGalleryAsync()
    {
        return await _medalsGalleryRepository.SumPowerMedalsGalleryAsync();
    }

    public async Task UpdateStarMedalGalleryAsync(string Id, double star)
    {
        await _medalsGalleryRepository.UpdateStarMedalGalleryAsync(Id, star);
    }

    public async Task UpdateMedalGalleryPowerAsync(string Id)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        await _medalsGalleryRepository.UpdateMedalGalleryPowerAsync(Id, await _service.GetMedalByIdAsync(Id));
    }
}
