using System.Collections.Generic;
using System.Threading.Tasks;

public class BadgesGalleryService : IBadgesGalleryService
{
    private static BadgesGalleryService _instance;
    private readonly IBadgesGalleryRepository _badgesGalleryRepository;

    public BadgesGalleryService(IBadgesGalleryRepository badgesGalleryRepository)
    {
        _badgesGalleryRepository = badgesGalleryRepository;
    }

    public static BadgesGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new BadgesGalleryService(new BadgesGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Badges>> GetBadgesCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _badgesGalleryRepository.GetBadgesCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesCountAsync(string search, string rare)
    {
        return await _badgesGalleryRepository.GetBadgesCountAsync(search, rare);
    }

    public async Task InsertBadgeGalleryAsync(string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _badgesGalleryRepository.InsertBadgeGalleryAsync(Id, await _service.GetBadgeByIdAsync(Id));
    }

    public async Task UpdateStatusBadgeGalleryAsync(string Id)
    {
        await _badgesGalleryRepository.UpdateStatusBadgeGalleryAsync(Id);
    }

    public async Task<Badges> SumPowerBadgesGalleryAsync()
    {
        return await _badgesGalleryRepository.SumPowerBadgesGalleryAsync();
    }

    public async Task UpdateStarBadgeGalleryAsync(string Id, double star)
    {
        await _badgesGalleryRepository.UpdateStarBadgeGalleryAsync(Id, star);
    }

    public async Task UpdateBadgeGalleryPowerAsync(string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _badgesGalleryRepository.UpdateBadgeGalleryPowerAsync(Id, await _service.GetBadgeByIdAsync(Id));
    }
}
