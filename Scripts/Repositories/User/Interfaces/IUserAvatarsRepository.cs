using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAvatarsRepository
{
    Task<List<Avatars>> GetUserAvatarsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserAvatarsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserAvatarByIdAsync(Avatars avatar, string userId);
    Task<InsertOrUpdateResult<Avatars>> InsertOrUpdateUserAvatarAsync(string userId, Avatars avatar);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Avatars>>> InsertOrUpdateUserAvatarsBatchAsync(string userId, List<Avatars> avatars);
    Task<InsertOrUpdateResult<bool>> UpdateUserAvatarLevelAsync(string userId, Avatars avatar);
    Task<InsertOrUpdateResult<bool>> UpdateUserAvatarStarAsync(string userId, Avatars avatar);
    Task<Avatars> GetUserAvatarByUsedAsync(string userId);
    Task<Avatars> GetUserAvatarByIdAsync(string userId, string Id);
    Task UpdateIsUsedUserAvatarAsync(string avatarId, string userId, bool is_used);
    Task<Avatars> SumPowerUserAvatarsAsync(string userId);
}