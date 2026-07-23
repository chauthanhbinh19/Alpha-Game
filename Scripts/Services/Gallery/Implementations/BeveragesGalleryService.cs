using System.Collections.Generic;
using System.Threading.Tasks;

public class BeveragesGalleryService : IBeveragesGalleryService
{
    private static BeveragesGalleryService _instance;
    private readonly IBeveragesGalleryRepository _beveragesGalleryRepository;

    public BeveragesGalleryService(IBeveragesGalleryRepository beveragesGalleryRepository)
    {
        _beveragesGalleryRepository = beveragesGalleryRepository;
    }

    public static BeveragesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new BeveragesGalleryService(new BeveragesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Beverages>> GetBeveragesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Beverages> list = await _beveragesGalleryRepository.GetBeveragesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBeveragesCountAsync(string search, string rare)
    {
        return await _beveragesGalleryRepository.GetBeveragesCountAsync(search, rare);
    }

    public async Task InsertBeverageGalleryAsync(string userId, string Id)
    {
        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        await _beveragesGalleryRepository.InsertBeverageGalleryAsync(userId, Id, await _service.GetBeverageByIdAsync(Id));
    }

    public async Task UpdateStatusBeverageGalleryAsync(string userId, string Id)
    {
        await _beveragesGalleryRepository.UpdateStatusBeverageGalleryAsync(userId, Id);
    }

    public async Task<Beverages> SumPowerBeveragesGalleryAsync(string userId)
    {
        return await _beveragesGalleryRepository.SumPowerBeveragesGalleryAsync(userId);
    }

    public async Task UpdateStarBeverageGalleryAsync(string userId, string Id, double star)
    {
        await _beveragesGalleryRepository.UpdateStarBeverageGalleryAsync(userId, Id, star);
    }

    public async Task UpdateBeverageGalleryPowerAsync(string userId, string Id)
    {
        IBeveragesRepository _repository = new BeveragesRepository();
        BeveragesService _service = new BeveragesService(_repository);
        await _beveragesGalleryRepository.UpdateBeverageGalleryPowerAsync(userId, Id, await _service.GetBeverageByIdAsync(Id));
    }
}
