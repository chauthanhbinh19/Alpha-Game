using System.Collections.Generic;
using System.Threading.Tasks;

public class BadgesGalleryService : IBadgesGalleryService
{
    private readonly IBadgesGalleryRepository _BadgesGalleryRepository;

    public BadgesGalleryService(IBadgesGalleryRepository BadgesGalleryRepository)
    {
        _BadgesGalleryRepository = BadgesGalleryRepository;
    }

    public static BadgesGalleryService Create()
    {
        return new BadgesGalleryService(new BadgesGalleryRepository());
    }

    public async Task<List<Badges>> GetBadgesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _BadgesGalleryRepository.GetBadgesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesCountAsync(string search, string rare)
    {
        return await _BadgesGalleryRepository.GetBadgesCountAsync(search, rare);
    }

    public async Task InsertBadgeGalleryAsync(string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _BadgesGalleryRepository.InsertBadgeGalleryAsync(Id, await _service.GetBadgeByIdAsync(Id));
    }

    public async Task UpdateStatusBadgeGalleryAsync(string Id)
    {
        await _BadgesGalleryRepository.UpdateStatusBadgeGalleryAsync(Id);
    }

    public async Task<Badges> SumPowerBadgesGalleryAsync()
    {
        return await _BadgesGalleryRepository.SumPowerBadgesGalleryAsync();
    }

    public async Task UpdateStarBadgeGalleryAsync(string Id, double star)
    {
        await _BadgesGalleryRepository.UpdateStarBadgeGalleryAsync(Id, star);
    }

    public async Task UpdateBadgeGalleryPowerAsync(string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _BadgesGalleryRepository.UpdateBadgeGalleryPowerAsync(Id, await _service.GetBadgeByIdAsync(Id));
    }
}
