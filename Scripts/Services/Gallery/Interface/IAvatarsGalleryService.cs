using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsGalleryService
{
    Task<List<Avatars>> GetAvatarsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string rare);
    Task InsertAvatarGalleryAsync(string Id);
    Task UpdateStatusAvatarGalleryAsync(string Id);
    Task UpdateStarAvatarGalleryAsync(string id, double star);
    Task UpdateAvatarGalleryPowerAsync(string id);
    Task<Avatars> SumPowerAvatarsGalleryAsync();
}