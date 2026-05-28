using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserResearchsService
{ 
    Task<UserResearchs> GetUserResearchsAsync(string id);
    Task InsertOrUpdateUserResearchsAsync(string user_id, UserResearchs Researchs, string id);
    Task<UserResearchs> GetSumUserResearchsAsync(string user_id);
}