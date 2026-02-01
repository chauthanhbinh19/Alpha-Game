using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class ForgesGalleryService : IForgesGalleryService
{
    private static ForgesGalleryService _instance;
    private IForgesGalleryRepository _forgesGalleryRepository;

    public ForgesGalleryService(IForgesGalleryRepository forgesGalleryRepository)
    {
        _forgesGalleryRepository = forgesGalleryRepository;
    }

    public static ForgesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new ForgesGalleryService(new ForgesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Forges>> GetForgesCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _forgesGalleryRepository.GetForgesCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesCountAsync(string search, string type, string rare)
    {
        return await _forgesGalleryRepository.GetForgesCountAsync(search, type, rare);
    }

    public async Task InsertForgeGalleryAsync(string Id)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        await _forgesGalleryRepository.InsertForgeGalleryAsync(Id, await _service.GetForgeByIdAsync(Id));
    }

    public async Task UpdateStatusForgeGalleryAsync(string Id)
    {
        await _forgesGalleryRepository.UpdateStatusForgeGalleryAsync(Id);
    }

    public async Task<Forges> SumPowerForgesGalleryAsync()
    {
        return await _forgesGalleryRepository.SumPowerForgesGalleryAsync();
    }

    public async Task UpdateStarForgeGalleryAsync(string Id, double star)
    {
        await _forgesGalleryRepository.UpdateStarForgeGalleryAsync(Id, star);
    }

    public async Task UpdateForgeGalleryPowerAsync(string Id)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        await _forgesGalleryRepository.UpdateForgeGalleryPowerAsync(Id, await _service.GetForgeByIdAsync(Id));
    }
}
