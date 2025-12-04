using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAvatarsService
{
    Task<List<Avatars>> GetUserAvatarsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserAvatarsCountAsync(string user_id, string rare);
    Task<bool> InsertUserAvatarAsync(Avatars avatars, string userId);
    Task<bool> InsertUserAvatarByIdAsync(string avatarId, string userId);
    Task<Avatars> GetAvatarByUsedAsync(string user_id);
    Task UpdateIsUsedAvatarAsync(string avatarId, string userId, bool is_used);
    Task<Avatars> SumPowerUserAvatarsAsync();
}