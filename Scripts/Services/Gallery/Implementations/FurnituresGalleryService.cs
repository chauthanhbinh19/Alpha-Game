using System.Collections.Generic;
using System.Threading.Tasks;

public class FurnituresGalleryService : IFurnituresGalleryService
{
    private static FurnituresGalleryService _instance;
    private readonly IFurnituresGalleryRepository _furnituresGalleryRepository;

    public FurnituresGalleryService(IFurnituresGalleryRepository furnituresGalleryRepository)
    {
        _furnituresGalleryRepository = furnituresGalleryRepository;
    }

    public static FurnituresGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new FurnituresGalleryService(new FurnituresGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Furnitures>> GetFurnituresCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _furnituresGalleryRepository.GetFurnituresCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string search, string type, string rare)
    {
        return await _furnituresGalleryRepository.GetFurnituresCountAsync(search, type, rare);
    }

    public async Task InsertFurnitureGalleryAsync(string Id)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _furnituresGalleryRepository.InsertFurnitureGalleryAsync(Id, await _service.GetFurnitureByIdAsync(Id));
    }

    public async Task UpdateStatusFurnitureGalleryAsync(string Id)
    {
        await _furnituresGalleryRepository.UpdateStatusFurnitureGalleryAsync(Id);
    }

    public async Task<Furnitures> SumPowerFurnituresGalleryAsync()
    {
        return await _furnituresGalleryRepository.SumPowerFurnituresGalleryAsync();
    }

    public async Task UpdateStarFurnitureGalleryAsync(string Id, double star)
    {
        await _furnituresGalleryRepository.UpdateStarFurnitureGalleryAsync(Id, star);
    }

    public async Task UpdateFurnitureGalleryPowerAsync(string Id)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _furnituresGalleryRepository.UpdateFurnitureGalleryPowerAsync(Id, await _service.GetFurnitureByIdAsync(Id));
    }
}
