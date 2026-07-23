using System.Collections.Generic;
using System.Threading.Tasks;

public class FoodsGalleryService : IFoodsGalleryService
{
    private static FoodsGalleryService _instance;
    private readonly IFoodsGalleryRepository _foodsGalleryRepository;

    public FoodsGalleryService(IFoodsGalleryRepository foodsGalleryRepository)
    {
        _foodsGalleryRepository = foodsGalleryRepository;
    }

    public static FoodsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new FoodsGalleryService(new FoodsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Foods>> GetFoodsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Foods> list = await _foodsGalleryRepository.GetFoodsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsCountAsync(string search, string rare)
    {
        return await _foodsGalleryRepository.GetFoodsCountAsync(search, rare);
    }

    public async Task InsertFoodGalleryAsync(string userId, string Id)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _foodsGalleryRepository.InsertFoodGalleryAsync(userId, Id, await _service.GetFoodByIdAsync(Id));
    }

    public async Task UpdateStatusFoodGalleryAsync(string userId, string Id)
    {
        await _foodsGalleryRepository.UpdateStatusFoodGalleryAsync(userId, Id);
    }

    public async Task<Foods> SumPowerFoodsGalleryAsync(string userId)
    {
        return await _foodsGalleryRepository.SumPowerFoodsGalleryAsync(userId);
    }

    public async Task UpdateStarFoodGalleryAsync(string userId, string Id, double star)
    {
        await _foodsGalleryRepository.UpdateStarFoodGalleryAsync(userId, Id, star);
    }

    public async Task UpdateFoodGalleryPowerAsync(string userId, string Id)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _foodsGalleryRepository.UpdateFoodGalleryPowerAsync(userId, Id, await _service.GetFoodByIdAsync(Id));
    }
}
