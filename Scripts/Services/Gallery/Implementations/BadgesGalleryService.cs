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

    public async Task<List<Badges>> GetBadgesCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _badgesGalleryRepository.GetBadgesCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBadgesCountAsync(string search, string rare)
    {
        return await _badgesGalleryRepository.GetBadgesCountAsync(search, rare);
    }

    public async Task InsertBadgeGalleryAsync(string userId, string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _badgesGalleryRepository.InsertBadgeGalleryAsync(userId, Id, await _service.GetBadgeByIdAsync(Id));
    }

    public async Task UpdateStatusBadgeGalleryAsync(string userId, string Id)
    {
        await _badgesGalleryRepository.UpdateStatusBadgeGalleryAsync(userId, Id);
    }

    public async Task<Badges> SumPowerBadgesGalleryAsync(string userId)
    {
        return await _badgesGalleryRepository.SumPowerBadgesGalleryAsync(userId);
    }

    public async Task UpdateStarBadgeGalleryAsync(string userId, string Id, double star)
    {
        await _badgesGalleryRepository.UpdateStarBadgeGalleryAsync(userId, Id, star);
    }

    public async Task UpdateBadgeGalleryPowerAsync(string userId, string Id)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        await _badgesGalleryRepository.UpdateBadgeGalleryPowerAsync(userId, Id, await _service.GetBadgeByIdAsync(Id));
    }
}
