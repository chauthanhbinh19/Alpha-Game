using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserScienceFictionsService
{ 
    Task<UserScienceFictions> GetUserScienceFictionsAsync(string id);
    Task InsertOrUpdateUserScienceFictionsAsync(string user_id, UserScienceFictions scienceFiction, string id);
    Task<UserScienceFictions> GetSumUserScienceFictionsAsync(string user_id);
}