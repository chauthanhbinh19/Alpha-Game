using System.Collections.Generic;
using System.Threading.Tasks;

public class ArchitecturesGalleryService : IArchitecturesGalleryService
{
    private static ArchitecturesGalleryService _instance;
    private readonly IArchitecturesGalleryRepository _architecturesGalleryRepository;

    public ArchitecturesGalleryService(IArchitecturesGalleryRepository architecturesGalleryRepository)
    {
        _architecturesGalleryRepository = architecturesGalleryRepository;
    }

    public static ArchitecturesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new ArchitecturesGalleryService(new ArchitecturesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Architectures>> GetArchitecturesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Architectures> list = await _architecturesGalleryRepository.GetArchitecturesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArchitecturesCountAsync(string search, string rare)
    {
        return await _architecturesGalleryRepository.GetArchitecturesCountAsync(search, rare);
    }

    public async Task InsertArchitectureGalleryAsync(string Id)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        await _architecturesGalleryRepository.InsertArchitectureGalleryAsync(Id, await _service.GetArchitectureByIdAsync(Id));
    }

    public async Task UpdateStatusArchitectureGalleryAsync(string Id)
    {
        await _architecturesGalleryRepository.UpdateStatusArchitectureGalleryAsync(Id);
    }

    public async Task<Architectures> SumPowerArchitecturesGalleryAsync()
    {
        return await _architecturesGalleryRepository.SumPowerArchitecturesGalleryAsync();
    }

    public async Task UpdateStarArchitectureGalleryAsync(string Id, double star)
    {
        await _architecturesGalleryRepository.UpdateStarArchitectureGalleryAsync(Id, star);
    }

    public async Task UpdateArchitectureGalleryPowerAsync(string Id)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        await _architecturesGalleryRepository.UpdateArchitectureGalleryPowerAsync(Id, await _service.GetArchitectureByIdAsync(Id));
    }
}
