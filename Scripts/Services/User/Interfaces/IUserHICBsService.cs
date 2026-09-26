using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICBsService
{ 
    Task<UserHICBs> GetUserHICBsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHICBsAsync(string userId, UserHICBs HICBs, string id);
    Task<UserHICBs> GetSumUserHICBsAsync(string userId);
}