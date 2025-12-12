using System.Collections.Generic;
using System.Threading.Tasks;

public class FoodsGalleryService : IFoodsGalleryService
{
    private readonly IFoodsGalleryRepository _FoodsGalleryRepository;

    public FoodsGalleryService(IFoodsGalleryRepository FoodsGalleryRepository)
    {
        _FoodsGalleryRepository = FoodsGalleryRepository;
    }

    public static FoodsGalleryService Create()
    {
        return new FoodsGalleryService(new FoodsGalleryRepository());
    }

    public async Task<List<Foods>> GetFoodsCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Foods> list = await _FoodsGalleryRepository.GetFoodsCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFoodsCountAsync(string rare)
    {
        return await _FoodsGalleryRepository.GetFoodsCountAsync(rare);
    }

    public async Task InsertFoodGalleryAsync(string Id)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _FoodsGalleryRepository.InsertFoodGalleryAsync(Id, await _service.GetFoodByIdAsync(Id));
    }

    public async Task UpdateStatusFoodGalleryAsync(string Id)
    {
        await _FoodsGalleryRepository.UpdateStatusFoodGalleryAsync(Id);
    }

    public async Task<Foods> SumPowerFoodsGalleryAsync()
    {
        return await _FoodsGalleryRepository.SumPowerFoodsGalleryAsync();
    }

    public async Task UpdateStarFoodGalleryAsync(string Id, double star)
    {
        await _FoodsGalleryRepository.UpdateStarFoodGalleryAsync(Id, star);
    }

    public async Task UpdateFoodGalleryPowerAsync(string Id)
    {
        IFoodsRepository _repository = new FoodsRepository();
        FoodsService _service = new FoodsService(_repository);
        await _FoodsGalleryRepository.UpdateFoodGalleryPowerAsync(Id, await _service.GetFoodByIdAsync(Id));
    }
}
