using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsGalleryRepository
{
    Task<List<Avatars>> GetAvatarsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Avatars>> InsertAvatarGalleryAsync(string userId, string Id, Avatars AvatarFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusAvatarGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAvatarsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarAvatarGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarAvatarGalleryAsync(string userId, string avatarId);
    Task<InsertOrUpdateResult<List<(string AvatarId, double CurrentStar)>>> UpdateBatchCurrentStarAvatarsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Avatars>>> InsertBatchAvatarsGalleryAsync(string userId, List<Avatars> avatars);
    Task<Avatars> GetAvatarCollectionByIdAsync(string userId, string objectId);
    Task UpdateAvatarGalleryPowerAsync(string userId, string id, Avatars AvatarFromDB);
    Task<Avatars> SumPowerAvatarsGalleryAsync(string userId);
}