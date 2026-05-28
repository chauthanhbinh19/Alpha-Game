using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIENsService
{ 
    Task<UserHIENs> GetUserHIENsAsync(string id);
    Task InsertOrUpdateUserHIENsAsync(string user_id, UserHIENs HIENs, string id);
    Task<UserHIENs> GetSumUserHIENsAsync(string user_id);
}