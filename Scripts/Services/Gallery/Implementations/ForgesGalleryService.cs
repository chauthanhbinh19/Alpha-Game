using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class ForgesGalleryService : IForgesGalleryService
{
    private static ForgesGalleryService _instance;
    private readonly IForgesGalleryRepository _forgesGalleryRepository;

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

    public async Task<List<Forges>> GetForgesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _forgesGalleryRepository.GetForgesCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetForgesCountAsync(string search, string type, string rare)
    {
        return await _forgesGalleryRepository.GetForgesCountAsync(search, type, rare);
    }

    public async Task InsertForgeGalleryAsync(string userId, string Id)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        await _forgesGalleryRepository.InsertForgeGalleryAsync(userId, Id, await _service.GetForgeByIdAsync(Id));
    }

    public async Task UpdateStatusForgeGalleryAsync(string userId, string Id)
    {
        await _forgesGalleryRepository.UpdateStatusForgeGalleryAsync(userId, Id);
    }

    public async Task<Forges> SumPowerForgesGalleryAsync(string userId)
    {
        return await _forgesGalleryRepository.SumPowerForgesGalleryAsync(userId);
    }

    public async Task UpdateStarForgeGalleryAsync(string userId, string Id, double star)
    {
        await _forgesGalleryRepository.UpdateStarForgeGalleryAsync(userId, Id, star);
    }

    public async Task UpdateForgeGalleryPowerAsync(string userId, string Id)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        await _forgesGalleryRepository.UpdateForgeGalleryPowerAsync(userId, Id, await _service.GetForgeByIdAsync(Id));
    }
}
