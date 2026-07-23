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

    public async Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _furnituresGalleryRepository.GetFurnituresCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string search, string type, string rare)
    {
        return await _furnituresGalleryRepository.GetFurnituresCountAsync(search, type, rare);
    }

    public async Task InsertFurnitureGalleryAsync(string userId, string Id)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _furnituresGalleryRepository.InsertFurnitureGalleryAsync(userId, Id, await _service.GetFurnitureByIdAsync(Id));
    }

    public async Task UpdateStatusFurnitureGalleryAsync(string userId, string Id)
    {
        await _furnituresGalleryRepository.UpdateStatusFurnitureGalleryAsync(userId, Id);
    }

    public async Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId)
    {
        return await _furnituresGalleryRepository.SumPowerFurnituresGalleryAsync(userId);
    }

    public async Task UpdateStarFurnitureGalleryAsync(string userId, string Id, double star)
    {
        await _furnituresGalleryRepository.UpdateStarFurnitureGalleryAsync(userId, Id, star);
    }

    public async Task UpdateFurnitureGalleryPowerAsync(string userId, string Id)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _furnituresGalleryRepository.UpdateFurnitureGalleryPowerAsync(userId, Id, await _service.GetFurnitureByIdAsync(Id));
    }
}
