using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsGalleryService
{
    Task<List<Avatars>> GetAvatarsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string search, string rare);
    Task InsertAvatarGalleryAsync(string userId, string Id);
    Task UpdateStatusAvatarGalleryAsync(string userId, string Id);
    Task UpdateStarAvatarGalleryAsync(string userId, string id, double star);
    Task UpdateAvatarGalleryPowerAsync(string userId, string id);
    Task<Avatars> SumPowerAvatarsGalleryAsync(string userId);
}