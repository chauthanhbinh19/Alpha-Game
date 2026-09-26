using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserScienceFictionsService
{ 
    Task<UserScienceFictions> GetUserScienceFictionsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserScienceFictionsAsync(string userId, UserScienceFictions scienceFiction, string id);
    Task<UserScienceFictions> GetSumUserScienceFictionsAsync(string userId);
}