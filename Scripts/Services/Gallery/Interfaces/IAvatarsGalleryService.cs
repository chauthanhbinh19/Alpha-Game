using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsGalleryService
{
    Task<List<Avatars>> GetAvatarsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string search, string rare);
    Task<bool> InsertAvatarGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusAvatarGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusAvatarsGalleryAsync(string userId);
    Task<bool> UpdateStarAvatarGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarAvatarGalleryAsync(string userId, string avatarId);
    Task<bool> UpdateBatchCurrentStarAvatarsGalleryAsync(string userId);
    Task<bool> InsertBatchAvatarsGalleryAsync(string userId, List<Avatars> avatars);
    Task<Avatars> GetAvatarCollectionByIdAsync(string userId, string objectId);
    Task UpdateAvatarGalleryPowerAsync(string userId, string id);
    Task<Avatars> SumPowerAvatarsGalleryAsync(string userId);
}