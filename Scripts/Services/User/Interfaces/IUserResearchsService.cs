using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserResearchsService
{ 
    Task<UserResearchs> GetUserResearchsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserResearchsAsync(string userId, UserResearchs Researchs, string id);
    Task<UserResearchs> GetSumUserResearchsAsync(string userId);
}