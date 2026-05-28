using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserResearchsRepository
{
    Task<UserResearchs> GetUserResearchsAsync(string type);
    Task InsertOrUpdateUserResearchsAsync(string user_id, UserResearchs Researchs, string id);
    Task<UserResearchs> GetSumUserResearchsAsync(string user_id);
}