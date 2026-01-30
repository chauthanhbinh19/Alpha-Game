using System.Collections.Generic;
using System.Threading.Tasks;

public class BeveragesGalleryService : IBeveragesGalleryService
{
    private readonly IBeveragesGalleryRepository _BeveragesGalleryRepository;

    public BeveragesGalleryService(IBeveragesGalleryRepository BeveragesGalleryRepository)
    {
        _BeveragesGalleryRepository = BeveragesGalleryRepository;
    }

    public static BeveragesGalleryService Create()
    {
        return new BeveragesGalleryService(new BeveragesGalleryRepository());
    }

    public async Task<List<Beverages>> GetBeveragesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _BeveragesGalleryRepository.GetBeveragesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesCountAsync(string search, string rare)
    {
        return await _BeveragesGalleryRepository.GetBeveragesCountAsync(search, rare);
    }

    public async Task InsertBeverageGalleryAsync(string Id)
    {
        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        await _BeveragesGalleryRepository.InsertBeverageGalleryAsync(Id, await _service.GetBeverageByIdAsync(Id));
    }

    public async Task UpdateStatusBeverageGalleryAsync(string Id)
    {
        await _BeveragesGalleryRepository.UpdateStatusBeverageGalleryAsync(Id);
    }

    public async Task<Beverages> SumPowerBeveragesGalleryAsync()
    {
        return await _BeveragesGalleryRepository.SumPowerBeveragesGalleryAsync();
    }

    public async Task UpdateStarBeverageGalleryAsync(string Id, double star)
    {
        await _BeveragesGalleryRepository.UpdateStarBeverageGalleryAsync(Id, star);
    }

    public async Task UpdateBeverageGalleryPowerAsync(string Id)
    {
        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        await _BeveragesGalleryRepository.UpdateBeverageGalleryPowerAsync(Id, await _service.GetBeverageByIdAsync(Id));
    }
}
