using System.Collections.Generic;
using System.Threading.Tasks;

public class FashionsGalleryService : IFashionsGalleryService
{
    private readonly IFashionsGalleryRepository _FashionGalleryRepository;

    public FashionsGalleryService(IFashionsGalleryRepository FashionGalleryRepository)
    {
        _FashionGalleryRepository = FashionGalleryRepository;
    }

    public static FashionsGalleryService Create()
    {
        return new FashionsGalleryService(new FashionsGalleryRepository());
    }

    public async Task<List<Fashions>> GetFashionsCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Fashions> list = await _FashionGalleryRepository.GetFashionsCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFashionsCountAsync(string type, string rare)
    {
        return await _FashionGalleryRepository.GetFashionsCountAsync(type, rare);
    }

    public async Task InsertFashionGalleryAsync(string Id)
    {
        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        await _FashionGalleryRepository.InsertFashionGalleryAsync(Id, await _service.GetFashionByIdAsync(Id));
    }

    public async Task UpdateStatusFashionGalleryAsync(string Id)
    {
        await _FashionGalleryRepository.UpdateStatusFashionGalleryAsync(Id);
    }

    public async Task<Fashions> SumPowerFashionsGalleryAsync()
    {
        return await _FashionGalleryRepository.SumPowerFashionsGalleryAsync();
    }

    public async Task UpdateStarFashionGalleryAsync(string Id, double star)
    {
        await _FashionGalleryRepository.UpdateStarFashionGalleryAsync(Id, star);
    }

    public async Task UpdateFashionGalleryPowerAsync(string Id)
    {
        IFashionsRepository _repository = new FashionsRepository();
        FashionsService _service = new FashionsService(_repository);
        await _FashionGalleryRepository.UpdateFashionGalleryPowerAsync(Id, await _service.GetFashionByIdAsync(Id));
    }
}
