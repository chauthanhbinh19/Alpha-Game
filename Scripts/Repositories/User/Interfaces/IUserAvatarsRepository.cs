using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAvatarsRepository
{
    Task<List<Avatars>> GetUserAvatarsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserAvatarsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserAvatarAsync(Avatars avatar, string userId);
    Task<bool> InsertUserAvatarByIdAsync(Avatars avatar, string userId);
    Task<bool> InsertOrUpdateUserAvatarsBatchAsync(string userId, List<Avatars> avatars);
    Task<Avatars> GetUserAvatarByUsedAsync(string userId);
    Task<bool> UpdateUserAvatarLevelAsync(string userId, Avatars avatar);
    Task<bool> UpdateUserAvatarStarAsync(string userId, Avatars avatar);
    Task UpdateIsUsedUserAvatarAsync(string avatarId, string userId, bool is_used);
    Task<Avatars> SumPowerUserAvatarsAsync(string userId);
}