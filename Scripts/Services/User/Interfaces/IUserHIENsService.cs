using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIENsService
{ 
    Task<UserHIENs> GetUserHIENsAsync(string userId, string id);
    Task InsertOrUpdateUserHIENsAsync(string userId, UserHIENs HIENs, string id);
    Task<UserHIENs> GetSumUserHIENsAsync(string userId);
}