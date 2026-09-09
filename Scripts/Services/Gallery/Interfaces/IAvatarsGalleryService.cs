using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsGalleryService
{
    Task<List<Avatars>> GetAvatarsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertAvatarGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusAvatarGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAvatarsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarAvatarGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarAvatarGalleryAsync(string userId, string avatarId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarAvatarsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchAvatarsGalleryAsync(string userId, List<Avatars> avatars);
    Task<Avatars> GetAvatarCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateAvatarGalleryPowerAsync(string userId, string id);
    Task<Avatars> SumPowerAvatarsGalleryAsync(string userId);
}