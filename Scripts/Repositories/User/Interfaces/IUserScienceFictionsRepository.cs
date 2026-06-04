using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserScienceFictionsRepository
{
    Task<UserScienceFictions> GetScienceFictionsAsync(string type);
    Task InsertOrUpdateScienceFictionsAsync(string user_id, UserScienceFictions scienceFiction, string id);
    Task<UserScienceFictions> GetSumScienceFictionsAsync(string user_id);
}