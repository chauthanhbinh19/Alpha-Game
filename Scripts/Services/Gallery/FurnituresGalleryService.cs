using System.Collections.Generic;
using System.Threading.Tasks;

public class FurnitureGalleryService : IFurnituresGalleryService
{
    private readonly IFurnitureGalleryRepository _FurnitureGalleryRepository;

    public FurnitureGalleryService(IFurnitureGalleryRepository FurnitureGalleryRepository)
    {
        _FurnitureGalleryRepository = FurnitureGalleryRepository;
    }

    public static FurnitureGalleryService Create()
    {
        return new FurnitureGalleryService(new FurnitureGalleryRepository());
    }

    public async Task<List<Furnitures>> GetFurnituresCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _FurnitureGalleryRepository.GetFurnituresCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string type, string rare)
    {
        return await _FurnitureGalleryRepository.GetFurnituresCountAsync(type, rare);
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
