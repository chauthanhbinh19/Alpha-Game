using System.Collections.Generic;
using System.Threading.Tasks;

public class OutfitsGalleryService : IOutfitsGalleryService
{
    private static OutfitsGalleryService _instance;
    private readonly IOutfitsGalleryRepository _weaponsGalleryRepository;

    public OutfitsGalleryService(IOutfitsGalleryRepository weaponsGalleryRepository)
    {
        _weaponsGalleryRepository = weaponsGalleryRepository;
    }

    public static OutfitsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new OutfitsGalleryService(new OutfitsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Outfits>> GetOutfitsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> list = await _weaponsGalleryRepository.GetOutfitsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetOutfitsCountAsync(string search, string type, string rare)
    {
        return await _weaponsGalleryRepository.GetOutfitsCountAsync(search, type, rare);
    }

    public async Task InsertOutfitGalleryAsync(string userId, string Id)
    {
        IOutfitsRepository _repository = new OutfitsRepository();
        OutfitsService _service = new OutfitsService(_repository);
        await _weaponsGalleryRepository.InsertOutfitGalleryAsync(userId, Id, await _service.GetOutfitByIdAsync(Id));
    }

    public async Task UpdateStatusOutfitGalleryAsync(string userId, string Id)
    {
        await _weaponsGalleryRepository.UpdateStatusOutfitGalleryAsync(userId, Id);
    }

    public async Task<Outfits> SumPowerOutfitsGalleryAsync(string userId)
    {
        return await _weaponsGalleryRepository.SumPowerOutfitsGalleryAsync(userId);
    }

    public async Task UpdateStarOutfitGalleryAsync(string userId, string Id, double star)
    {
        await _weaponsGalleryRepository.UpdateStarOutfitGalleryAsync(userId, Id, star);
    }

    public async Task UpdateOutfitGalleryPowerAsync(string userId, string Id)
    {
        IOutfitsRepository _repository = new OutfitsRepository();
        OutfitsService _service = new OutfitsService(_repository);
        await _weaponsGalleryRepository.UpdateOutfitGalleryPowerAsync(userId, Id, await _service.GetOutfitByIdAsync(Id));
    }
}
