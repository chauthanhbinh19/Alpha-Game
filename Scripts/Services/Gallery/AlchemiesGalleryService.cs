using System.Collections.Generic;
using System.Threading.Tasks;

public class AlchemiesGalleryService : IAlchemiesGalleryService
{
    private IAlchemiesGalleryRepository _alchemyGalleryRepository;

    public AlchemiesGalleryService(IAlchemiesGalleryRepository alchemyGalleryRepository)
    {
        _alchemyGalleryRepository = alchemyGalleryRepository;
    }

    public static AlchemiesGalleryService Create()
    {
        return new AlchemiesGalleryService(new AlchemiesGalleryRepository());
    }

    public async Task<List<Alchemies>> GetAlchemyCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _alchemyGalleryRepository.GetAlchemyCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAlchemyCountAsync(string search, string type, string rare)
    {
        return await _alchemyGalleryRepository.GetAlchemyCountAsync(search, type, rare);
    }

    public async Task InsertAlchemyGalleryAsync(string Id)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemyGalleryRepository.InsertAlchemyGalleryAsync(Id, await _service.GetAlchemyByIdAsync(Id));
    }

    public async Task UpdateStatusAlchemyGalleryAsync(string Id)
    {
        await _alchemyGalleryRepository.UpdateStatusAlchemyGalleryAsync(Id);
    }

    public async Task<Alchemies> SumPowerAlchemyGalleryAsync()
    {
        return await _alchemyGalleryRepository.SumPowerAlchemyGalleryAsync();
    }

    public async Task UpdateStarAlchemyGalleryAsync(string Id, double star)
    {
        await _alchemyGalleryRepository.UpdateStarAlchemyGalleryAsync(Id, star);
    }

    public async Task UpdateAlchemyGalleryPowerAsync(string Id)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        await _alchemyGalleryRepository.UpdateAlchemyGalleryPowerAsync(Id, await _service.GetAlchemyByIdAsync(Id));
    }
}
