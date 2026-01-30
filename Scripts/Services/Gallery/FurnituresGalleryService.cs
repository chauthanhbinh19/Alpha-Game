using System.Collections.Generic;
using System.Threading.Tasks;

public class FurnituresGalleryService : IFurnituresGalleryService
{
    private readonly IFurnituresGalleryRepository _FurnitureGalleryRepository;

    public FurnituresGalleryService(IFurnituresGalleryRepository FurnitureGalleryRepository)
    {
        _FurnitureGalleryRepository = FurnitureGalleryRepository;
    }

    public static FurnituresGalleryService Create()
    {
        return new FurnituresGalleryService(new FurnituresGalleryRepository());
    }

    public async Task<List<Furnitures>> GetFurnituresCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _FurnitureGalleryRepository.GetFurnituresCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string search, string type, string rare)
    {
        return await _FurnitureGalleryRepository.GetFurnituresCountAsync(search, type, rare);
    }

    public async Task InsertFurnitureGalleryAsync(string Id)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _FurnitureGalleryRepository.InsertFurnitureGalleryAsync(Id, await _service.GetFurnitureByIdAsync(Id));
    }

    public async Task UpdateStatusFurnitureGalleryAsync(string Id)
    {
        await _FurnitureGalleryRepository.UpdateStatusFurnitureGalleryAsync(Id);
    }

    public async Task<Furnitures> SumPowerFurnituresGalleryAsync()
    {
        return await _FurnitureGalleryRepository.SumPowerFurnituresGalleryAsync();
    }

    public async Task UpdateStarFurnitureGalleryAsync(string Id, double star)
    {
        await _FurnitureGalleryRepository.UpdateStarFurnitureGalleryAsync(Id, star);
    }

    public async Task UpdateFurnitureGalleryPowerAsync(string Id)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _FurnitureGalleryRepository.UpdateFurnitureGalleryPowerAsync(Id, await _service.GetFurnitureByIdAsync(Id));
    }
}
