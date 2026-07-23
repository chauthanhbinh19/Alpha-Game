using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAvatarsService
{
    Task<List<Avatars>> GetUserAvatarsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserAvatarsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserAvatarAsync(Avatars avatar, string userId);
    Task<bool> InsertUserAvatarByIdAsync(string avatarId, string userId);
    Task<bool> InsertOrUpdateUserAvatarsBatchAsync(string userId, List<Avatars> avatars);
    Task<bool> UpdateUserAvatarLevelAsync(string userId, Avatars avatar);
    Task<bool> UpdateUserAvatarStarAsync(string userId, Avatars avatar);
    Task<Avatars> GetUserAvatarByUsedAsync(string userId);
    Task UpdateIsUsedUserAvatarAsync(string avatarId, string userId, bool is_used);
    Task<Avatars> SumPowerUserAvatarsAsync(string userId);
}