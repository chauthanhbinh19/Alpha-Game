using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICBsService
{ 
    Task<UserHICBs> GetUserHICBsAsync(string id);
    Task InsertOrUpdateUserHICBsAsync(string user_id, UserHICBs HICBs, string id);
    Task<UserHICBs> GetSumUserHICBsAsync(string user_id);
}