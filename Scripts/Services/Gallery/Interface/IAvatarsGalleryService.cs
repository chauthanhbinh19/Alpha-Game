using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsGalleryService
{
    Task<List<Avatars>> GetAvatarsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string search, string rare);
    Task InsertAvatarGalleryAsync(string Id);
    Task UpdateStatusAvatarGalleryAsync(string Id);
    Task UpdateStarAvatarGalleryAsync(string id, double star);
    Task UpdateAvatarGalleryPowerAsync(string id);
    Task<Avatars> SumPowerAvatarsGalleryAsync();
}