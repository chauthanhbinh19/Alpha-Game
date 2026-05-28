using System.Collections.Generic;
using System.Threading.Tasks;

public class CoresGalleryService : ICoresGalleryService
{
    private static CoresGalleryService _instance;
    private readonly ICoresGalleryRepository _coresGalleryRepository;

    public CoresGalleryService(ICoresGalleryRepository coresGalleryRepository)
    {
        _coresGalleryRepository = coresGalleryRepository;
    }

    public static CoresGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CoresGalleryService(new CoresGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Cores>> GetCoresCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _coresGalleryRepository.GetCoresCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCoresCountAsync(string search, string rare)
    {
        return await _coresGalleryRepository.GetCoresCountAsync(search, rare);
    }

    public async Task InsertCoreGalleryAsync(string Id)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        await _coresGalleryRepository.InsertCoreGalleryAsync(Id, await _service.GetCoreByIdAsync(Id));
    }

    public async Task UpdateStatusCoreGalleryAsync(string Id)
    {
        await _coresGalleryRepository.UpdateStatusCoreGalleryAsync(Id);
    }

    public async Task<Cores> SumPowerCoresGalleryAsync()
    {
        return await _coresGalleryRepository.SumPowerCoresGalleryAsync();
    }

    public async Task UpdateStarCoreGalleryAsync(string Id, double star)
    {
        await _coresGalleryRepository.UpdateStarCoreGalleryAsync(Id, star);
    }

    public async Task UpdateCoreGalleryPowerAsync(string Id)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        await _coresGalleryRepository.UpdateCoreGalleryPowerAsync(Id, await _service.GetCoreByIdAsync(Id));
    }
}
