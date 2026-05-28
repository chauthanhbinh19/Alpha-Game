using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAvatarsRepository
{
    Task<List<Avatars>> GetUserAvatarsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserAvatarsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserAvatarAsync(Avatars avatar, string userId);
    Task<bool> InsertUserAvatarByIdAsync(Avatars avatar, string userId);
    Task<bool> InsertOrUpdateUserAvatarsBatchAsync(List<Avatars> avatars);
    Task<Avatars> GetAvatarByUsedAsync(string user_id);
    Task UpdateIsUsedAvatarAsync(string avatarId, string userId, bool is_used);
    Task<Avatars> SumPowerUserAvatarsAsync();
}