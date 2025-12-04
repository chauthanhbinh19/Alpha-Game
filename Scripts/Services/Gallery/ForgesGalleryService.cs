using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class ForgesGalleryService : IForgesGalleryService
{
    private IForgesGalleryRepository _forgeGalleryRepository;

    public ForgesGalleryService(IForgesGalleryRepository forgeGalleryRepository)
    {
        _forgeGalleryRepository = forgeGalleryRepository;
    }

    public static ForgesGalleryService Create()
    {
        return new ForgesGalleryService(new ForgesGalleryRepository());
    }

    public async Task<List<Forges>> GetForgesCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _forgeGalleryRepository.GetForgesCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesCountAsync(string type, string rare)
    {
        return await _forgeGalleryRepository.GetForgesCountAsync(type, rare);
    }

    public async Task InsertForgeGalleryAsync(string Id)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        await _forgeGalleryRepository.InsertForgeGalleryAsync(Id, await _service.GetForgeByIdAsync(Id));
    }

    public async Task UpdateStatusForgeGalleryAsync(string Id)
    {
        await _forgeGalleryRepository.UpdateStatusForgeGalleryAsync(Id);
    }

    public async Task<Forges> SumPowerForgesGalleryAsync()
    {
        return await _forgeGalleryRepository.SumPowerForgesGalleryAsync();
    }

    public async Task UpdateStarForgeGalleryAsync(string Id, double star)
    {
        await _forgeGalleryRepository.UpdateStarForgeGalleryAsync(Id, star);
    }

    public async Task UpdateForgeGalleryPowerAsync(string Id)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        await _forgeGalleryRepository.UpdateForgeGalleryPowerAsync(Id, await _service.GetForgeByIdAsync(Id));
    }
}
