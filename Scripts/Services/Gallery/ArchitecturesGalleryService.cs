using System.Collections.Generic;
using System.Threading.Tasks;

public class ArchitecturesGalleryService : IArchitecturesGalleryService
{
    private readonly IArchitecturesGalleryRepository _ArchitecturesGalleryRepository;

    public ArchitecturesGalleryService(IArchitecturesGalleryRepository ArchitecturesGalleryRepository)
    {
        _ArchitecturesGalleryRepository = ArchitecturesGalleryRepository;
    }

    public static ArchitecturesGalleryService Create()
    {
        return new ArchitecturesGalleryService(new ArchitecturesGalleryRepository());
    }

    public async Task<List<Architectures>> GetArchitecturesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Architectures> list = await _ArchitecturesGalleryRepository.GetArchitecturesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArchitecturesCountAsync(string search, string rare)
    {
        return await _ArchitecturesGalleryRepository.GetArchitecturesCountAsync(search, rare);
    }

    public async Task InsertArchitectureGalleryAsync(string Id)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        await _ArchitecturesGalleryRepository.InsertArchitectureGalleryAsync(Id, await _service.GetArchitectureByIdAsync(Id));
    }

    public async Task UpdateStatusArchitectureGalleryAsync(string Id)
    {
        await _ArchitecturesGalleryRepository.UpdateStatusArchitectureGalleryAsync(Id);
    }

    public async Task<Architectures> SumPowerArchitecturesGalleryAsync()
    {
        return await _ArchitecturesGalleryRepository.SumPowerArchitecturesGalleryAsync();
    }

    public async Task UpdateStarArchitectureGalleryAsync(string Id, double star)
    {
        await _ArchitecturesGalleryRepository.UpdateStarArchitectureGalleryAsync(Id, star);
    }

    public async Task UpdateArchitectureGalleryPowerAsync(string Id)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        await _ArchitecturesGalleryRepository.UpdateArchitectureGalleryPowerAsync(Id, await _service.GetArchitectureByIdAsync(Id));
    }
}
