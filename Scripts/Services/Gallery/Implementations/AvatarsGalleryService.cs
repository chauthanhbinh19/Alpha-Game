using System.Collections.Generic;
using System.Threading.Tasks;

public class AvatarsGalleryService : IAvatarsGalleryService
{
    private static AvatarsGalleryService _instance;
    private readonly IAvatarsGalleryRepository _avatarsGalleryRepository;

    public AvatarsGalleryService(IAvatarsGalleryRepository avatarsGalleryRepository)
    {
        _avatarsGalleryRepository = avatarsGalleryRepository;
    }

    public static AvatarsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new AvatarsGalleryService(new AvatarsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Avatars>> GetAvatarsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        return await _avatarsGalleryRepository.GetAvatarsCollectionAsync(userId, search, pageSize, offset, rare);
    }

    public async Task<int> GetAvatarsCountAsync(string search, string rare)
    {
        return await _avatarsGalleryRepository.GetAvatarsCountAsync(search, rare);
    }

    public async Task InsertAvatarGalleryAsync(string userId, string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        await _avatarsGalleryRepository.InsertAvatarGalleryAsync(userId, Id, await _service.GetAvatarByIdAsync(Id));
    }

    public async Task UpdateStatusAvatarGalleryAsync(string userId, string Id)
    {
        await _avatarsGalleryRepository.UpdateStatusAvatarGalleryAsync(userId, Id);
    }

    public async Task<Avatars> SumPowerAvatarsGalleryAsync(string userId)
    {
        return await _avatarsGalleryRepository.SumPowerAvatarsGalleryAsync(userId);
    }

    public async Task UpdateStarAvatarGalleryAsync(string userId, string Id, double star)
    {
        await _avatarsGalleryRepository.UpdateStarAvatarGalleryAsync(userId, Id, star);
    }

    public async Task UpdateAvatarGalleryPowerAsync(string userId, string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        await _avatarsGalleryRepository.UpdateAvatarGalleryPowerAsync(userId, Id, await _service.GetAvatarByIdAsync(Id));
    }
}