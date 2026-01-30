using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsGalleryRepository
{
    Task<List<Avatars>> GetAvatarsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string search, string rare);
    Task InsertAvatarGalleryAsync(string Id, Avatars AvatarFromDB);
    Task UpdateStatusAvatarGalleryAsync(string Id);
    Task UpdateStarAvatarGalleryAsync(string id, double star);
    Task UpdateAvatarGalleryPowerAsync(string id, Avatars AvatarFromDB);
    Task<Avatars> SumPowerAvatarsGalleryAsync();
}