using System.Collections.Generic;
using System.Threading.Tasks;

public class AvatarsGalleryService : IAvatarsGalleryService
{
    private readonly IAvatarsGalleryRepository _avatarsGalleryRepository;

    public AvatarsGalleryService(IAvatarsGalleryRepository avatarsGalleryRepository)
    {
        _avatarsGalleryRepository = avatarsGalleryRepository;
    }

    public static AvatarsGalleryService Create()
    {
        return new AvatarsGalleryService(new AvatarsGalleryRepository());
    }

    public async Task<List<Avatars>> GetAvatarsCollectionAsync(int pageSize, int offset, string rare)
    {
        return await _avatarsGalleryRepository.GetAvatarsCollectionAsync(pageSize, offset, rare);
    }

    public async Task<int> GetAvatarsCountAsync(string rare)
    {
        return await _avatarsGalleryRepository.GetAvatarsCountAsync(rare);
    }

    public async Task InsertAvatarGalleryAsync(string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        await _avatarsGalleryRepository.InsertAvatarGalleryAsync(Id, await _service.GetAvatarByIdAsync(Id));
    }

    public async Task UpdateStatusAvatarGalleryAsync(string Id)
    {
        await _avatarsGalleryRepository.UpdateStatusAvatarGalleryAsync(Id);
    }

    public async Task<Avatars> SumPowerAvatarsGalleryAsync()
    {
        return await _avatarsGalleryRepository.SumPowerAvatarsGalleryAsync();
    }

    public async Task UpdateStarAvatarGalleryAsync(string Id, double star)
    {
        await _avatarsGalleryRepository.UpdateStarAvatarGalleryAsync(Id, star);
    }

    public async Task UpdateAvatarGalleryPowerAsync(string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        await _avatarsGalleryRepository.UpdateAvatarGalleryPowerAsync(Id, await _service.GetAvatarByIdAsync(Id));
    }
}