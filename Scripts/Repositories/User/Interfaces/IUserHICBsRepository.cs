using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICBsRepository
{
    Task<UserHICBs> GetUserHICBsAsync(string userId, string id);
    Task InsertOrUpdateUserHICBsAsync(string userId, UserHICBs HICBs, string id);
    Task<UserHICBs> GetSumUserHICBsAsync(string userId);
}