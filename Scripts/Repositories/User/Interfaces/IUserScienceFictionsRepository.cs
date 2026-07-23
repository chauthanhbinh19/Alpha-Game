using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserScienceFictionsRepository
{
    Task<UserScienceFictions> GetUserScienceFictionsAsync(string userId, string id);
    Task InsertOrUpdateUserScienceFictionsAsync(string userId, UserScienceFictions scienceFiction, string id);
    Task<UserScienceFictions> GetSumUserScienceFictionsAsync(string userId);
}