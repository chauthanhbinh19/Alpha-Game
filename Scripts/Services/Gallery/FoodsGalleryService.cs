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

    public async Task<List<Foods>> GetFoodsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Foods> list = await _foodsGalleryRepository.GetFoodsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsCountAsync(string search, string rare)
    {
        return await _foodsGalleryRepository.GetFoodsCountAsync(search, rare);
    }

    public async Task InsertFoodGalleryAsync(string Id)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _foodsGalleryRepository.InsertFoodGalleryAsync(Id, await _service.GetFoodByIdAsync(Id));
    }

    public async Task UpdateStatusFoodGalleryAsync(string Id)
    {
        await _foodsGalleryRepository.UpdateStatusFoodGalleryAsync(Id);
    }

    public async Task<Foods> SumPowerFoodsGalleryAsync()
    {
        return await _foodsGalleryRepository.SumPowerFoodsGalleryAsync();
    }

    public async Task UpdateStarFoodGalleryAsync(string Id, double star)
    {
        await _foodsGalleryRepository.UpdateStarFoodGalleryAsync(Id, star);
    }

    public async Task UpdateFoodGalleryPowerAsync(string Id)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _foodsGalleryRepository.UpdateFoodGalleryPowerAsync(Id, await _service.GetFoodByIdAsync(Id));
    }
}
