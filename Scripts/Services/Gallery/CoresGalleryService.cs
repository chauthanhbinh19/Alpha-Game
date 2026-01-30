using System.Collections.Generic;
using System.Threading.Tasks;

public class CoresGalleryService : ICoresGalleryService
{
    private readonly ICoresGalleryRepository _CoresGalleryRepository;

    public CoresGalleryService(ICoresGalleryRepository CoresGalleryRepository)
    {
        _CoresGalleryRepository = CoresGalleryRepository;
    }

    public static CoresGalleryService Create()
    {
        return new CoresGalleryService(new CoresGalleryRepository());
    }

    public async Task<List<Cores>> GetCoresCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _CoresGalleryRepository.GetCoresCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCoresCountAsync(string search, string rare)
    {
        return await _CoresGalleryRepository.GetCoresCountAsync(search, rare);
    }

    public async Task InsertCoreGalleryAsync(string Id)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        await _CoresGalleryRepository.InsertCoreGalleryAsync(Id, await _service.GetCoreByIdAsync(Id));
    }

    public async Task UpdateStatusCoreGalleryAsync(string Id)
    {
        await _CoresGalleryRepository.UpdateStatusCoreGalleryAsync(Id);
    }

    public async Task<Cores> SumPowerCoresGalleryAsync()
    {
        return await _CoresGalleryRepository.SumPowerCoresGalleryAsync();
    }

    public async Task UpdateStarCoreGalleryAsync(string Id, double star)
    {
        await _CoresGalleryRepository.UpdateStarCoreGalleryAsync(Id, star);
    }

    public async Task UpdateCoreGalleryPowerAsync(string Id)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        await _CoresGalleryRepository.UpdateCoreGalleryPowerAsync(Id, await _service.GetCoreByIdAsync(Id));
    }
}
