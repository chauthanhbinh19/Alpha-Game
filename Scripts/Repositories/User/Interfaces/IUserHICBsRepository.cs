using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICBsRepository
{
    Task<UserHICBs> GetUserHICBsAsync(string type);
    Task InsertOrUpdateUserHICBsAsync(string user_id, UserHICBs HICBs, string id);
    Task<UserHICBs> GetSumUserHICBsAsync(string user_id);
}